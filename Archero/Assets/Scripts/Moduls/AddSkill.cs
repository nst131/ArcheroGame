using UnityEngine;

public class AddSkill : MonoBehaviour
{
    [SerializeField] SkillName _skill;
    private PlayerData _playerData;

    public void PlusSkill()
    {
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();

        _playerData.SumSkill(_skill);
    }
}
