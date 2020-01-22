using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private GameObject _Target;
    private NavMeshAgent _Bot;
    private Animator _anim;
    private EnemyAttack _enemyAttack;
    private Vector3 StartPoint;

    private float FirstBlood=0;
    public float Firstblood { get { return FirstBlood; } }
    private float LastAttack;

    private bool Moving=false;

    void Start()
    {
        StartPoint = transform.position;
        _Target = GameObject.FindGameObjectWithTag("Player").gameObject;
        _Bot = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        if (!_Target)
            return;

        if(Vector3.Distance(transform.position,_Target.transform.position) <= _enemyAttack.Range && FirstBlood==0)
        {
            _enemyAttack.Attack();
            StartCoroutine(WateAnimation());
        }
        else if(FirstBlood==0)
        {
            Move(_Target.transform.position);
        }
        else
        {
            if (Time.time > LastAttack + 1)
                FirstBlood = 0;
            else
            Move(StartPoint);
        }

        if (_Target.GetComponent<HealthHelper>().Dead)
            _Target = null;
    }
    IEnumerator WateAnimation()
    {
        yield return new WaitForSeconds(1);

        FirstBlood++;
        LastAttack = Time.time;
    }

    void Move(Vector3 Target)
    {
        if (_Target.GetComponent<HealthHelper>().Dead || _Target.transform.position==Vector3.zero 
            || gameObject.GetComponent<HealthHelper>().Dead)
            return;

        _Bot.SetDestination(Target);
        Animation();
    }
    void Animation()
    {
        Moving = false;
        if (Vector3.Distance(transform.position, _Target.transform.position) > _Bot.stoppingDistance)
            Moving = true;

        if (_anim.GetBool("Moving") != Moving)
            _anim.SetBool("Moving", Moving);
    }
}
