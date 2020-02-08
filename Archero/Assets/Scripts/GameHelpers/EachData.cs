using UnityEngine;

public class EachData : MonoBehaviour
{
    [HideInInspector] public EnemyBotsData BotsData ;
    [HideInInspector] public EnemyBossData BossData ;
    [HideInInspector] public PlayerData PlayersData ;

    private void Start()
    {
        BotsData = GetComponent<EnemyBotsData>();
        BossData = GetComponent<EnemyBossData>();
        PlayersData = GetComponent<PlayerData>();
    }
}
