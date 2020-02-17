using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private BotsData _botsData;
    [SerializeField] private BossData _bossData;
    [SerializeField] private GameObject[] _pointsBots;
    [SerializeField] private GameObject _camera;
    private GameObject[] _bots;
    private GameObject _boss;

    private int levelPassage = 1;
    public int LevelPassage { get { return levelPassage; } }

    private void Start()
    {
        _bots = Resources.LoadAll<GameObject>("Prefabs/Bots");
        _boss = Resources.Load<GameObject>("Prefabs/Boss/EnemyBoss");

        RevivalBots();
    }

    public void RevivalBots()
    {
        for (int i = 0; i < _pointsBots.Length; i++)
        {
            Instantiate<GameObject>(_bots[Random.Range(0, _bots.Length)], _pointsBots[i].transform.position, Quaternion.identity);
        }
        levelPassage++;
    }

    public void RevivalBoss()
    {
        Instantiate<GameObject>(_boss, _pointsBots[Random.Range(0, _pointsBots.Length)].transform.position, Quaternion.identity);
        levelPassage++;
        _bossData.InvokeEventLevelUpBoss();
        _botsData.InvokeEventLevelUpBots();
        _camera.AddComponent<Boss>();
    }
}
