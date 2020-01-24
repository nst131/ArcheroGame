using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyZombieMove : MonoBehaviour
{
    private GameObject _zombie;
    private GameObject _player;
    private Animator _anim;
    private NavMeshAgent _agent;
    private EnemyZombieAttack _zombieAttack;

    private float timing;
    private float times;

    [SerializeField] private float ForceJump;
    [SerializeField] private float ForceMove;
    [SerializeField] private bool IsGround;
    public bool Reloading = true;
    public bool ReloadingJump = true;

    void Start()
    {
        _zombieAttack = GetComponent<EnemyZombieAttack>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _zombie = gameObject;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        DrowHitLine();
        IsGrounding();
        Move();
        IgnorePlayer();

        Tactic();
    }

    private void Tactic()
    {
        if (GetComponent<HealthHelper>().Dead)
            return;

        if(IsGround && !Reloading)
        {
            _zombieAttack.ShootAttack();
        }

        if (IsGround && !ReloadingJump)
        {
            Jump();
        }
        else if(ReloadingJump)
        {
            if (Time.time < times)
                return;

            ReloadingJump = false;
            times = Time.time + 8;
        }
    }

    private void DrowHitLine()
    {
        Debug.DrawRay(transform.position+GetComponent<CapsuleCollider>().center, Vector3.down*1.1f, Color.red);
    }

    private void Move()
    {
        if(!IsGround)
        {
            _zombie.transform.Translate(Vector3.forward*Time.deltaTime*ForceMove);
        }
    }

    private void Jump()
    {
        ReloadingJump = true;

      if(!_zombie.GetComponent<Rigidbody>())
      {
          _zombie.AddComponent<Rigidbody>();
      }
      if(_agent.enabled)
      {
            _agent.enabled = false;
      }
      if(IsGround)
      {
            _anim.SetTrigger("Jump");
            _zombie.GetComponent<Rigidbody>().AddForce(_zombie.transform.up * ForceJump);
      }
    }

    private void IsGrounding()
    {
        Ray ray = new Ray(transform.position+GetComponent<CapsuleCollider>().center, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1.1f))
        {
            IsGround = true;
            RotatetoTarget();

            if (Time.time < timing || !IsGround)
                return;
            
            _agent.enabled = true;
            timing = Time.time + 1;
        }
        else
        {
            //Fly();
            IsGround = false;
            Reloading = false;
        }
    }

    //private void Fly()
    //{
    //    _zombie.transform.rotation = Quaternion.Euler(0, _zombie.transform.rotation.y, 0); Нельзя сбить в воздухе
    //}

    private void IgnorePlayer()
    {
        Physics.IgnoreLayerCollision(8, 9);
    }

    private void RotatetoTarget()
    {
        if (GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _player.transform.position - _zombie.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _zombie.transform.rotation = Quaternion.Lerp(_zombie.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
