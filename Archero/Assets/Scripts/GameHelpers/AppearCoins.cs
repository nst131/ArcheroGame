using UnityEngine;

public delegate void Scatter();

public class AppearCoins : MonoBehaviour
{
    private GameObject _coin;
    private GameObject _enemy;
    private event Scatter scatterCoins;

    private void Start()
    {
        _coin = Resources.Load<GameObject>("Coin");
        _enemy = gameObject;
        scatterCoins += ScatterCoins;
    }

    public void InvokeEventScatterCoins()
    {
        scatterCoins.Invoke();
    }

    private void ScatterCoins()
    {
        Vector3 enemyPosHeight = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 2, _enemy.transform.position.z);
        for (int i = 0; i < 5; i++)
        {
            Instantiate<GameObject>(_coin, enemyPosHeight + Random.insideUnitSphere, Quaternion.identity);
        }
    }
}
