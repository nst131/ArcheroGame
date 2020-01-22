using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    public float SpeedMove;

    private Transform Point1;
    private Transform Point2;
    private NavMeshAgent _NavMeshAgent;
    private Animator _animator;
    private Animator _animatorGate;
    private Vector3 moveVector;
    private HealthHelper _healthHelper;
    MobileController instance;


    void Start()
    {
        //Point1 = GameObject.Find("Ground").transform.GetChild(0).GetComponent<Transform>();
        //Point2= GameObject.Find("Ground").transform.GetChild(1).GetComponent<Transform>();

        _healthHelper = GetComponent<HealthHelper>();
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        //_animatorGate = GameObject.Find("Ground").transform.GetChild(2).GetComponent<Animator>();
        _animator = GetComponent<Animator>();
        instance = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (_healthHelper.Dead)
            return;

        moveVector = Vector3.zero;
        // moveVector.x = Input.GetAxis("Horizontal")*SpeedMove;
        // moveVector.z = Input.GetAxis("Vertical")*SpeedMove;
        moveVector.x = instance.Horizontal()*SpeedMove;
        moveVector.z = instance.Vertical()*SpeedMove;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            _animator.SetBool("Move", true);
            _animator.SetBool("Damage", false);
        }
        else
        {
            _animator.SetBool("Move", false);
        }
        if(Vector3.Angle(Vector3.forward,moveVector)>1f || Vector3.Angle(Vector3.forward,moveVector)==0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, SpeedMove, 00);
            transform.rotation = Quaternion.LookRotation(direction); 
        }

        //_characterController.Move(moveVector*Time.deltaTime);

        if(moveVector.x != 0 && moveVector.z != 0)
        {
            moveVector = new Vector3(moveVector.x, 0, moveVector.z);
            _NavMeshAgent.Move(moveVector*Time.deltaTime/1.3f);
        }
        else
        {
            _NavMeshAgent.Move(moveVector * Time.deltaTime);
        }

        //if (Vector3.Distance(transform.position, Point1.position) <= 0.5f && _animatorGate.GetBool("Open"))
        //{
        //    _NavMeshAgent.SetDestination(Point2.position);
        //}
    }
}
