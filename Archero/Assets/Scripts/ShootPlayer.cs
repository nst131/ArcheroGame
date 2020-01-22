using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootPlayer : MonoBehaviour
{
    private Animator _animator;
    private GameObject Gamer;
    private bool _Rotate = false;
    private bool _Stay = false;
    private Vector3 CurrentPos;
    private GameObject _Enemy;
    private HealthHelper [] Enemies;
    private bool _Reloading = false;
    private int _CountArrows = 0;


    public float _Wate = 1.3f;
    public float _ExplainReloading = 1.3f;
    public float Shoot;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Gamer = GameObject.FindGameObjectWithTag("Player").gameObject;
        CurrentPos = Gamer.transform.position;
    }

    void Update()
    {
        ChooseEnemy();
        
        StayOrNo();
        
        //Rotate(_animator.GetBool("Damage"));  //Метод для разворота Персонажа Под Анимацию !!!
        
        FightOrNo();
        
        //Debug.DrawRay(transform.position,transform.forward*1000, Color.red);  //Линия просмотра Атаки !!!
    }

    void ChooseEnemy()
    {
         float MinInterval = 0;
         int IndexEnemy = 0;
         Enemies = GameObject.FindObjectsOfType<HealthHelper>().Where<HealthHelper>(p => !p.Dead && p.gameObject.tag=="Enemy").ToArray();
        
         for (int i = 0; i < Enemies.Length; i++)
         {
             if (Vector3.Distance(Gamer.transform.position,Enemies[i].transform.position)<=MinInterval)
             {
                 MinInterval = Vector3.Distance(Gamer.transform.position, Enemies[i].transform.position);
                 IndexEnemy = i;
             }
         }
         if(Enemies.Length>0)
         {
             _Enemy = Enemies[IndexEnemy].gameObject;
         }
   
    }

    void StayOrNo()
    {
        if (CurrentPos == Gamer.transform.position)
        {
            if (Enemies.Length == 0)
                return;

            _Stay = true;
            RotatetoTarget(_Enemy);
        }
        else
        {
            CurrentPos = Gamer.transform.position;
            _Stay = false;
            _CountArrows = 0;
            _Reloading = false;
        }
    }

    void FightOrNo()
    {
        if (_Stay && _Enemy && !_Enemy.GetComponent<HealthHelper>().Dead && !_Reloading)
        {
            Damage();
        }
        else if(!_Enemy || _Enemy.GetComponent<HealthHelper>().Dead)
        {
            _animator.SetBool("Damage", false);
        }
    }

    void Damage()
    {
        _animator.SetBool("Damage",true);
        
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        _Reloading = true;

        if (_CountArrows==0)
        {
            yield return new WaitForSeconds(_Wate);
        }
        else
        {
            yield return new WaitForSeconds(_ExplainReloading);
        }

        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out hit, 1000))
        {
           if (hit.collider.tag == "Enemy")
           {
                GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("Arrow"), transform.position,
                    Quaternion.Euler(-7,0,0));
                arrow.transform.LookAt(_Enemy.transform);
                arrow.transform.Rotate(-7, 0, 0);
          
                arrow.AddComponent<Rigidbody>().AddForce(arrow.transform.forward * Shoot);
                Destroy(arrow.gameObject, 3);
                _CountArrows++;
                _Reloading = false;
           }
        }
    }

   // Для Разворачивания Персонажа , Анимация Смещена !!! 

   // void Rotate(bool _Damage)
   // {
   //
   //     if(_Damage && !_Rotate)
   //     {
   //         Gamer.transform.Rotate(new Vector3(0, 64, 0));
   //         _Rotate = true;
   //     }
   //     else if(!_Damage && _Rotate)
   //     { 
   //         Gamer.transform.Rotate(new Vector3(0, -64, 0));
   //         _Rotate = false;
   //     }
   // }

    void RotatetoTarget(GameObject Enemy)
    {
        Vector3 direction = Enemy.transform.position - Gamer.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        Gamer.transform.rotation = Quaternion.Lerp(Gamer.transform.rotation, lookrotation, Time.deltaTime*5);
    }   
}
