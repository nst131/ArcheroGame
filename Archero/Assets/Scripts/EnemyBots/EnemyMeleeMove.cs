using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeMove : MonoBehaviour
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

    private void Tactic()
    {
        if (!_player)
            return;

        if (Vector3.Distance(_melee.transform.position, _player.transform.position) 
            <= _melee.GetComponent<EnemyMeleeAttack>().RangeAttack && firstAttack == 0)
        {
            RotateToPlayer();
            _melee.GetComponent<EnemyMeleeAttack>().ShootAttack();
            StartCoroutine(WaitingEndAttack());
        }
        else if (firstAttack == 0)
        {
            Move(_player.transform.position);
        }
        else
        {
            if (Time.time > lastAttack + 1)
                firstAttack = 0;
            else
                Move(_spawnPoint);
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

    private void Move(Vector3 Player)
    {
        if (_player.GetComponent<HealthHelper>().Dead || _player.transform.position==Vector3.zero 
            || _melee.GetComponent<HealthHelper>().Dead)
            return;

        _navMeshAgent.SetDestination(Player);
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

    private void RotateToPlayer()
    {
        if (_melee.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _player.transform.position - _melee.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _melee.transform.rotation = Quaternion.Lerp(_melee.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
