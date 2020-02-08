using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] _bots;
    private GameObject[] _pointsBots;
    private GameObject _boss;
    private GameObject _camera;
    private EnemyBotsData _botsData;
    private EnemyBossData _bossData;

    private int levelPassage = 1;
    public int LevelPassage { get { return levelPassage; } }

    private void Start()
    {
        _bots = Resources.LoadAll<GameObject>("Prefabs/Enemy");
        _pointsBots = GameObject.FindGameObjectsWithTag("Point");
        _boss = Resources.Load<GameObject>("Prefabs/Boss/EnemyBoss");
        _camera = Camera.main.gameObject;
        _botsData = GetComponent<EnemyBotsData>();
        _bossData = GetComponent<EnemyBossData>();

        RevivalBots();
    }

    public void RevivalBots()
    {
        for (int i = 0; i < _pointsBots.Length; i++)
        {
            Instantiate<GameObject>(_bots[Random.Range(0, _bots.Length)], _pointsBots[i].transform.position, Quaternion.identity);
            levelPassage++;
        }
    }

    public void RevivalBoss()
    {
        Instantiate<GameObject>(_boss, _pointsBots[Random.Range(0, _pointsBots.Length)].transform.position, Quaternion.identity);
        levelPassage++;
        _bossData.InvokeEventLevelUpBoss();
        _botsData.InvokeEventLevelUpBots();
        _camera.AddComponent<BossMove>();
    }
}
