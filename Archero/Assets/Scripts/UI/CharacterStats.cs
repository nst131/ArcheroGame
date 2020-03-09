using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private Text _textPlayerDamage;
    [SerializeField] private Text _textPlayerHealth;
    [SerializeField] private Text _textPlayerSpeed;
    [SerializeField] private Text _textCurrentDamage;
    [SerializeField] private Text _textCurrentHealth;
    [SerializeField] private Text _textCurrentSpeed;
    [SerializeField] private List<InvertoryCell> _allSlotsType;
    [SerializeField] private List<GameObject> _allSlots;
    private Dictionary<GameObject, InvertoryCell> _allSlotsWithKey;
    public Dictionary<GameObject,InvertoryCell> AllSlotsWithKey { get { return _allSlotsWithKey; } }

    [SerializeField] private GameObject _buttonDress;
    [SerializeField] private GameObject _buttonClothe;
    [SerializeField] private GameObject _panelShowClothes;
    [SerializeField] private Image _imageShowClothes;
    [SerializeField] private Text _textShowDamage;
    [SerializeField] private Text _textShowHealth;

    private GameObject _currentClothes;
    public GameObject CurrentClothes { get { return _currentClothes; } }
    private GameObject _currentSlot;
    public GameObject CurrentSlot { get { return _currentSlot; } }

    private static float _playerDamage = 50;
    private static float _playerHealth = 50;
    private static float _playerSpeed = 2;

    public static float PlayerCurrentDamage;
    public static float PlayerCurrentHealth;
    public static float PlayerCurrentSpeed;

    private static float _damage;
    private static float _health;
    private static float _speed;

    private void Awake()
    {
        IntilizationDict();
    }

    private void Start()
    {
        IntilizationStats();
    }

    private void IntilizationDict()
    {
        _allSlotsWithKey = new Dictionary<GameObject, InvertoryCell>();
        for (int i = 0; i < _allSlots.Count; i++)
        {
            _allSlotsWithKey.Add(_allSlots[i], _allSlotsType[i]);
        }
    }

    public void IntilizationStats()
    {
        CollectionPoints();

        _textPlayerDamage.text = _playerDamage.ToString() + "+" + _damage.ToString();
        _textPlayerHealth.text = _playerHealth.ToString() + "+" + _health.ToString();
        _textPlayerSpeed.text = _playerSpeed.ToString() + "+" + _speed.ToString();

        PlayerCurrentDamage = _playerDamage + _damage;
        PlayerCurrentHealth = _playerHealth + _health;
        PlayerCurrentSpeed = _playerSpeed + _speed;

        _textCurrentDamage.text = PlayerCurrentDamage.ToString();
        _textCurrentHealth.text = PlayerCurrentHealth.ToString();
        _textCurrentSpeed.text = PlayerCurrentSpeed.ToString();
    }

    private void CollectionPoints()
    {
        _damage = 0;
        _health = 0;
        for (int i = 0; i < _allSlots.Count; i++)
        {
           if(_allSlots[i].transform.childCount == 1)
           {
                ClothesData clothes = _allSlots[i].GetComponentInChildren<ClothesData>();
                _damage += clothes.CharacteristicClothes.Damage;
                _health += clothes.CharacteristicClothes.Health;
           }
        }
    }

    public void WatchClothes(GameObject backgroundClothes)
    {
        ClothesData clothes = backgroundClothes.GetComponentInChildren<ClothesData>();
        if (!clothes)
            return ;
        _panelShowClothes.SetActive(true);
        if(backgroundClothes.GetComponent<UIInventoryCell>())
        {
            _buttonDress.SetActive(true);
        }
        else
        {
            _buttonClothe.SetActive(true);
        }


        _imageShowClothes.sprite = backgroundClothes.transform.GetChild(0).GetComponent<Image>().sprite;
        _textShowDamage.text = "Damage" + " " + "+" + clothes.CharacteristicClothes.Damage.ToString();
        _textShowHealth.text = "Health" + " " + "+" + clothes.CharacteristicClothes.Health.ToString();

        _currentClothes = clothes.gameObject;
        _currentSlot = backgroundClothes;
    }

    public void CloseWatchClothes()
    {
        _buttonDress.SetActive(false);
        _buttonClothe.SetActive(false);
        _panelShowClothes.SetActive(false);
    }

    public void DeleteClothes()
    {
        switch (_currentClothes.GetComponent<ClothesData>().NameClothe)
        {
            case AllClothes.Armor : AmountClothesHasPlayer.Armor -= 1;
                break;
            case AllClothes.Arrow : AmountClothesHasPlayer.Arrow -= 1;
                break;
            case AllClothes.Boots : AmountClothesHasPlayer.Boots -= 1;
                break;
            case AllClothes.Bow   : AmountClothesHasPlayer.Bow -= 1;
                break;
            case AllClothes.Pants : AmountClothesHasPlayer.Pants -= 1;
                break;
            case AllClothes.Helm : AmountClothesHasPlayer.Helm -= 1;
                break;
                
        }

        Destroy(_currentClothes);

        CloseWatchClothes();
        Invoke("IntilizationStats", 0.1f);
    }
}
