using UnityEngine;

public class EnemyArcherDamage : MonoBehaviour
{
    [SerializeField] private float DamageArrow = 15;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(DamageArrow);
            Destroy(gameObject);
        }
    }
}
