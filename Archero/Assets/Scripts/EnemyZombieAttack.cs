using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombieAttack : MonoBehaviour
{
    private EnemyZombieMove _enemyZombie;

    [SerializeField] private float Shoot;

    void Start()
    {
        _enemyZombie = GetComponent<EnemyZombieMove>();
    }

    void Update()
    {
    }

    public void ShootAttack()
    {
        GameObject poison1 = Instantiate<GameObject>(Resources.Load<GameObject>("Poison"), transform.GetChild(15).position,
          Quaternion.LookRotation(transform.GetChild(15).position - transform.position));
        poison1.transform.Rotate(40, 0, 0);
        Destroy(poison1.gameObject, 3);

        GameObject poison2 = Instantiate<GameObject>(Resources.Load<GameObject>("Poison"), transform.GetChild(16).position,
            Quaternion.LookRotation(transform.GetChild(16).position - transform.position));
        poison2.transform.Rotate(40, 0, 0);
        Destroy(poison2.gameObject, 3);
        GameObject poison3 = Instantiate<GameObject>(Resources.Load<GameObject>("Poison"), transform.GetChild(17).position,
            Quaternion.LookRotation(transform.GetChild(17).position - transform.position));
        poison3.transform.Rotate(40, 0, 0);
        Destroy(poison3.gameObject, 3);
        GameObject poison4 = Instantiate<GameObject>(Resources.Load<GameObject>("Poison"), transform.GetChild(18).position,
            Quaternion.LookRotation(transform.GetChild(18).position - transform.position));
        poison4.transform.Rotate(40, 0, 0);
        Destroy(poison4.gameObject, 3);

        poison1.AddComponent<Rigidbody>().AddForce(poison1.transform.forward * Shoot);  //Кидаем с помощью RigidBody
        poison2.AddComponent<Rigidbody>().AddForce(poison2.transform.forward * Shoot);
        poison3.AddComponent<Rigidbody>().AddForce(poison3.transform.forward * Shoot);
        poison4.AddComponent<Rigidbody>().AddForce(poison4.transform.forward * Shoot);

        _enemyZombie.Reloading = true;
    }
}
