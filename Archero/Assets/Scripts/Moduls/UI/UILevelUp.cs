using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UILevelUp : MonoBehaviour
{
    private delegate void GetExperience();
    private GameObject _gameManager;
    private MenuAbilities _menuAbilities;
    [SerializeField] private Slider _sliderLevel;
    [SerializeField] private Text _textLevel;
    [SerializeField] private Text _textGold;
    [SerializeField] private List<UILevelUpData> _listLevels;

    private int _currentLevelText = 1;
    private int _priceCoinExp = 10;
    private int _priceCoinGold = 10;
    private int _currentCoinGold = 0;
    private event GetExperience getExperience;


    private void Start()
    {
        _gameManager = GameObject.FindObjectOfType<LevelUp>().gameObject;
        _menuAbilities = _gameManager.GetComponent<MenuAbilities>();

        _sliderLevel.value = 0;
        _sliderLevel.maxValue = _listLevels[_currentLevelText - 1].AmountExperience;

        _textLevel.text = _listLevels[_currentLevelText - 1].name;
        _textGold.text = _currentCoinGold.ToString();

        getExperience += GetCoins;
        getExperience += LevelUp;
    }

    private void GetCoins()
    {
        _sliderLevel.value += _priceCoinExp;
        _currentCoinGold += _priceCoinGold;
        _textGold.text = _currentCoinGold.ToString();
    }

    private void LevelUp()
    {
        if(_sliderLevel.value >=_sliderLevel.maxValue)
        {
            ++_currentLevelText;
            _sliderLevel.value = 0;
            _sliderLevel.maxValue = _listLevels[_currentLevelText-1].AmountExperience;
            _textLevel.text = _listLevels[_currentLevelText-1].name;
    
            _menuAbilities.StartAbilities();
        }
    }

    public void InvokeIventGetExperience()
    {
        getExperience.Invoke();
    }
}
