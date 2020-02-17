using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthHelper : MonoBehaviour
{
    private UIHealthHelper _uIHealthHelper;
    private GameObject _slider;
    private LevelUp _levelUp;
    private PlayerData _playerData;
    private BotsData _botsData;
    private BossData _bossData;
    [SerializeField] private BossAttack _boss;
    [SerializeField] private Drop _drop;
    [SerializeField] private CapsuleCollider _targetCapsuleCollider;

    private float maxHp;
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    private float hp;
    public float Hp {get { if(hp > maxHp) hp=maxHp; return hp; } set { hp = value; } }
    private bool dead;
    public bool Dead { get { return dead; } set { dead = value; }}
    
    private void Start()
    {
        _levelUp = GameObject.FindObjectOfType<LevelUp>();
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        _botsData = _levelUp.GetComponent<BotsData>();
        _bossData = _levelUp.GetComponent<BossData>();

        InitializationHp();
        InitializationSlider();
    }

    public void SliderInstallActive(bool active)
    {
        _uIHealthHelper.GetComponent<Slider>().gameObject.SetActive(active);
    }

    private void InitializationSlider()
    {
        _slider = Instantiate<GameObject>(Resources.Load<GameObject>("UI/Slider"));
        _slider.transform.SetParent(GameObject.Find("Canvas").transform);
        _slider.tag = gameObject.tag;
        _uIHealthHelper = _slider.GetComponent<UIHealthHelper>();
        _uIHealthHelper.Target = this;
        _uIHealthHelper.TargetCapsuleCollider = _targetCapsuleCollider;
    }

    private void InitializationHp()
    {
        if (gameObject.tag == "Player")
        {
            maxHp = _playerData.MaxHp;
        }
        else if(gameObject.tag == "Enemy")
        {
            if(_boss)
            {
                maxHp = _bossData.MaxHp;
            }
            else
            {
                maxHp = _botsData.MaxHp;
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

            if(GetComponent<Animator>())
            {
                GetComponent<Animator>().SetBool("Dead", true);
            }

            Destroy(_slider);
            Destroy(gameObject, 3);

            if(gameObject.tag == "Enemy")
            {
                _drop.InvokeEventScatterCoins();
                _drop.InvokeEventScatterHealth();
            }
        }
        else
        {
            hp = newHp;
        }
    }
}
