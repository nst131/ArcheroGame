using UnityEngine;
using System.Collections;

public class BossRoundDamage : MonoBehaviour
{
    [SerializeField] private ParticleSystem _roundDamage;
    [SerializeField] private SphereCollider _sphereColliderAttack;
    private LevelUp _levelUp;
    private float _roundDamageAttack;

    private void Start()
    {
        _roundDamage.Stop();
        _sphereColliderAttack.enabled = false;
        _levelUp = GameObject.FindObjectOfType<LevelUp>();
        _roundDamageAttack = _levelUp.GetComponent<BossData>().RoundDamage;
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
            other.GetComponent<HealthHelper>().TakeAwayHP(_roundDamageAttack);
        }
    }
}
