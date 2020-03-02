using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private HealthHelper _playerHealth;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private NavMeshAgent _playerNavMesh;
    [SerializeField] private Collider _playerCollider;
    [SerializeField] private PlayerSounds _playerSounds;
    private event Action _playerDead;
    private ArcheroMenu _archeroMenu;

    private float _maxHp;
    public float MaxHp { get { return _maxHp; } }
    private float _damage;
    public float Damage { get { return _damage; } }

    private float _hp = 50;
    private float _rate = 1.0f;
    private float _minusTime = 0.05f;
    private float _force = 20;

    private void Awake()
    {
        _maxHp = PlayerStats.Health;
        _damage = PlayerStats.Damage;
    }

    private void Start()
    {
        _archeroMenu = GameObject.FindObjectOfType<ArcheroMenu>();
        _playerDead += GamesEnd;
        _playerDead += _playerSounds.GameOver;
        _playerDead += _playerSounds.WalkStop;
    }

    public void Resurrect()
    {
        _playerHealth.Dead = false;
        _playerAnim.SetBool("Dead", false);
        _playerNavMesh.enabled = true;
        _playerCollider.enabled = true;
        _playerHealth.Hp = _playerHealth.MaxHp;
        _playerHealth.TextHp = _playerHealth.MaxHp;
        _archeroMenu.ButtonCountinue.SetActive(false);
        _archeroMenu.ImageCoin.SetActive(false);
    }

    private void GamesEnd()
    {
        _archeroMenu.PanelGameOver.SetActive(true);
    }

    public void InvokeEventPlayerDead()
    {
        _playerDead.Invoke();
    }

    public void Health(float Hp)
    {
        _maxHp += Hp;
        _playerHealth.MaxHp += Hp;
        _playerHealth.Hp += Hp;
        _playerHealth.TextHp += Hp;
    }

    private void Speed(float Rate ,float MinusTime)
    {
        _playerAnim.SetFloat("Speed", _playerAnim.GetFloat("Speed") + Rate);
        _playerAttack._waitingAnimFirstAttack -= MinusTime;
        _playerAttack._waitingAnimSecondAttack -= MinusTime;
    }

    private void Attack(float Damage)
    {
        _damage += Damage;
    }

    private void DoubleShoot()
    {
        _playerAttack.AmountArrow++;
    }

    public void SumSkill(SkillName name)
    {
        switch(name)
        {
            case SkillName.Health:
                Health(_hp);
                break;
            case SkillName.Speed:
                Speed(_rate, _minusTime);
                break;
            case SkillName.Attack:
                Attack(_force);
                break;
            case SkillName.DoubleShoot:
                DoubleShoot();
                break;
        }
    }
}

public enum SkillName
{
   Health,
   Speed,
   Attack,
   DoubleShoot
}

public static class PlayerStats
{
   public static float Damage;
   public static float Health;
}
