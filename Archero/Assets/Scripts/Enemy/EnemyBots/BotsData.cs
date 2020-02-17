using UnityEngine;

public class BotsData : MonoBehaviour
{
    private delegate void EnemyBots();
    [SerializeField] private LevelUp _levelUp;

    [Header ("Characteristics")]
    [SerializeField] private float _maxHp = 100;
    public float MaxHp { get { return _maxHp; } }

    [SerializeField] private float _damage = 20;
    public float Damage { get { return _damage; } }

    private event EnemyBots levelUpBots;
    private int levelPassage;

    private void Start()
    {
        levelPassage = _levelUp.LevelPassage;
        levelUpBots += LevelUp;
    }

    private void LevelUp()
    {
        if(levelPassage < _levelUp.LevelPassage)
        {
            levelPassage = _levelUp.LevelPassage;
            _maxHp += 0.2f * _maxHp;
            _damage += 0.2f * _damage;
        }
    }

    public void InvokeEventLevelUpBots()
    {
        levelUpBots.Invoke();
    }
}
