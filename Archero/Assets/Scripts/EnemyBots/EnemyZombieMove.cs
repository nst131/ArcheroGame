using UnityEngine;

public class EnemyZombieMove : MonoBehaviour
{
    private GameObject _zombie;
    private GameObject _player;
    private Animator _anim;
    private EnemyZombieAttack _zombieAttack;

    private float timing;
    private float times;

    private bool isGround;
    private bool reloadingJump = true;
    private bool reloadingAttack = true;

    [SerializeField] private float forceJump;
    [SerializeField] private float forceMove;

    void Start()
    {
        _zombieAttack = GetComponent<EnemyZombieAttack>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _zombie = gameObject;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounding();
        Move();
        IgnorePlayer();
        Tactic();
    }

    private void Tactic()
    {
        if (_zombie.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        if(isGround && !reloadingAttack)
        {
            reloadingAttack = true;
            _zombieAttack.ShootAttack();
        }

        if (isGround && !reloadingJump)
        {
            Jump();
        }
        else if(reloadingJump)
        {
            if (Time.time < times)
                return;

            reloadingJump = false;
            times = Time.time + 8;
        }
    }

    private void Move()
    {
        if(!isGround)
        {
            _zombie.transform.Translate(Vector3.forward*Time.deltaTime*forceMove);
        }
    }

    private void Jump()
    {
        reloadingJump = true;

      if(!_zombie.GetComponent<Rigidbody>())
      {
          _zombie.AddComponent<Rigidbody>();
      }

      if(isGround)
      {
            _anim.SetTrigger("Jump");
            _zombie.GetComponent<Rigidbody>().AddForce(_zombie.transform.up * forceJump);
      }
    }

    private void isGrounding()
    {
        Ray ray = new Ray(transform.position+GetComponent<CapsuleCollider>().center, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1.1f))
        {
            if (hit.collider.tag == "Ground")
            {
                isGround = true;
                RotateToPlayer();
            }
        }
        else
        {
            isGround = false;
            reloadingAttack = false;
        }
    }

    private void IgnorePlayer()
    {
        Physics.IgnoreLayerCollision(8, 9);
    }

    private void RotateToPlayer()
    {
        if (_zombie.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _player.transform.position - _zombie.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _zombie.transform.rotation = Quaternion.Lerp(_zombie.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
