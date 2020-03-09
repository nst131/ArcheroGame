using System.Collections.Generic;
using UnityEngine;

public class ClotheItem : MonoBehaviour
{
    [SerializeField] private GameObject _buttonClothe;
    [SerializeField] private GameObject _panelCharacteristicsClothes;
    [SerializeField] private CharacterStats _characterStats;

    public void Clothe()
    {
        if (_characterStats.CurrentSlot.transform.childCount == 0) return;
        GameObject slot = _characterStats.CurrentSlot;
        ClothesData myitem = slot.GetComponentInChildren<ClothesData>();
        InvertoryCell typeClothes = myitem.TypeCltothes;
        Dictionary<GameObject, InvertoryCell> allSlots = _characterStats.AllSlotsWithKey;
        foreach (var item in allSlots)
        {
            if (item.Value == typeClothes)
            {
                myitem.gameObject.transform.SetParent(item.Key.transform);
                myitem.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
        }
        _buttonClothe.SetActive(false);
        _panelCharacteristicsClothes.SetActive(false);
    }
}
