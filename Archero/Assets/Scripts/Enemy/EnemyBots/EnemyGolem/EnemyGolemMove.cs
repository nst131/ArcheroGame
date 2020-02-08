using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolemMove : Enemy
{
    private GameObject _golem;
    private GameObject _player;
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;

    private float timing;

    private bool moving = false;
    private bool moves = false;
    private bool attack = false;

    private void Start()
    {
        _golem = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        RotateToPlayer();
        Tactic();
        Stand();
    }
    public override void Tactic()
    {
        if (_golem.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        if (!moves)
        {
            moves = true;
            Move();
        }

        if (!attack && !moving)
        {
            attack = true;
            ShootAttack();
            StartCoroutine(WaitingTime());
        }
    }
    private IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(3);
   
        moves = false;
        attack = false;
    }

    public override void ShootAttack()
    {
        _golem.GetComponent<EnemyGolemAttack>().Attack();
    }

    public override void Move()
    {
        if (gameObject.GetComponent<HealthHelper>().Dead || !_player || (_player && _player.GetComponent<HealthHelper>().Dead)
            || _player.transform.position == Vector3.zero)
            return;

        if (_navMeshAgent.isStopped)
            _navMeshAgent.isStopped = false;
        _anim.SetBool("Move", true);
        _navMeshAgent.SetDestination(_player.transform.position);
        timing = Time.time;
        moving = true;
    }
    private void Stand()
    {
        if (Time.time < timing + 1 || GetComponent<HealthHelper>().Dead)
            return;

        _navMeshAgent.isStopped = true;
        _anim.SetBool("Move", false);
        moving = false;
    }

    public override void RotateToPlayer()
    {
        if (_golem.GetComponent<EnemyGolemAttack>().ReloadingAttack || moving || _golem.GetComponent<HealthHelper>().Dead 
            || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _player.transform.position - _golem.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _golem.transform.rotation = Quaternion.Lerp(_golem.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
