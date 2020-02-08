using UnityEngine;
using UnityEngine.UI;
using System;

public delegate void GetExperience();

public class UILevelData : MonoBehaviour
{
    private Slider _sliderLevel;
    private Text _textLevel;
    private Text _textGold;

    private UILevel[] levels;
    private int currentLevelText = 1;
    private int priceCoinExp = 10;
    private int priceCoinGold = 10;
    private int currentCoinGold = 0;
    private event GetExperience getExperience;


    private void Start()
    {
        levels = Enum.GetValues(typeof(UILevel)) as UILevel[];

        _sliderLevel = GameObject.FindGameObjectWithTag("SliderLevel").GetComponent<Slider>();
        _sliderLevel.value = (int)UILevel.Zero;
        _sliderLevel.maxValue = (int)levels[currentLevelText];

        _textLevel = _sliderLevel.transform.GetChild(3).GetComponent<Text>();
        _textLevel.text = "LEVEL" + " " + currentLevelText;
        _textGold = _sliderLevel.transform.GetChild(4).GetComponent<Text>();
        _textGold.text = currentCoinGold.ToString();

        getExperience += GetCoins;
        getExperience += LevelUp;
    }

    private void GetCoins()
    {
        _sliderLevel.value += priceCoinExp;
        currentCoinGold += priceCoinGold;
        _textGold.text = currentCoinGold.ToString();
    }

    private void LevelUp()
    {
        if(_sliderLevel.value >=_sliderLevel.maxValue)
        {
            _sliderLevel.value = (int)UILevel.Zero;
            _sliderLevel.maxValue = (int)levels[++currentLevelText];
            _textLevel.text = "LEVEL" + " " + currentLevelText;
        }
    }

    public void InvokeIventGetExperience()
    {
        getExperience.Invoke();
    }
}
public enum UILevel
{
    Zero = 0,
    One = 150,
    Two = 200,
    Three = 250,
    Four = 300,
    Five = 350,
    Six = 400,
    Seven = 450,
    Eight = 500,
    Nine = 550,
    Ten = 600
}
