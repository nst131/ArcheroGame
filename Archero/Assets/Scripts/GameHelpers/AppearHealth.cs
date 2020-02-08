using UnityEngine;

public class AppearHealth : MonoBehaviour
{
    private GameObject _bottleHealth;
    private GameObject _enemy;
    private Scatter _scatterBottleHealth;

    private void Start()
    {
        _bottleHealth = Resources.Load<GameObject>("BottleHealth");
        _enemy = gameObject;
        _scatterBottleHealth += ScatterBottleHealth;
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

    private void ScatterBottleHealth()
    {
        if(ChanceOfFalling())
        {
            InsertBottleHealth();
        }
    }

    public void InvokeEventScatterBottleHealth()
    {
        _scatterBottleHealth.Invoke();
    }
}
