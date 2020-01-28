using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 10;
    private GameObject _camera;
    private GameObject _player;
    private Vector3 _different;

    private void Start()
    {
        _camera = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _different = transform.position - _player.transform.position;
    }

    private void Update()
    {
        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _player.transform.position + _different, SpeedMove * Time.deltaTime);
    }
}
