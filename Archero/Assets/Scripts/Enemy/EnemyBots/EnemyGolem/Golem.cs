using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Golem : Enemy
{
    private GameObject _player;
    private HealthHelper _playerHealth;
    [SerializeField] private GameObject _golem;
    [SerializeField] private HealthHelper _golemHealth;
    [SerializeField] private GolemAttack _golemAttack;
    [SerializeField] private Animator _golemAnim;
    [SerializeField] private NavMeshAgent _golemNavMesh;
    [SerializeField] private Collider _golemCollider;
    [SerializeField] private Drop _golemDrop;
    private BotsData _botsData;

    private float _timing;

    private bool _moving = false;
    private bool _moves = false;
    private bool _attack = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
        _botsData = GameObject.FindObjectOfType<LevelUp>().GetComponent<BotsData>();
    }

    private void Update()
    {
        RotateToPlayer();
        Tactic();
        Stand();
    }
    public override void Tactic()
    {
        if (_golemHealth.Dead || !_player || _playerHealth.Dead)
            return;

        if (!_moves)
        {
            _moves = true;
            Move();
        }

        if (!_attack && !_moving)
        {
            _attack = true;
            ShootAttack();
            StartCoroutine(WaitingTime());
        }
    }
    private IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(3);
   
        _moves = false;
        _attack = false;
    }

    public override void ShootAttack()
    {
        _golemAttack.Attack();
    }

    public override void Clash()
    {
        _golemAttack.OnTriggerEnter(_golemCollider);
    }

    public override void LevelUp()
    {
        _botsData.InvokeEventLevelUpBots();
    }

    public override void DropThings()
    {
        _golemDrop.InvokeEventScatterCoins();
        _golemDrop.InvokeEventScatterHealth();
    }

    public override void Move()
    {
        if (!_golem || _golemHealth.Dead || !_player || (_player && _playerHealth.Dead)
            || _player.transform.position == Vector3.zero)
            return;

        if (_golemNavMesh.isStopped)
            _golemNavMesh.isStopped = false;
        _golemAnim.SetBool("Move", true);
        _golemNavMesh.SetDestination(_player.transform.position);
        _timing = Time.time;
        _moving = true;
    }
    private void Stand()
    {
        if (Time.time < _timing + 1 || _golemHealth.Dead)
            return;

        _golemNavMesh.isStopped = true;
        _golemAnim.SetBool("Move", false);
        _moving = false;
    }

    public override void RotateToPlayer()
    {
        if (_golemAttack.ReloadingAttack || _moving || _golemHealth.Dead 
            || !_player || _playerHealth.Dead)
            return;

        Vector3 direction = _player.transform.position - _golem.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _golem.transform.rotation = Quaternion.Lerp(_golem.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
