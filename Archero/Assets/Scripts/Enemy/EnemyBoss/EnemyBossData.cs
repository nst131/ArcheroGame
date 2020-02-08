using UnityEngine;

public delegate void EnemyBoss();

public class EnemyBossData : MonoBehaviour
{
    [SerializeField] private float maxHp = 200;
    public float MaxHp { get { return maxHp; } }

    [SerializeField] private float firstDamage = 20;
    public float FirstDamage { get { return firstDamage; } }
    [SerializeField] private float secondDamage = 20;
    public float SecondDamage { get { return secondDamage; } }
    [SerializeField] private float roundDamage = 0.5f;
    public float RoundDamage { get { return roundDamage; } }
    [SerializeField] private float fireDamage = 0.5f;
    public float FireDamage { get { return fireDamage; } }

    private GameManager _gameManager;
    private event EnemyBoss levelUpBoss;
    private int levelPassage;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
        levelUpBoss += LevelUp;
        levelPassage = _gameManager.LevelPassage;
    }

    private void LevelUp()
    {
        if (levelPassage < _gameManager.LevelPassage)
        {
            levelPassage = _gameManager.LevelPassage;
            maxHp += 0.5f * maxHp;
            firstDamage += 0.2f * firstDamage;
            secondDamage += 0.2f * secondDamage;
            roundDamage += 0.1f * roundDamage;
            fireDamage += 0.1f * fireDamage;
        }
    }

    public void InvokeEventLevelUpBoss()
    {
        levelUpBoss.Invoke();
    }
}
