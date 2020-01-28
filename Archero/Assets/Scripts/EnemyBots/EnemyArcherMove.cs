using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArcherMove : MonoBehaviour
{
    private GameObject _archer;
    private GameObject _player;
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;
    private LineRenderer _lineRenderer;

    private float timing;

    private bool moving = false;
    private bool moves = false;
    private bool attack = false;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetWidth(0.1f, 0.1f);
        _lineRenderer.enabled = false;

        _archer = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        RotateToPlayer();
        Tactic();
        Stand();
        AimLineRenderer();
    }

    private void AimLineRenderer()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _player.transform.position);
    }

    private void Tactic()
    {
        if(!moves)
        {
            moves = true;
            Move();
        }

        if(!attack && !moving)
        {
            attack = true;
            _archer.GetComponent<EnemyArcherAttack>().Damage();
            StartCoroutine(WatingTime());
        }
    }

    private IEnumerator WatingTime()
    {
        yield return new WaitForSeconds(3);

        moves = false;
        attack = false;
    }

    private void Move()
    { 
        if (_archer.GetComponent<HealthHelper>().Dead || !_player || (_player && _player.GetComponent<HealthHelper>().Dead)
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
        if (Time.time < timing + 1 || _archer.GetComponent<HealthHelper>().Dead)
            return;

        _navMeshAgent.isStopped = true;
        _anim.SetBool("Move", false);
        moving = false;
    }

    private void RotateToPlayer()
    {
        if (_archer.GetComponent<EnemyArcherAttack>().ReloadingAttack || moving || _archer.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _player.transform.position - _archer.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _archer.transform.rotation = Quaternion.Lerp(_archer.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
