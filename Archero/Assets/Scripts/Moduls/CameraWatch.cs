using UnityEngine;

public class CameraWatch : MonoBehaviour
{
    [Header ("DescriptionMove")]
    [SerializeField] private float _cameraSpeed = 10;

    private GameObject _camera;
    private GameObject _player;
    private HealthHelper _playerHealth;

    private float _axisBounderiesMaxZ = 35;
    private float _axisBounderiesMinZ = 17;
    private float _cameraHeight = 8;

    private void Start()
    {
        _camera = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
    }

    private void Update()
    {
        Move();
    }  

    private void Move()
    {
        if (!_player || _playerHealth.Dead)
            return;

        float z = 0;
        if(_player.transform.position.z >= _axisBounderiesMaxZ)
        {
            z = _axisBounderiesMaxZ;
        }
        else if(_player.transform.position.z < _axisBounderiesMaxZ && _player.transform.position.z > _axisBounderiesMinZ)
        {
            z = _player.transform.position.z;
        }
        else
        {
            z = _axisBounderiesMinZ;
        }
        Vector3 posPlayer = new Vector3(_camera.transform.position.x, _camera.transform.position.y, z + _cameraHeight);

        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, posPlayer, _cameraSpeed * Time.deltaTime);
    }
}
