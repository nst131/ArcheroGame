using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttack : MonoBehaviour
{
    private GameObject _melee;
    private Animator _animMelee;
    private NavMeshAgent _navMeshAgent;
    private GameManager _eachData;

    [Header("DescriptionAttack")]
    [SerializeField] private float radiusAttack = 1.5f;
    [SerializeField] private float speedAttack = 5;
    [SerializeField] private float rangeAttack = 3;
    public float RangeAttack { get { return rangeAttack; } }

    private float damageAttack;
    private float timeLastAttack;

    private void Start()
    {
        _melee = gameObject;
        _animMelee = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _eachData = GameObject.FindObjectOfType<GameManager>();
        damageAttack = _eachData.GetComponent<EnemyBotsData>().Damage;
    }

    public void Attack()
    {
        if (!_melee || _melee.GetComponent<HealthHelper>().Dead )
            return;

        if (Time.time < timeLastAttack + speedAttack)
            return;

        Vector3 pointAttack = _melee.transform.position + _melee.transform.forward * rangeAttack;

        if (_animMelee)
        { _animMelee.SetTrigger("Attack"); _animMelee.SetBool("Move", false); }
        StartCoroutine(ExpactAnimAttack(pointAttack));
        timeLastAttack = Time.time;
    }

    private IEnumerator ExpactAnimAttack(Vector3 pointAttack)
    {
        yield return new WaitForSeconds(1);

        Collider[] colliders = Physics.OverlapSphere(pointAttack, radiusAttack);
        foreach (var item in colliders)
        {
            if(_melee.GetComponent<EnemyMeleeMove>().FirstAttack == 0)
            {
                _animMelee.ResetTrigger("Attack");
            }
            if (item.GetComponent<HealthHelper>() && !item.GetComponent<HealthHelper>().Dead
                && item.tag == "Player" && _melee.GetComponent<EnemyMeleeMove>().FirstAttack == 0)
            {
                item.GetComponent<HealthHelper>().TakeAwayHP(damageAttack);
            }
        }
    }
}
