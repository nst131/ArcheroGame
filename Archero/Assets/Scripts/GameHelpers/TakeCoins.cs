using UnityEngine;

public delegate void CoinsMove(GameObject[] coins, Vector3 posPlayer);

public class TakeCoins : MonoBehaviour
{
    private GameObject _player;
    private GateOpen _gateOpen;
    private UILevelData _levelData;
    private event CoinsMove moveCoins;

    void Start()
    {
        _player = gameObject;
        _gateOpen = GameObject.FindObjectOfType<GateOpen>();
        _levelData = GameObject.FindGameObjectWithTag("SliderLevel").GetComponent<UILevelData>();
        moveCoins += new CoinsMove(MoveCoinsToPlayer);
    }

    void Update()
    {
        IgnoreCoins();
    }

    private void IgnoreCoins()
    {
        Physics.IgnoreLayerCollision(10, 10);
    }

    public void InvokeEventMoveCoins()
    {
        moveCoins.Invoke(NumberCoins(), _player.transform.position);
    }

    private GameObject[] NumberCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coins");
        return coins;
    }

    private void MoveCoinsToPlayer(GameObject[] coins, Vector3 posPlayer)
    {
        Vector3 posPlayerHigh = new Vector3(posPlayer.x, posPlayer.y + 3, posPlayer.z);
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].transform.position = Vector3.MoveTowards(coins[i].transform.position, posPlayerHigh, Time.deltaTime * 20);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Coins") && _gateOpen.openGate)
        {
            _levelData.InvokeIventGetExperience();
            Destroy(other.gameObject);
        }
    }
}
