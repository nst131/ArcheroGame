using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private GameManager _eachData;
    private float damageAttack;

    private void Start()
    {
        _eachData = FindObjectOfType<GameManager>();
        damageAttack = _eachData.GetComponent<PlayerData>().Damage;
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.GetComponent<HealthHelper>() && other.GetComponent<HealthHelper>().Dead)
            return;

        if(other.tag=="Enemy" && other.name != "RoundDamage")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(damageAttack);
            Destroy(gameObject);
        }
    }
}
