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
    [SerializeField] private GameObject _sliderInsert;
    [SerializeField] private BossAttack _boss;
    [SerializeField] private Drop _drop;
    [SerializeField] private CapsuleCollider _capsuleCollider;

    private float _textHp;
    public float TextHp { get { if (_textHp > _maxHp) _textHp = _maxHp; return _textHp; } set { _textHp = value; } }
    private float _maxHp;
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    private float _hp;
    public float Hp {get { if(_hp > _maxHp) _hp = _maxHp; return _hp; } set { _hp = value; } }
    private bool _dead;
    public bool Dead { get { return _dead; } set { _dead = value; }}
    
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
        _slider = Instantiate<GameObject>(_sliderInsert);
        _slider.transform.SetParent(GameObject.Find("Canvas").transform);
        _slider.tag = gameObject.tag;
        _uIHealthHelper = _slider.GetComponent<UIHealthHelper>();
        _uIHealthHelper.Target = this;
        _uIHealthHelper.TargetCapsuleCollider = _capsuleCollider;
    }

    private void InitializationHp()
    {
        if (gameObject.tag == "Player")
        {
            
            _maxHp = _playerData.MaxHp;
            _textHp = _playerData.MaxHp;
            
        }
        else if(gameObject.tag == "Enemy")
        {
            if(_boss)
            {
                _maxHp = _bossData.MaxHp;
                _textHp = _bossData.MaxHp;
            }
            else
            {
                _maxHp = _botsData.MaxHp;
                _textHp = _botsData.MaxHp;
            }
        }
        _hp = _maxHp;
    }

    public void TakeAwayHP(float damage)
    {
        float newHp = _hp - damage;

        if(newHp<=0)
        {
            _hp = 0;
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

            if(gameObject.tag == "Enemy")
            {
                Destroy(_slider);
                Destroy(gameObject, 3);
                _drop.InvokeEventScatterCoins();
                _drop.InvokeEventScatterHealth();
                _drop.InvokeEventScatterbox();
            }
            else
            {
                _playerData.InvokeEventPlayerDead();
            }
        }
        else
        {
            _hp = newHp;
            _textHp = newHp;
        }
    }
}
