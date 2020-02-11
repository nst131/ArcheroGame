using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private GameObject _player;
    private Animator _animPlayer;
    private HealthHelper _healtPlayer;
    private PlayerAttack _attackPlayer;

    [SerializeField] private float maxHp = 100;
    public float MaxHp { get { return maxHp; } }
    [SerializeField] private float damage = 100;
    public float Damage { get { return damage; } }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animPlayer = _player.GetComponent<Animator>();
        _healtPlayer = _player.GetComponent<HealthHelper>();
        _attackPlayer = _player.GetComponent<PlayerAttack>();
    }

    private void Health(float Hp)
    {
        maxHp += Hp;
        _healtPlayer.MaxHp += Hp;
        _healtPlayer.Hp += Hp;
    }

    private void Speed(float Rate ,float MinusFisrtTime ,float MinusSecondTime)
    {
        _animPlayer.SetFloat("Speed", _animPlayer.GetFloat("Speed") + Rate);
        _attackPlayer.waitingAnimFirstAttack -= MinusFisrtTime;
        _attackPlayer.waitingAnimSecondAttack -= MinusSecondTime;
    }

    private void Attack(float Damage)
    {
        damage += Damage;
    }

    public void SumSkill(string NameMethod)
    {
        switch (NameMethod)
        {
            case "Health": Health(300);
                break;
            case "Speed":
                Speed(1.0f,0.05f,0.05f);
                break;
            case "Attack":
                Attack(20);
                break;
        }
    }
}
