using UnityEngine;

public class BossData : MonoBehaviour
{
    private delegate void EnemyBoss();
    [SerializeField] private LevelUp _levelUp;

    [Header("Characteristics")]
    [SerializeField] private float _maxHp = 200;
    public float MaxHp { get { return _maxHp; } }

    [SerializeField] private float _firstDamage = 20;
    public float FirstDamage { get { return _firstDamage; } }

    [SerializeField] private float _secondDamage = 20;
    public float SecondDamage { get { return _secondDamage; } }

    [SerializeField] private float _roundDamage = 0.5f;
    public float RoundDamage { get { return _roundDamage; } }

    [SerializeField] private float _fireDamage = 0.5f;
    public float FireDamage { get { return _fireDamage; } }

    private event EnemyBoss _levelUpBoss;
    private int _levelPassage;

    private void Start()
    {
        _levelUpBoss += LevelUp;
        _levelPassage = _levelUp.LevelPassage;
    }

    private void LevelUp()
    {
        if (_levelPassage < _levelUp.LevelPassage)
        {
            _levelPassage = _levelUp.LevelPassage;
            _maxHp += 0.5f * _maxHp;
            _firstDamage += 0.2f * _firstDamage;
            _secondDamage += 0.2f * _secondDamage;
            _roundDamage += 0.1f * _roundDamage;
            _fireDamage += 0.1f * _fireDamage;
        }
    }

    public void InvokeEventLevelUpBoss()
    {
        _levelUpBoss.Invoke();
    }
}
