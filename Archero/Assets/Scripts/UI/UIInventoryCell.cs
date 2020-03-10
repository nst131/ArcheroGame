using UnityEngine;

public class UIInventoryCell : MonoBehaviour
{
    [SerializeField] private InvertoryCell _invertoryCell;
    public InvertoryCell InvertoryCell { get { return _invertoryCell; } }
}
public enum InvertoryCell
{
    Helm,
    Armor,
    Bow,
    Arrow,
    Pants,
    Boots
}
