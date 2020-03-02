using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private Collider _zombieCollider;
    [SerializeField] private GameObject _zombieShell;

    [Header ("DescriptionAttack")]
    [SerializeField] private float _forceShoot = 500f;
    [SerializeField] private float _forceClash = 20f;

    public void Attack()
    {
        int g = 14;
        for (int i = 0; i < 4; i++)
        {
            g += 1;
            GameObject poison = Instantiate<GameObject>(_zombieShell, transform.GetChild(g).position,
              Quaternion.LookRotation(transform.GetChild(g).position - transform.position));
            poison.transform.Rotate(40, 0, 0);
            Destroy(poison.gameObject, 3);
   
            poison.GetComponent<Rigidbody>().AddForce(poison.transform.forward * _forceShoot);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<HealthHelper>().TakeAwayHP(_forceClash);
        }
    }
}
