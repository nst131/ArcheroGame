using UnityEngine;

public class EnemyGolemDamage : MonoBehaviour
{
    [SerializeField] private float DamageArrow = 20;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(DamageArrow);
            Destroy(gameObject);
        }
    }
}
