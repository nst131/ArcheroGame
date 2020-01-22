using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGiantMove : MonoBehaviour
{
    private GameObject _Target;
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;
    private EnemyGiantAttack _attack;

    private float Timing;
    [HideInInspector]
    public bool Moving = false;
    private bool Moves = false;
    private bool Attack = false;

    void Start()
    {
        _Target = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _attack = GetComponent<EnemyGiantAttack>();
    }

    void Update()
    {
        _attack.RotatetoTarget();    //Поворачиваемся к цели.

        Tactic();
       
        MoveWithTime();   //Остановка через какой то  Time , если мы Move()

    }
    void Tactic()
    {
        if (!Moves)
        {
            Moves = true;
            Move();
        }
        if (!Attack && !Moving)
        {
            Attack = true;
            _attack.Damage();
            StartCoroutine(Wate());
        }
    }
    IEnumerator Wate()
    {
        yield return new WaitForSeconds(3);

        Moves = false;
        Attack = false;
    }

    void Move()
    {
        if (gameObject.GetComponent<HealthHelper>().Dead || !_Target || (_Target && _Target.GetComponent<HealthHelper>().Dead)
            || _Target.transform.position == Vector3.zero)
            return;

        if (_navMeshAgent.isStopped)
            _navMeshAgent.isStopped = false;
        _anim.SetBool("Move", true);
        _navMeshAgent.SetDestination(_Target.transform.position);
        Timing = Time.time;
        Moving = true;
    }
    void MoveWithTime()
    {
        if (Time.time < Timing + 1 || GetComponent<HealthHelper>().Dead)
            return;

        _navMeshAgent.isStopped = true;
        _anim.SetBool("Move", false);
        Moving = false;
    }
}
