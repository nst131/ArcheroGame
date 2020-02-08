using UnityEngine;
using UnityEngine.AI;

public class TransitionToNextLevel : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _playerNavMeshAgent;
    private Vector3 _pointStartOfDeparture;
    private Vector3 _pointFinishOfDeparture;
    private GateOpen _gateOpen;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerNavMeshAgent = _player.GetComponent<NavMeshAgent>();
        _pointStartOfDeparture = gameObject.transform.position;
        _pointFinishOfDeparture = GameObject.FindGameObjectWithTag("Ground").transform.GetChild(2).gameObject.transform.position;
        _gateOpen = GameObject.FindGameObjectWithTag("Ground").transform.GetChild(0).gameObject.GetComponent<GateOpen>();
    }

    private void Update()
    {
        GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        if(_gateOpen.openGate)
        {
            if (Vector3.Distance(_player.transform.position, _pointStartOfDeparture) <= 0.5f)
            {
                _playerNavMeshAgent.SetDestination(_pointFinishOfDeparture);
            }
        }
    }
}
