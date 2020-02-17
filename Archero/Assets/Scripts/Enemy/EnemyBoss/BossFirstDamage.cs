using UnityEngine;

public class BossFirstDamage : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleFirstDamage;
    private GameObject _firstDamage;
    private LevelUp _levelUp;
    private float _firstDamageAttack;

    private void Start()
    {
        _particleFirstDamage.Play();
        _firstDamage = gameObject;
        _levelUp = GameObject.FindObjectOfType<LevelUp>();
        _firstDamageAttack = _levelUp.GetComponent<BossData>().FirstDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            Instantiate<GameObject>(Resources.Load<GameObject>("BossShell/FireShell"),_firstDamage.transform.position,Quaternion.identity);
            Destroy(_firstDamage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_firstDamageAttack);
            Destroy(_firstDamage);
        }
    }
}
