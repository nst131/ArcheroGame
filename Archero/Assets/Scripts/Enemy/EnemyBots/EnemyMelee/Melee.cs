using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Melee : Enemy
{
    private GameObject _player;
    private HealthHelper _playerHealth;
    [SerializeField] private GameObject _melee;
    [SerializeField] private NavMeshAgent _meleeNavMesh;
    [SerializeField] private Animator _meleeAnim;
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private HealthHelper _meleeHealth;
    [SerializeField] private Collider _meleeCollider;
    [SerializeField] private Drop _meleeDrop;
    private Vector3 _meleeSpawn;
    private BotsData _botsData;

    private float _lastAttack;
    private float _firstAttack;
    public float FirstAttack { get { return _firstAttack; } }

    private bool _moving;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerHealth = _player.GetComponent<HealthHelper>();
        _meleeSpawn = _melee.transform.position;
        _botsData = GameObject.FindObjectOfType<LevelUp>().GetComponent<BotsData>();
    }

    private void Update()
    {
        Tactic();
    }

    public override void Tactic()
    {
        if (!_player || _playerHealth.Dead)
            return;

        if (Vector3.Distance(_melee.transform.position, _player.transform.position) 
            <= _meleeAttack.RangeAttack && _firstAttack == 0)
        {
            RotateToPlayer();
            ShootAttack();
            StartCoroutine(WaitingEndAttack());
        }
        else if (_firstAttack == 0)
        {
            Move();
        }
        else
        {
            if (Time.time > _lastAttack + 1)
                _firstAttack = 0;
            else
                MoveBack(_meleeSpawn);
        }
    }

    private IEnumerator WaitingEndAttack()
    {
        yield return new WaitForSeconds(1);

        _firstAttack++;
        _lastAttack = Time.time;
    }

    public override void ShootAttack()
    {
        _meleeAttack.Attack();
    }

    public override void Clash()
    {
        _meleeAttack.OnTriggerEnter(_meleeCollider);
    }

    public override void LevelUp()
    {
        _botsData.InvokeEventLevelUpBots();
    }

    public override void DropThings()
    {
        _meleeDrop.InvokeEventScatterCoins();
        _meleeDrop.InvokeEventScatterHealth();
    }

    private void MoveBack(Vector3 startPlayerPosition)
    {
        if (_playerHealth.Dead || _player.transform.position==Vector3.zero 
            || _meleeHealth.Dead)
            return;

        _meleeNavMesh.SetDestination(startPlayerPosition);
        Animation();
    }

    public override void Move()
    {
        if (_playerHealth.Dead || _player.transform.position == Vector3.zero
            || _meleeHealth.Dead)
            return;

        _meleeNavMesh.SetDestination(_player.transform.position);
        Animation();
    }

    private void Animation()
    {
        _moving = false;
        if (Vector3.Distance(_melee.transform.position, _player.transform.position) > _meleeNavMesh.stoppingDistance)
            _moving = true;

        if (_meleeAnim.GetBool("Move") != _moving)
            _meleeAnim.SetBool("Move", _moving);
    }

    public override void RotateToPlayer()
    {
        if (_meleeHealth.Dead || !_player || _playerHealth.Dead)
            return;

        Vector3 direction = _player.transform.position - _melee.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _melee.transform.rotation = Quaternion.Lerp(_melee.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
