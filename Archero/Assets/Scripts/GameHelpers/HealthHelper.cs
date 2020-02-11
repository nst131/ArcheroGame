using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthHelper : MonoBehaviour
{
    private UIHealthHelper _uIHealthHelper;
    private GameObject _slider;
    private GameManager _eachData;
    private AppearCoins _appearCoins;
    private AppearHealth _appearHealth;

    private float maxHp;
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    private float hp;
    public float Hp {get { if(hp > maxHp) hp=maxHp; return hp; } set { hp = value; } }
    private bool dead;
    public bool Dead { get { return dead; } set { dead = value; }}
    
    private void Start()
    {
        _eachData = GameObject.FindObjectOfType<GameManager>();
        _appearCoins = GetComponent<AppearCoins>();
        _appearHealth = GetComponent<AppearHealth>();
        InitializationHp();
        InitializationSlider();
    }

    public void SliderInstallActive(bool active)
    {
        _uIHealthHelper.GetComponent<Slider>().gameObject.SetActive(active);
    }

    private void InitializationSlider()
    {
        _slider = Instantiate<GameObject>(Resources.Load<GameObject>("Slider"));
        _slider.transform.SetParent(GameObject.Find("Canvas").transform);
        _slider.tag = gameObject.tag;
        _uIHealthHelper = _slider.GetComponent<UIHealthHelper>();
        _slider.GetComponent<UIHealthHelper>().Target = this;
    }

    private void InitializationHp()
    {
        if (gameObject.tag == "Player")
        {
            maxHp = _eachData.GetComponent<PlayerData>().MaxHp;
        }
        else if(gameObject.tag == "Enemy")
        {
            if(gameObject.GetComponent<BossAttack>())
            {
                maxHp = _eachData.GetComponent<EnemyBossData>().MaxHp;
            }
            else
            {
                maxHp = _eachData.GetComponent<EnemyBotsData>().MaxHp;
            }
        }
        hp = maxHp;
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
                GetComponent<Collider>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
            else
            {
                gameObject.AddComponent<NavMeshAgent>();
                GetComponent<Collider>().enabled = false;
            }
            if(GetComponent<LineRenderer>())
            {
                GetComponent<LineRenderer>().enabled = false;
            }

            GetComponent<Animator>().SetBool("Dead", true);
            Destroy(_slider);
            Destroy(gameObject, 3);

            if(gameObject.tag == "Enemy")
            {
                _appearCoins.InvokeEventScatterCoins();
                _appearHealth.InvokeEventScatterBottleHealth();
            }
        }
        else
        {
            hp = newHp;
        }
    }
}
