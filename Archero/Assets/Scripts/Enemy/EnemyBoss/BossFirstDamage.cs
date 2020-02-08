using UnityEngine;

public class BossFirstDamage : MonoBehaviour
{
    private ParticleSystem _particleFirstDamage;
    private GameObject _firstDamage;
    private GameManager _eachData;
    private float firstDamageAttack;

    private void Start()
    {
        _firstDamage = gameObject;
        _particleFirstDamage = GetComponentInChildren<ParticleSystem>();
        _particleFirstDamage.Play();
        _eachData = GameObject.FindObjectOfType<GameManager>();
        firstDamageAttack = _eachData.GetComponent<EnemyBossData>().FirstDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            Instantiate<GameObject>(Resources.Load<GameObject>("BossFireShell"),_firstDamage.transform.position,Quaternion.identity);
            Destroy(_firstDamage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(firstDamageAttack);
            Destroy(_firstDamage);
        }
    }
}
