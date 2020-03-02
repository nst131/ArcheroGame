using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour
{
    private LevelUp _levelUp;
    [SerializeField] private GameObject _melee;
    [SerializeField] private HealthHelper _meleeHealth;
    [SerializeField] private Animator _meleeAnim;
    [SerializeField] private NavMeshAgent _meleeNavMesh;
    [SerializeField] private Melee _meleeAttack;

    [Header("DescriptionAttack")]
    [SerializeField] private float _radiusAttack = 1.5f;
    [SerializeField] private float _speedAttack = 5;
    [SerializeField] private float _rangeAttack = 3;
    [SerializeField] private float _forceClash = 20f;
    public float RangeAttack { get { return _rangeAttack; } }

    private float _damageAttack;
    private float _timeLastAttack;

    private void Start()
    {
        _levelUp = GameObject.FindObjectOfType<LevelUp>();
        _damageAttack = _levelUp.GetComponent<BotsData>().Damage;
    }

    public void Attack()
    {
        if (!_melee || _meleeHealth.Dead )
            return;

        if (Time.time < _timeLastAttack + _speedAttack)
            return;

        Vector3 pointAttack = _melee.transform.position + _melee.transform.forward * _rangeAttack;

        if (_meleeAnim)
        { _meleeAnim.SetTrigger("Attack"); _meleeAnim.SetBool("Move", false); }
        StartCoroutine(ExpactAnimAttack(pointAttack));
        _timeLastAttack = Time.time;
    }

    private IEnumerator ExpactAnimAttack(Vector3 pointAttack)
    {
        yield return new WaitForSeconds(1);

        Collider[] colliders = Physics.OverlapSphere(pointAttack, _radiusAttack);
        foreach (var item in colliders)
        {
            if(_meleeAttack.FirstAttack == 0)
            {
                _meleeAnim.ResetTrigger("Attack");
            }
            if (item.GetComponent<HealthHelper>() && !item.GetComponent<HealthHelper>().Dead
                && item.tag == "Player" && _meleeAttack.FirstAttack == 0)
            {
                item.GetComponent<HealthHelper>().TakeAwayHP(_damageAttack);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_forceClash);
        }
    }
}
