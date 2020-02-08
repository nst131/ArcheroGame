using UnityEngine;

public class BossSecondDamage : MonoBehaviour
{
    private GameManager _eachData;
    private float secondDamageAttack;

    private void Start()
    {
        _eachData = GameObject.FindObjectOfType<GameManager>();
        secondDamageAttack = _eachData.GetComponent<EnemyBossData>().SecondDamage;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(secondDamageAttack);
            Destroy(gameObject);
        }
    }
}
