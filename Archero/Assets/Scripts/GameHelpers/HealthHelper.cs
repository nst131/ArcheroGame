using UnityEngine;
using UnityEngine.AI;

public class HealthHelper : MonoBehaviour
{
    [SerializeField] private float maxHp = 100;
    public float MaxHp { get { return maxHp; } }
    private float hp;
    public float Hp {get { return hp; } }
    private bool dead;
    public bool Dead
    {
        get { return dead; }
        set { dead = value; }
    }
    
    private void Start()
    {
        hp = maxHp;
        GameObject _Slider = Instantiate<GameObject>(Resources.Load<GameObject>("Slider"));
        _Slider.transform.SetParent(GameObject.Find("Canvas").transform);
        _Slider.tag = gameObject.tag;
        _Slider.GetComponent<UIHealthHelper>().Target = this;
    }

    public void TakeAwayHP(float Damage)
    {
        float newHp = hp - Damage;

        if(newHp<=0)
        {
            hp = 0;
            Dead = true;

            if (GetComponent<NavMeshAgent>() && GetComponent<Collider>())
            {
                GetComponent<Collider>().isTrigger = true;
                GetComponent<NavMeshAgent>().enabled = false;
            }

            GetComponent<Animator>().SetBool("Dead", true);
        }
        else
        {
            hp = newHp;
        }
    }
}
