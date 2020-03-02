using UnityEngine;
using UnityEngine.AI;

public delegate void ReproductionSounds();

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private HealthHelper _playerHealth;
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private NavMeshAgent _playerNavMesh;
    [SerializeField] private PlayerSounds _playerSounds;
    private event ReproductionSounds _soundWalkStart;
    private event ReproductionSounds _soundWalkStop;
    private event ReproductionSounds _soundShoot;

    [Header("DescriptionMove")]
    [SerializeField] private float _speedMove = 8;
    public float SpeedMove { get { return _speedMove; } set { _speedMove = value; } }

    private MobileController _mobileController;
    private Vector3 _moveVector;

    private void Start()
    {
        _mobileController = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
        _soundWalkStart += _playerSounds.WalkStart;
        _soundWalkStop += _playerSounds.WalkStop;
        _soundShoot += _playerSounds.ShootStart;
    }

    private void InvokeEventWalkStart()
    {
        _soundWalkStart.Invoke();
    }

    private void InvokeEventWalkStop()
    {
        _soundWalkStop.Invoke();
    }

    public void InvokeEventShoot()
    {
        _soundShoot.Invoke();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (_playerHealth.Dead)
            return;

        _moveVector = Vector3.zero;
        _moveVector.x = _mobileController.Horizontal() * _speedMove;
        _moveVector.z = _mobileController.Vertical() * _speedMove;

        if (_moveVector.x != 0 || _moveVector.z != 0)
        {
            _playerAnim.SetBool("Move", true);
            _playerAnim.SetBool("Damage", false);
            InvokeEventWalkStart();
        }
        else
        {
            _playerAnim.SetBool("Move", false);
            InvokeEventWalkStop();
        }

        if(Vector3.Angle(Vector3.forward, _moveVector)>1f || Vector3.Angle(Vector3.forward, _moveVector)==0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _speedMove, 00);
            transform.rotation = Quaternion.LookRotation(direction); 
        }

        if(_moveVector.x != 0 && _moveVector.z != 0)
        {
            _moveVector = new Vector3(_moveVector.x, 0, _moveVector.z);
            _playerNavMesh.Move(_moveVector * Time.deltaTime/1.3f);
        }
        else
        {
            _playerNavMesh.Move(_moveVector * Time.deltaTime);
        }
    }
}
