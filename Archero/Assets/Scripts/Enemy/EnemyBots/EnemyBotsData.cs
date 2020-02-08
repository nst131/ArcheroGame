using UnityEngine;

public delegate void EnemyBots();

public class EnemyBotsData : MonoBehaviour
{
    [SerializeField] private float maxHp = 100;
    public float MaxHp { get { return maxHp; } }
    [SerializeField] private float damage = 20;
    public float Damage { get { return damage; } }
    private event EnemyBots levelUpBots;

    private GameManager _gameManager;
    private int levelPassage;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
        levelPassage = _gameManager.LevelPassage;
        levelUpBots += LevelUp;
    }

    private void LevelUp()
    {
        if(levelPassage < _gameManager.LevelPassage)
        {
            levelPassage = _gameManager.LevelPassage;
            maxHp += 0.2f * maxHp;
            damage += 0.2f * damage;
        }
    }

    public void InvokeEventLevelUpBots()
    {
        levelUpBots.Invoke();
    }
}
