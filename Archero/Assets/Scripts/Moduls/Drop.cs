using UnityEngine;

public class Drop : MonoBehaviour
{
    private delegate void Scatter();

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _health;
    [SerializeField] private GameObject _box;
    [SerializeField] private BossAttack _boss;
    private event Scatter _scatterHealth;
    private event Scatter _scatterCoins;
    private event Scatter _scatterBox;

    private void Start()
    {
        _scatterHealth += ScatterHealth;
        _scatterCoins += ScatterCoins;
        _scatterBox += ScatterBox;
    }

    private bool ChanceOfFalling()
    {
        if(_boss != null)
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

    private bool ChanceofFallingBox()
    {
        if(_boss != null)
        {
            if (Random.Range(0, 10) <= 8) 
            {
                return true;
            }
        }
        return false;
    }

    private void ScatterHealth()
    {
        if(ChanceOfFalling())
        {
            InsertThings(_health);
        }
    }

    private void ScatterBox()
    {
        if(ChanceofFallingBox())
        {
            InsertThings(_box);
        }
    }

    private void InsertThings(GameObject thing)
    {
        Vector3 enemyPosHeight = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 2, _enemy.transform.position.z);
        if(thing == _health)
        {
            Instantiate<GameObject>(thing, enemyPosHeight, Quaternion.identity);
        }
        else
        {
            Instantiate<GameObject>(thing, enemyPosHeight + Random.insideUnitSphere , Quaternion.identity);
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

    public void InvokeEventScatterbox()
    {
        _scatterBox.Invoke();
    }
}
