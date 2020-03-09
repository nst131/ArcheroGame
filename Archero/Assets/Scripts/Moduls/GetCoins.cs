using UnityEngine;

public class GetCoins : MonoBehaviour
{
    private delegate void Move(GameObject[] coins, Vector3 posPlayer);

    private GameObject _player;
    private Gate _gate;
    private UILevelUp _levelUpPlayer;
    private event Move _moveCoins;
    private event Move _moveBox;

    void Start()
    {
        _player = gameObject;
        _gate = GameObject.FindObjectOfType<Gate>();
        _levelUpPlayer = GameObject.FindGameObjectWithTag("SliderLevel").GetComponent<UILevelUp>();
        _moveCoins += MoveThingsToPlayer;
        _moveBox += MoveThingsToPlayer;
    }

    public void InvokeEventMoveCoins()
    {
        _moveCoins.Invoke(NumberCoins(), _player.transform.position);
    }

    public void InvokeEventMoveBox()
    {
        _moveBox.Invoke(NumberBoxs(),_player.transform.position);
    }

    private GameObject[] NumberCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coins");
        return coins;
    }

    private GameObject[] NumberBoxs()
    {
        GameObject[] boxs = GameObject.FindGameObjectsWithTag("Box");
        return boxs;
    }

    private void MoveThingsToPlayer(GameObject[] coins, Vector3 posPlayer)
    {
        Vector3 posPlayerHigh = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].transform.position = Vector3.MoveTowards(coins[i].transform.position, posPlayerHigh, Time.deltaTime * 20);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Coins") && _gate.GateOpen)
        {
            _levelUpPlayer.InvokeIventGetExperience();
            Destroy(other.gameObject);
        }
    }
}
