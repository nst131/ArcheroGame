using UnityEngine;

public class EnemyZombieDamage : MonoBehaviour
{
    [SerializeField] private float DamagePoison = 20; 

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(DamagePoison);
            Destroy(gameObject);
        }
    }
}
