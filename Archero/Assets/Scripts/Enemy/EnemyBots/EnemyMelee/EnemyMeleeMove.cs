using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeMove : Enemy
{
    private GameObject _melee;
    private GameObject _player;
    private NavMeshAgent _navMeshAgent;
    private Animator _anim;
    private Vector3 _spawnPoint;

    private float lastAttack;
    private float firstAttack;
    public float FirstAttack { get { return firstAttack; } }

    private bool moving;

    private void Start()
    {
        _melee = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _spawnPoint = transform.position;
    }

    private void Update()
    {
        Tactic();
    }

    public override void Tactic()
    {
        if (!_player)
            return;

        if (Vector3.Distance(_melee.transform.position, _player.transform.position) 
            <= _melee.GetComponent<EnemyMeleeAttack>().RangeAttack && firstAttack == 0)
        {
            RotateToPlayer();
            ShootAttack();
            StartCoroutine(WaitingEndAttack());
        }
        else if (firstAttack == 0)
        {
            Move();
        }
        else
        {
            if (Time.time > lastAttack + 1)
                firstAttack = 0;
            else
                MoveBack(_spawnPoint);
        }

        if (_player.GetComponent<HealthHelper>().Dead)
            _player = null;
    }

    private IEnumerator WaitingEndAttack()
    {
        yield return new WaitForSeconds(1);

        firstAttack++;
        lastAttack = Time.time;
    }

    public override void ShootAttack()
    {
        _melee.GetComponent<EnemyMeleeAttack>().Attack();
    }

    private void MoveBack(Vector3 startPlayerPosition)
    {
        if (_player.GetComponent<HealthHelper>().Dead || _player.transform.position==Vector3.zero 
            || _melee.GetComponent<HealthHelper>().Dead)
            return;

        _navMeshAgent.SetDestination(startPlayerPosition);
        Animation();
    }

    public override void Move()
    {
        if (_player.GetComponent<HealthHelper>().Dead || _player.transform.position == Vector3.zero
            || _melee.GetComponent<HealthHelper>().Dead)
            return;

        _navMeshAgent.SetDestination(_player.transform.position);
        Animation();
    }

    private void Animation()
    {
        moving = false;
        if (Vector3.Distance(_melee.transform.position, _player.transform.position) > _navMeshAgent.stoppingDistance)
            moving = true;

        if (_anim.GetBool("Move") != moving)
            _anim.SetBool("Move", moving);
    }

    public override void RotateToPlayer()
    {
        if (_melee.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _player.transform.position - _melee.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _melee.transform.rotation = Quaternion.Lerp(_melee.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
