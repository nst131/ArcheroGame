using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private HealthHelper _playerHealth;
    [SerializeField] private PlayerAttack _playerAttack;
    private NameSkill[] _nameSkill;

    [Header ("Characteristics")]
    [SerializeField] private float _maxHp = 100;
    public float MaxHp { get { return _maxHp; } }
    [SerializeField] private float _damage = 100;
    public float Damage { get { return _damage; } }

    private void Start()
    {
        _nameSkill = Enum.GetValues(typeof(NameSkill)) as NameSkill[];
    }

    public void Health(float Hp)
    {
        _maxHp += Hp;
        _playerHealth.MaxHp += Hp;
        _playerHealth.Hp += Hp;
    }

    private void Speed(float Rate ,float MinusFisrtTime ,float MinusSecondTime)
    {
        _playerAnim.SetFloat("Speed", _playerAnim.GetFloat("Speed") + Rate);
        _playerAttack._waitingAnimFirstAttack -= MinusFisrtTime;
        _playerAttack._waitingAnimSecondAttack -= MinusSecondTime;
    }

    private void Attack(float Damage)
    {
        _damage += Damage;
    }

    //public void SumSkill(string NameMethod)
    //{
    //    switch (NameMethod)
    //    {
    //        case "Health": Health(300);
    //            break;
    //        case "Speed":
    //            Speed(1.0f,0.05f,0.05f);
    //            break;
    //        case "Attack":
    //            Attack(20);
    //            break;
    //    }
    //}

    public void SumSkill(string NameSkillMethod)
    {
        for (int i = 0; i < _nameSkill.Length; i++)
        {
            if(NameSkillMethod ==_nameSkill[i].ToString())
            {
                switch (NameSkillMethod)
                {
                    case "Health":
                        Health(300);
                        break;
                    case "Speed":
                        Speed(1.0f, 0.05f, 0.05f);
                        break;
                    case "Attack":
                        Attack(20);
                        break;
                }
            }
        }
    }
}

public enum NameSkill
{
   Health = 300,
   Speed = 1,
   Attack = 20
}
