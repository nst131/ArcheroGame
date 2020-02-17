using UnityEngine;

public class PlayerShellDamage : MonoBehaviour
{
    private GameObject _player;
    private float _damageAttack;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _damageAttack = _player.GetComponent<PlayerData>().Damage;
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.GetComponent<HealthHelper>() && other.GetComponent<HealthHelper>().Dead)
            return;

        if(other.tag=="Enemy" && other.name != "RoundDamage")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_damageAttack);
            Destroy(gameObject);
        }
    }
}
