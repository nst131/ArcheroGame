using UnityEngine;

public class Drop : MonoBehaviour
{
    private delegate void Scatter();

    private GameObject _enemy;
    private GameObject _coin;
    private GameObject _bottleHealth;
    private event Scatter _scatterHealth;
    private event Scatter _scatterCoins;

    private void Start()
    {
        _enemy = gameObject;
        _bottleHealth = Resources.Load<GameObject>("Health");
        _coin = Resources.Load<GameObject>("Coin");
        _scatterHealth += ScatterHealth;
        _scatterCoins += ScatterCoins;
    }

    private void InsertBottleHealth()
    {
        Vector3 enemyPosHeight = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 2, _enemy.transform.position.z);
        Instantiate<GameObject>(_bottleHealth, enemyPosHeight, Quaternion.identity);
    }

    private bool ChanceOfFalling()
    {
        if(GetComponent<BossAttack>())
        {
            if (Random.Range(0, 10) <= 10)
                return true;
        }
        else
        {
            if (Random.Range(0, 10) <= 2)
                return true;
        }

        return false;
    }

    private void ScatterHealth()
    {
        if(ChanceOfFalling())
        {
            InsertBottleHealth();
        }
    }

    private void ScatterCoins()
    {
        Vector3 enemyPosHeight = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 2, _enemy.transform.position.z);
        for (int i = 0; i < 5; i++)
        {
            Instantiate<GameObject>(_coin, enemyPosHeight + Random.insideUnitSphere, Quaternion.identity);
        }
    }

    public void InvokeEventScatterHealth()
    {
        _scatterHealth.Invoke();
    }

    public void InvokeEventScatterCoins()
    {
        _scatterCoins.Invoke();
    }
}
