using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Archer : Enemy
{
    private GameObject _player;
    private HealthHelper _playerHealth;
    [SerializeField] private GameObject _archer;
    [SerializeField] private Animator _archerAnim;
    [SerializeField] private NavMeshAgent _archerNavMesh;
    [SerializeField] private LineRenderer _archerLineRenderer;
    [SerializeField] private ArcherAttack _archerAttack;
    [SerializeField] private HealthHelper _archerHealth;
    [SerializeField] private Collider _archerCollider;
    [SerializeField] private Drop _archerDrop;
    private BotsData _botsData;

    private float _timing = 0f;
    private float _timingMove = 1f;
    private float _watingTimeAttack = 5f;

    private bool _moving = false;
    private bool _moves = false;
    private bool _attack = false;

    private void Start()
    {
        _archerLineRenderer.SetWidth(0.1f, 0.1f);
        _archerLineRenderer.enabled = false;

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
        _botsData = GameObject.FindObjectOfType<LevelUp>().GetComponent<BotsData>();
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
        if (!_player || _playerHealth.Dead || !_archer || _archerHealth.Dead)
            return;

        Ray ray = new Ray(_archer.transform.position, _archer.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,1000))
        {
            if(hit.collider.tag == "Player")
            {
                _archerLineRenderer.SetPosition(0, transform.position);
                _archerLineRenderer.SetPosition(1, _player.transform.position);
            }
            else
            {
                _archerLineRenderer.SetPosition(0, transform.position);
                _archerLineRenderer.SetPosition(1, hit.point);
            }
        }
    }

    public override void Tactic()
    {
        if(!_moves)
        {
            _moves = true;
            Move();
        }

        if(!_attack && !_moving)
        {
            _attack = true;
            ShootAttack();
            StartCoroutine(WatingTimeAttack());
        }
    }

    public override void ShootAttack()
    {
        _archerAttack.Attack();
    }
    public override void Clash()
    {
        _archerAttack.OnTriggerEnter(_archerCollider);
    }

    public override void LevelUp()
    {
        _botsData.InvokeEventLevelUpBots();
    }

    public override void DropThings()
    {
        _archerDrop.InvokeEventScatterCoins();
        _archerDrop.InvokeEventScatterHealth();
    }

    private IEnumerator WatingTimeAttack()
    {
        yield return new WaitForSeconds(5);

        _moves = false;
        _attack = false;
    }

    public override void Move()
    { 
        if (_archerHealth.Dead || !_player || (_player && _playerHealth.Dead)
            || _player.transform.position == Vector3.zero)
            return;

        if (_archerNavMesh.isStopped)
            _archerNavMesh.isStopped = false;

        _archerAnim.SetBool("Move", true);
        _archerNavMesh.SetDestination(_player.transform.position);
        _timing = Time.time;
        _moving = true;
    }

    private void Stand()
    {
        if (Time.time < _timing + _timingMove || _archerHealth.Dead)
            return;

        _archerNavMesh.isStopped = true;
        _archerAnim.SetBool("Move", false);
        _moving = false;
    }

    public override void RotateToPlayer()
    {
        if (!_player || _playerHealth.Dead || _archerAttack.ReloadingAttack
            || _moving || _archerHealth.Dead)
            return;

        Vector3 direction = _player.transform.position - _archer.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _archer.transform.rotation = Quaternion.Lerp(_archer.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
