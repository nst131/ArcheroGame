using UnityEngine;

public class ClothesData : MonoBehaviour
{
    [SerializeField] private UIClothesData _characteristicClothes;
    public UIClothesData CharacteristicClothes { get { return _characteristicClothes; } }
    [SerializeField] private InvertoryCell _typeClothes;
    public InvertoryCell TypeCltothes { get { return _typeClothes; } }
    [SerializeField] private AllClothes _nameClothe;
    public AllClothes NameClothe { get { return _nameClothe; } }
}
