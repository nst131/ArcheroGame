using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthHelper : MonoBehaviour
{
    public float MaxHp = 100;
    public float Hp;

    private bool _dead = false;

    public bool Dead
    {
        get { return _dead; }
        set { _dead = value; }
    }

    
    void Start()
    {
        Hp = MaxHp;
        GameObject _Slider = Instantiate<GameObject>(Resources.Load<GameObject>("Slider"));
        _Slider.transform.SetParent(GameObject.Find("Canvas").transform);
        _Slider.tag = gameObject.tag;
        _Slider.GetComponent<UIHealthHelper>().Target = this;
    }

    void Update()
    {
        
    }

  public void TakeAwayHP(float Damage)
    {
        float newHP = Hp - Damage;

        if(newHP<=0)
        {
            Hp = 0;
            Dead = true;

            if (GetComponent<Collider>())
                GetComponent<Collider>().isTrigger = true;
            if (GetComponent<NavMeshAgent>())
                GetComponent<NavMeshAgent>().enabled = false;

            GetComponent<Animator>().SetBool("Dead", true);
        }
        else
        {
            Hp = newHP;
        }
    }
}
