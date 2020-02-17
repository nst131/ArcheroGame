using UnityEngine;
using System.Linq;

public class Gate : MonoBehaviour
{
    [SerializeField] private Animator _gateAnim;

    private GameObject _player;
    private GetCoins _getCoins;

    public bool GateOpen;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _getCoins = _player.GetComponent<GetCoins>();
    }

    private void Update()
    {
        OpenGate();
    }

    private void OpenGate()
    {
        HealthHelper [] _everbodyDied = GameObject.FindObjectsOfType<HealthHelper>().Where<HealthHelper>(p => !p.Dead).ToArray();
        if (_everbodyDied.Length == 1 && _everbodyDied[0].gameObject.GetComponent<Player>())
        {
            GateOpen = true;
            _gateAnim.SetBool("Open", true);
            _getCoins.InvokeEventMoveCoins();
        }
        else
        {
            GateOpen = false;
            _gateAnim.SetBool("Open", false);
        }
    }
}

