using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [Header ("DescriptionAttack")]
    [SerializeField] private float _forceShoot = 500;

    public void Attack()
    {
        int g = 14;
        for (int i = 0; i < 4; i++)
        {
            g += 1;
            GameObject poison = Instantiate<GameObject>(Resources.Load<GameObject>("BotsShell/ZombieShell"), transform.GetChild(g).position,
              Quaternion.LookRotation(transform.GetChild(g).position - transform.position));
            poison.transform.Rotate(40, 0, 0);
            Destroy(poison.gameObject, 3);
   
            poison.GetComponent<Rigidbody>().AddForce(poison.transform.forward * _forceShoot);
        }
    }
}
