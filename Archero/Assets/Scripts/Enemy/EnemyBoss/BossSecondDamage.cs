using UnityEngine;

public class BossSecondDamage : MonoBehaviour
{
    private LevelUp _levelUp;
    private float _secondDamageAttack;

    private void Start()
    {
        _levelUp = GameObject.FindObjectOfType<LevelUp>();
        _secondDamageAttack = _levelUp.GetComponent<BossData>().SecondDamage;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_secondDamageAttack);
            Destroy(gameObject);
        }
    }
}
