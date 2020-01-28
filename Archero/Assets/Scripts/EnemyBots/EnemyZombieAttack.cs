using UnityEngine;

public class EnemyZombieAttack : MonoBehaviour
{
    [SerializeField] private float forceShoot;

    public void ShootAttack()
    {
        int g = 14;
        for (int i = 0; i < 4; i++)
        {
            g += 1;
            GameObject poison = Instantiate<GameObject>(Resources.Load<GameObject>("EnemyZombieShell"), transform.GetChild(g).position,
              Quaternion.LookRotation(transform.GetChild(g).position - transform.position));
            poison.transform.Rotate(40, 0, 0);
            Destroy(poison.gameObject, 3);
   
            poison.GetComponent<Rigidbody>().AddForce(poison.transform.forward * forceShoot);
        }
    }
}
