using UnityEngine;
using System.Linq;

public class GateOpen : MonoBehaviour
{
    private GameObject _player;
    private Animator _animGate;
    private TakeCoins _takeCoins;
    private HealthHelper [] _everbodyDied;

    public bool openGate;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animGate = GetComponent<Animator>();
        _takeCoins = _player.GetComponent<TakeCoins>();
    }

    private void Update()
    {
        OpenGate();
    }

    private void OpenGate()
    {
        _everbodyDied = GameObject.FindObjectsOfType<HealthHelper>().Where<HealthHelper>(p => !p.Dead).ToArray();
        if (_everbodyDied.Length == 1 && _everbodyDied[0].gameObject.GetComponent<PlayerMove>())
        {
            openGate = true;
            _animGate.SetBool("Open", true);
            _takeCoins.InvokeEventMoveCoins();
        }
        else
        {
            openGate = false;
            _animGate.SetBool("Open", false);
        }
    }
}

