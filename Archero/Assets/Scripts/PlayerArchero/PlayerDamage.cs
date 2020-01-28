using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float DamageArrow = 30;

    void OnTriggerStay (Collider other)
    {
        if(other.tag=="Enemy")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(DamageArrow);
            Destroy(gameObject);
        }
    }
}
