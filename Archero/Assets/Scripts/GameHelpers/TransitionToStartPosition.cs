using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TransitionToStartPosition : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _startPlayerPosition;
    private BlackoutScreen _blackoutScreen;
    private GameManager _gameManager;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _startPlayerPosition = GameObject.FindGameObjectWithTag("Ground").transform.GetChild(3).transform.position;
        _blackoutScreen = GameObject.FindObjectOfType<BlackoutScreen>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            _blackoutScreen.GetDarkScreen = true;
            _player.GetComponent<NavMeshAgent>().enabled = false;
            _player.transform.position = _startPlayerPosition;
            StartCoroutine(WateRevivalBots());
            _player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private IEnumerator WateRevivalBots()
    {
        yield return new WaitForSeconds(3);

        if(_gameManager.LevelPassage % 2 == 0)
        {
            _gameManager.RevivalBoss();
        }
        else
        {
            _gameManager.RevivalBots();
        }
    }
}
