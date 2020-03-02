using UnityEngine;
using System.Collections.Generic;

public class UISensor : MonoBehaviour
{
    [SerializeField] private GameObject _sensor;
    [SerializeField] private GameObject _imageSlot;
    [SerializeField] private GameObject _panelItems;
    [SerializeField] private RectTransform _panelMenuInventor;
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private GameObject _panelCharacteristics;
    [SerializeField] private List<GameObject> _allCell;
    public List<GameObject> AllCell { get { return _allCell; } }

    [Tooltip("Distance Ray")]private float _distance = 90;
    private bool _insertCell = false;

    private void Update()
    {
        CheckCell();
    }

    private void CheckCell()
    {
        Ray ray = new Ray(_sensor.transform.position, Vector3.up);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,_distance))
        {
            _insertCell = false;

            if(!_insertCell && hit.collider.gameObject.transform.childCount == 1)
            {
                InsertCell();
                IncreaseBottom();
            }
        }
        else
        {
            _insertCell = true;
        }

        if (!_insertCell)
            return;

        InsertCell();
    }

    private void InsertCell()
    {
        GameObject cell = Instantiate<GameObject>(_imageSlot);
        cell.transform.SetParent(_panelItems.transform);
        _allCell.Add(cell);
    }

    public void DressClothes()
    {
        for (int i = 0; i < _allCell.Count; i++)
        {
            if (_allCell[i].transform.childCount == 0)
            {
                _characterStats.CurrentClothes.GetComponent<ClothesData>().ClothesActive = 0;
                _characterStats.CurrentClothes.transform.SetParent(_allCell[i].transform);
                _characterStats.CurrentClothes.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                _panelCharacteristics.SetActive(false);
                break;
            }
        }
    }

    private void IncreaseBottom()
    {
        _panelMenuInventor.offsetMin = new Vector2(_panelMenuInventor.offsetMin.x,_panelMenuInventor.offsetMin.y - 71);
        _panelMenuInventor.offsetMax = new Vector2(_panelMenuInventor.offsetMax.x, _panelMenuInventor.offsetMax.y);
    }
}
