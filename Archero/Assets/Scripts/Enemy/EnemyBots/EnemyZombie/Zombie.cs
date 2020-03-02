using UnityEngine;

public class Zombie : Enemy
{
    private GameObject _player;
    private HealthHelper _playerHealth;
    [SerializeField] private GameObject _zombie;
    [SerializeField] private Animator _zombieAnim;
    [SerializeField] private ZombieAttack _zombieAttack;
    [SerializeField] private HealthHelper _zombieHealth;
    [SerializeField] private Rigidbody _zombieRigBody;
    [SerializeField] private Collision _zombieCollision;
    [SerializeField] private CapsuleCollider _zombieCapsuleCollider;
    [SerializeField] private Drop _zombieDrop;

    private BotsData _botsData;

    private float _timing;
    private float _times;

    private bool _isGround;
    private bool _reloadingJump = true;
    private bool _reloadingAttack = true;

    [Header ("DescriptionMove")]
    [SerializeField] private float forceJump;
    [SerializeField] private float forceMove;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
        _botsData = GameObject.FindObjectOfType<LevelUp>().GetComponent<BotsData>();
    }

    private void Update()
    {
        IsGrounding();
        Move();
        Tactic();
    }

    public override void Tactic()
    {
        if (!_zombie || _zombieHealth.Dead || !_player || _playerHealth.Dead)
            return;

        if(_isGround && !_reloadingAttack)
        {
            _reloadingAttack = true;
            ShootAttack();
        }

        if (_isGround && !_reloadingJump)
        {
            Jump();
        }
        else if(_reloadingJump)
        {
            if (Time.time < _times)
                return;

            _reloadingJump = false;
            _times = Time.time + 8;
        }
    }

    public override void ShootAttack()
    {
        _zombieAttack.Attack();
    }

    public override void Clash()
    {
        _zombieAttack.OnCollisionEnter(_zombieCollision);
    }

    public override void LevelUp()
    {
        _botsData.InvokeEventLevelUpBots();
    }

    public override void DropThings()
    {
        _zombieDrop.InvokeEventScatterCoins();
        _zombieDrop.InvokeEventScatterHealth();
    }

    public override void Move()
    {
        if(!_isGround)
        { 
            _zombie.transform.Translate(Vector3.forward * Time.deltaTime * forceMove);
        }
    }

    private void Jump()
    {
        if (!_zombie || _zombieHealth.Dead || !_player || _playerHealth.Dead)
            return;

        _reloadingJump = true;

      if(_isGround)
      {
            _zombieAnim.SetTrigger("Jump");
            _zombieRigBody.AddForce(_zombie.transform.up * forceJump);
      }
    }

    private void IsGrounding()
    {
        Ray ray = new Ray(transform.position + _zombieCapsuleCollider.center, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1.1f))
        {
            if (hit.collider.tag == "Ground")
            {
                _isGround = true;
                RotateToPlayer();
            }
        }
        else
        {
            _isGround = false;
            _reloadingAttack = false;
        }
    }

    public override void RotateToPlayer()
    {
        if (_zombieHealth.Dead || !_player || _playerHealth.Dead)
            return;

        Vector3 direction = _player.transform.position - _zombie.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _zombie.transform.rotation = Quaternion.Lerp(_zombie.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
