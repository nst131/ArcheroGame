using UnityEngine;
using System.Collections;

public class BossRoundDamage : MonoBehaviour
{
    private ParticleSystem _roundDamage;
    private SphereCollider _sphereColliderAttack;
    private GameManager _eachData;
    private float roundDamageAttack;

    private void Start()
    {
        _roundDamage = GetComponent<ParticleSystem>();
        _roundDamage.Stop();
        _sphereColliderAttack = GetComponent<SphereCollider>();
        _sphereColliderAttack.enabled = false;
        _eachData = GameObject.FindObjectOfType<GameManager>();
        roundDamageAttack = _eachData.GetComponent<EnemyBossData>().RoundDamage;
    }

    public void StartRoundDamage()
    {
        _roundDamage.Play();
        _sphereColliderAttack.enabled = true;
        StartCoroutine(StopRoundDamage());
    }

    private IEnumerator StopRoundDamage()
    {
        yield return new WaitForSeconds(6);

        _roundDamage.Stop();
        _sphereColliderAttack.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(roundDamageAttack);
        }
    }
}
