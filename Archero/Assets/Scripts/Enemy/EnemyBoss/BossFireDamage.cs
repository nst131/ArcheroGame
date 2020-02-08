using UnityEngine;
using System.Collections;

public class BossFireDamage : MonoBehaviour
{
    private ParticleSystem _firstFireDamage;
    private GameManager _eachData;
    private float fireDamageAttack;

    private void Start()
    {
        _firstFireDamage = GetComponent<ParticleSystem>();
        StartCoroutine(StopFireDamage());
        _eachData = GameObject.FindObjectOfType<GameManager>();
        fireDamageAttack = _eachData.GetComponent<EnemyBossData>().FireDamage;
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
            other.GetComponent<HealthHelper>().TakeAwayHP(fireDamageAttack);
        }
    }
}
