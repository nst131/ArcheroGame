using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyGiantAttack : MonoBehaviour
{ 
    private GameObject _Enemy;
    private Animator _animator;
    private GameObject _Player;
    private bool _Reloading = false;

    public float _Wate = 1.2f;
    public static float Shoot = 2000f;

    void Start()
    {
        _Enemy = GetComponent<EnemyGiantMove>().gameObject;
        _Player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //SearchRock();   //Бросок с помощью MoveTowards()
    }
   // void SearchRock()
   // {
   //     if (!GameObject.FindGameObjectWithTag("Rock"))
   //         return;
   //
   //    GameObject[] Rocks = GameObject.FindGameObjectsWithTag("Rock");
   //
   //    for (int i = 0; i < Rocks.Length; i++)
   //    {
   //        RockMove(Rocks[i]);
   //    }
   //    
   // }

    public void Damage()
    {
        if (!_Player || _Player.GetComponent<HealthHelper>().Dead)
            return;

        _animator.SetBool("Damage", true);

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        _Reloading = true;
        yield return new WaitForSeconds(_Wate);
        GameObject rock1 = Instantiate<GameObject>(Resources.Load<GameObject>("Rock"), transform.GetChild(2).position,
           Quaternion.LookRotation(transform.GetChild(2).position - transform.position));
        rock1.transform.Rotate(30, 0, 0);
        Destroy(rock1.gameObject, 3);

        GameObject rock2 = Instantiate<GameObject>(Resources.Load<GameObject>("Rock"), transform.GetChild(3).position,
            Quaternion.LookRotation(transform.GetChild(3).position - transform.position));
        rock2.transform.Rotate(30, 0, 0);
        Destroy(rock2.gameObject, 3);
        GameObject rock3 = Instantiate<GameObject>(Resources.Load<GameObject>("Rock"), transform.GetChild(4).position,
            Quaternion.LookRotation(transform.GetChild(4).position - transform.position));
        rock3.transform.Rotate(30, 0, 0);
        Destroy(rock3.gameObject, 3);

        rock1.AddComponent<Rigidbody>().AddForce(rock1.transform.forward * Shoot);  //Кидаем с помощью RigidBody
        rock2.AddComponent<Rigidbody>().AddForce(rock2.transform.forward * Shoot);
        rock3.AddComponent<Rigidbody>().AddForce(rock3.transform.forward * Shoot);

        _Reloading = false;
        _animator.SetBool("Damage", false);
    }
    public void RotatetoTarget()
    {
        if (_Reloading || _Enemy.GetComponent<EnemyGiantMove>().Moving)
            return;

        Vector3 direction = _Player.transform.position - _Enemy.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _Enemy.transform.rotation = Quaternion.Lerp(_Enemy.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
    //void RockMove(GameObject rock)
    //{
    //    rock.transform.position = Vector3.MoveTowards(rock.transform.position, rock.transform.forward, Time.deltaTime * Shoot);
    //}
}


