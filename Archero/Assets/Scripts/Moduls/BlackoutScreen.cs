using UnityEngine;

public class BlackoutScreen : MonoBehaviour
{
    [SerializeField] private GameObject _panelBlackout;
    [SerializeField] private Animator _panelBlackoutAnim;
    [SerializeField] private GameObject _buttonBack;
    private GameObject _sliderLevel;
    private GameObject _textCoin;
    private bool _getDarkScreen;
    public bool GetDarkScreen { get { return _getDarkScreen; } set { _getDarkScreen = value; } }
    private bool _transparentScreen = true;
    private float _times;

    private void Start()
    {
        _panelBlackout = gameObject;
        _sliderLevel = GameObject.FindGameObjectWithTag("SliderLevel");
        _textCoin = GameObject.FindGameObjectWithTag("TextCoin");
    }

    private void Update()
    {
        DarkScreen();
    }

    private void DarkScreen()
    {
        if(_getDarkScreen)
        {
            if(_transparentScreen)
            {
                _sliderLevel.SetActive(false);
                _textCoin.SetActive(false);
                _buttonBack.SetActive(false);
                _panelBlackoutAnim.SetTrigger("GetBlack");
                _times = Time.time + 1;
                _transparentScreen = false;
            }
           
            if(Time.time > _times && !_transparentScreen)
            {
                _getDarkScreen = false;
                _sliderLevel.SetActive(true);
                _buttonBack.SetActive(true);
                _textCoin.SetActive(true);
                _panelBlackoutAnim.ResetTrigger("GetBlack");
                _transparentScreen = true;
            }
        }
    }
}
