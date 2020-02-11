using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    PlayerData _playerData;

    public void PlusSkill(string NameSkill)
    {
        _playerData = GameObject.FindObjectOfType<GameManager>().GetComponent<PlayerData>();

        _playerData.SumSkill(NameSkill);
    }
}
