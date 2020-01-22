using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArcherMove : MonoBehaviour
{
    private GameObject _Target;
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;
    private EnemyArcherAttack _attack;
    [HideInInspector]
    public LineRenderer _lineRenderer;

    private float Timing;
    [HideInInspector]
    public bool Moving=false;
    private bool Moves = false;
    private bool Attack = false;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetWidth(0.1f, 0.1f);
        _lineRenderer.enabled = false;

        _Target = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _attack = GetComponentInChildren<EnemyArcherAttack>();
    }

    void Update()
    {
        _attack.RotatetoTarget();    //Поворачиваемся к цели.

        Tactic();

        MoveWithTime();   //Остановка через какой то  Time , если мы Move()

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _Target.transform.position);
    }
    void Tactic()
    {
        if(!Moves)
        {
            Moves = true;
            Move();
        }
        if(!Attack && !Moving)
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
        if (Time.time < Timing + 1)
            return;
        _navMeshAgent.isStopped = true;
        _anim.SetBool("Move", false);
        Moving = false;
    }
}
