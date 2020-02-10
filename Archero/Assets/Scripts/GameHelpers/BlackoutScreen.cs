using UnityEngine;
using UnityEngine.UI;

public class BlackoutScreen : MonoBehaviour
{
    private GameObject _panel;
    private GameObject _sliderLevel;
    private GameObject _textCoin;
    [SerializeField] private float stepColorPositive = 0.5f;
    [SerializeField] private float stepColorNegative = 0.5f;
    [SerializeField] private bool getDarkScreen;
    public bool GetDarkScreen { get { return getDarkScreen; } set { getDarkScreen = value; } }
    private int colorA;

    private void Start()
    {
        _panel = gameObject;
        _sliderLevel = GameObject.FindGameObjectWithTag("SliderLevel");
        _textCoin = GameObject.FindGameObjectWithTag("TextCoin");
    }

    private void Update()
    {
        DarkScreen();
    }

    private void DarkScreen()
    {
        if(getDarkScreen)
        {
            if (colorA == 0)
            {
                _sliderLevel.SetActive(false);
                _textCoin.SetActive(false);
                Image alphaImage = _panel.GetComponent<Image>();
                alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, alphaImage.color.a + stepColorPositive * Time.deltaTime);
                if (alphaImage.color.a >= 1)
                {
                    colorA = 1;
                }
            }

            if(colorA == 1)
            {
                Image alphaImage = _panel.GetComponent<Image>();
                alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.b, alphaImage.color.a - stepColorNegative * Time.deltaTime);
                if (alphaImage.color.a <= 0)
                {
                    colorA = 0;
                    getDarkScreen = false;
                    _sliderLevel.SetActive(true);
                    _textCoin.SetActive(true);
                }
            }
        }
    }
}
