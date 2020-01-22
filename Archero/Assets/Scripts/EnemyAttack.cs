using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float Range = 3;
    public float Radius = 1.5f;
    public float AttackSpeed = 5;
    public float Damage = 15;

    private float LastAttack;
    private Animator _anim;
    private HealthHelper _healthHelper;
    private NavMeshAgent _navMeshAgent;
    private EnemyMove _enemyMove;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _anim = GetComponent<Animator>();
        _healthHelper = GetComponent<HealthHelper>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (_healthHelper && _healthHelper.Dead )
            return;

        if (Time.time < LastAttack + AttackSpeed)
            return;

        Vector3 PointAttack = transform.position + transform.forward * Range;
        Debug.DrawLine(transform.position, PointAttack, Color.red, 3);
        if (_anim)
        { _anim.SetTrigger("Attack"); _anim.SetBool("Moving", false); }
        StartCoroutine(AttackCaroutine(PointAttack));
        LastAttack = Time.time;
    }
    IEnumerator AttackCaroutine(Vector3 PointAttack)
    {
        yield return new WaitForSeconds(1);

        Collider[] colliders = Physics.OverlapSphere(PointAttack, Radius);
        foreach (var item in colliders)
        {
            if(gameObject.GetComponent<EnemyMove>().Firstblood == 0)
            {
                _anim.ResetTrigger("Attack");
            }
            if (item.GetComponent<HealthHelper>() && !item.GetComponent<HealthHelper>().Dead
                && item.gameObject.tag == "Player" && gameObject.GetComponent<EnemyMove>().Firstblood == 0)
            {
                item.GetComponent<HealthHelper>().TakeAwayHP(Damage);
            }
        }
    }
}
