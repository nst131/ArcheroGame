using UnityEngine;

public class EnemyZombieDamage : MonoBehaviour
{
    private GameManager _eachData;
    private float damageAttack;

    private void Start()
    {
        _eachData = FindObjectOfType<GameManager>();
        damageAttack = _eachData.GetComponent<EnemyBotsData>().Damage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(damageAttack);
            Destroy(gameObject);
        }
    }
}
