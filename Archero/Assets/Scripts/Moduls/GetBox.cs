using UnityEngine;

public class GetBox : MonoBehaviour
{
    private Gate _gate;
    private SaveLoadManager _manager;

    private void Start()
    {
        _gate = GameObject.FindObjectOfType<Gate>();
        _manager = GameObject.FindObjectOfType<SaveLoadManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && _gate.GateOpen)
        {
            AmountClothesHasPlayer.Armor = 1;
            AmountClothesHasPlayer.Helm = 1;
            _manager.LoadScene = true;
            Destroy(gameObject);
        }
    }
}
