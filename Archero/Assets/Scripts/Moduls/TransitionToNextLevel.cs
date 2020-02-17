using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TransitionToNextLevel : MonoBehaviour
{
    [SerializeField] private Transform _pointStartPlayer;
    [SerializeField] private Transform _pointStartOfDeparture;
    [SerializeField] private Transform _pointFinishOfDeparture;
    [SerializeField] private Gate _gate;

    private GameObject _player;
    private NavMeshAgent _playerNavMesh;
    private BlackoutScreen _blackoutScreen;
    private LevelUp _levelUp;

    private float _wateRevivalBots = 1.0f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerNavMesh = _player.GetComponent<NavMeshAgent>();
        _blackoutScreen = GameObject.FindObjectOfType<BlackoutScreen>();
        _levelUp = GameObject.FindObjectOfType<LevelUp>();
    }

    private void Update()
    {
        GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        if (_gate.GateOpen)
        {
            if (Vector3.Distance(_player.transform.position, _pointStartOfDeparture.position) <= 0.5f)
            {
                _playerNavMesh.SetDestination(_pointFinishOfDeparture.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            _blackoutScreen.GetDarkScreen = true;
            _playerNavMesh.enabled = false;
            _player.transform.position = _pointStartPlayer.position;
            StartCoroutine(WateRevivalBots());
            _playerNavMesh.enabled = true;
        }
    }

    private IEnumerator WateRevivalBots()
    {
        yield return new WaitForSeconds(_wateRevivalBots);

        if(_levelUp.LevelPassage % 2 == 0)
        {
            _levelUp.RevivalBoss();
        }
        else
        {
            _levelUp.RevivalBots();
        }
    }
}
