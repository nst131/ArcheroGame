using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttack : MonoBehaviour
{
    private GameObject _melee;
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;

    [Header("DescriptionAttack")]
    [SerializeField] private float radiusAttack = 1.5f;
    [SerializeField] private float speedAttack = 5;
    [SerializeField] private float damageAttack = 15;
    [SerializeField] private float rangeAttack = 3;
    public float RangeAttack { get { return rangeAttack; } }

    private float timeLastAttack;

    private void Start()
    {
        _melee = gameObject;
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void ShootAttack()
    {
        if (_melee.GetComponent<HealthHelper>().Dead )
            return;

        if (Time.time < timeLastAttack + speedAttack)
            return;

        Vector3 pointAttack = _melee.transform.position + _melee.transform.forward * rangeAttack;

        if (_anim)
        { _anim.SetTrigger("Attack"); _anim.SetBool("Move", false); }
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
                _anim.ResetTrigger("Attack");
            }
            if (item.GetComponent<HealthHelper>() && !item.GetComponent<HealthHelper>().Dead
                && item.tag == "Player" && _melee.GetComponent<EnemyMeleeMove>().FirstAttack == 0)
            {
                item.GetComponent<HealthHelper>().TakeAwayHP(damageAttack);
            }
        }
    }
}
