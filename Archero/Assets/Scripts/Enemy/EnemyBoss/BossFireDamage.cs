using UnityEngine;
using System.Collections;

public class BossFireDamage : MonoBehaviour
{
    [SerializeField] private ParticleSystem _firstFireDamage;
    private LevelUp _levelUp;
    private float _fireDamageAttack;

    private void Start()
    {
        StartCoroutine(StopFireDamage());
        _levelUp = GameObject.FindObjectOfType<LevelUp>();
        _fireDamageAttack = _levelUp.GetComponent<BossData>().FireDamage;
    }

    private IEnumerator StopFireDamage()
    {
        yield return new WaitForSeconds(3);

        Destroy(_firstFireDamage.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_fireDamageAttack);
        }
    }
}
