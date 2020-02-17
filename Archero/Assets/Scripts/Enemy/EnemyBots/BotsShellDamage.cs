using UnityEngine;

public class BotsShellDamage : MonoBehaviour
{
    private LevelUp _levelUp;
    private float _damageAttack;

    private void Start()
    {
        _levelUp = FindObjectOfType<LevelUp>();
        _damageAttack = _levelUp.GetComponent<BotsData>().Damage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_damageAttack);
            Destroy(gameObject);
        }
    }
}
