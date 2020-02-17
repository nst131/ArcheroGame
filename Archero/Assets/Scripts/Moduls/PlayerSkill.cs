using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerData _playerData;

    public void PlusSkill(string NameSkill)
    {
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();

        _playerData.SumSkill(NameSkill);
    }
}
