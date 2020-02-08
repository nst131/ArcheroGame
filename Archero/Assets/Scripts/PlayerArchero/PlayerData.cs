using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float maxHp = 100;
    public float MaxHp { get { return maxHp; } }
    [SerializeField] private float damage = 100;
    public float Damage { get { return damage; } }
}
