using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyZombieMove : MonoBehaviour
{
    private GameObject _zombie;
    private Animator _anim;
    private NavMeshAgent _agent;

    [SerializeField] private float ForceJump;
    [SerializeField] private float ForceMove;
    [SerializeField] private bool IsGround;
    private bool WantJump;

    void Start()
    {
        _zombie = gameObject;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        DrowHitLine();
        IsGrounding();

        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        Move();
    }

    private void DrowHitLine()
    {
        Debug.DrawRay(transform.position+GetComponent<CapsuleCollider>().center, Vector3.down*1.1f, Color.red);
    }

    private void Move()
    {
        if(!IsGround)
        {
            _zombie.transform.Translate(_zombie.transform.forward*Time.deltaTime*ForceMove);
        }
    }

    private void Jump()
    {
      if(!_zombie.GetComponent<Rigidbody>())
      {
          _zombie.AddComponent<Rigidbody>();
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
        }
        else
        {
            IsGround = false;
        }
    }
}
