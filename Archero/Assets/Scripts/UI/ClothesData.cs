using UnityEngine;

public class ClothesData : MonoBehaviour
{
    [SerializeField] private UIClothesData _characteristicClothes;
    public UIClothesData CharacteristicClothes { get { return _characteristicClothes; } }
    [SerializeField] private InvertoryCell _typeClothes;
    public InvertoryCell TypeCltothes { get { return _typeClothes; } }
    [SerializeField] private int _clothesActive = 0;
    public int ClothesActive { get { return _clothesActive; } set { _clothesActive = value; } }
}
