using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 10;
    private GameObject _camera;
    private GameObject _player;

    private void Start()
    {
        _camera = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Move();
    }  

    private void Move()
    {
        if (!_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        float z = 0;
        if(_player.transform.position.z >= 35)
        {
            z = 35;
        }
        else if(_player.transform.position.z < 35 && _player.transform.position.z > 17)
        {
            z = _player.transform.position.z;
        }
        else
        {
            z = 17;
        }
        Vector3 posPlayer = new Vector3(_camera.transform.position.x, _camera.transform.position.y, z + 8);

        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, posPlayer, SpeedMove * Time.deltaTime);
    }
}
