using UnityEngine;
using System.Collections.Generic;

public class UIInventoryCell : MonoBehaviour
{
    [SerializeField] private InvertoryCell _invertoryCell;
    public InvertoryCell InvertoryCell { get { return _invertoryCell; } }
    [SerializeField] private UISensor _uISensor;

    public void CheckClothes()
    {
        if (gameObject.transform.childCount == 1)
        {
            GameObject item = gameObject.transform.GetChild(0).gameObject;

            if (item.GetComponent<ClothesData>().ClothesActive == 0)
            {
                List<GameObject> allSlots = _uISensor.AllCell;

                for (int i = 0; i < allSlots.Count; i++)
                {
                    if (allSlots[i].transform.childCount == 0)
                    {
                        item.transform.SetParent(allSlots[i].transform);
                        item.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                        break;
                    }
                }
            }
        }
    }
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
