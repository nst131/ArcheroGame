using UnityEngine;

public class GetHealth : MonoBehaviour
{
    private GameObject _bottleHealth;
    private GameObject _player;

    [Header("Characteristics")]
    [SerializeField] private float _health = 50;

    private void Start()
    {
        _bottleHealth = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        BootleHealthMove();  
    }

    private void BootleHealthMove()
    {
        if (!_bottleHealth || !_player)
            return;

        _bottleHealth.transform.position = Vector3.MoveTowards(_bottleHealth.transform.position, _player.transform.position, Time.deltaTime * 20);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<HealthHelper>().Hp += _health;
            other.GetComponent<HealthHelper>().TextHp += _health;
            Destroy(_bottleHealth);
        }
    }
}
