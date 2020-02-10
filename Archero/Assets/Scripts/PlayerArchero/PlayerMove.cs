using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speedMove = 8;
    public float SpeedMove { get { return speedMove; } set { speedMove = value; } }

    private GameObject _player;
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;
    private Vector3 moveVector;
    private MobileController _instance;

    private void Start()
    {
        _player = gameObject;
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _instance = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (_player.GetComponent<HealthHelper>().Dead)
            return;

        moveVector = Vector3.zero;
        moveVector.x = _instance.Horizontal()*speedMove;
        moveVector.z = _instance.Vertical()*speedMove;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            _anim.SetBool("Move", true);
            _anim.SetBool("Damage", false);
        }
        else
        {
            _anim.SetBool("Move", false);
        }
        if(Vector3.Angle(Vector3.forward,moveVector)>1f || Vector3.Angle(Vector3.forward,moveVector)==0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 00);
            transform.rotation = Quaternion.LookRotation(direction); 
        }

        if(moveVector.x != 0 && moveVector.z != 0)
        {
            moveVector = new Vector3(moveVector.x, 0, moveVector.z);
            _navMeshAgent.Move(moveVector*Time.deltaTime/1.3f);
        }
        else
        {
            _navMeshAgent.Move(moveVector * Time.deltaTime);
        }
    }
}
