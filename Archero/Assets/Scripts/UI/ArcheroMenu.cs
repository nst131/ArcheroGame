using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcheroMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panelGameOver;
    public GameObject PanelGameOver { get { return _panelGameOver; } }
    [SerializeField] private GameObject _buttonContinue;
    public GameObject ButtonCountinue { get { return _buttonContinue; } }
    [SerializeField] GameObject _imageCoin;
    public GameObject ImageCoin { get { return _imageCoin; } }
    [SerializeField] private GameObject _panelZeroMoney;
    [SerializeField] private Text _textTime;
    [SerializeField] private SaveLoadManager _manager;
    private PlayerData _playerData;

    private float _times;
    private float _timeText = 8;
    private int _priceCountinue = 100;

    private void Start()
    {
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    private void Update()
    {
        ChangeText();
    }

    public void Back()
    {
        SceneManager.LoadScene(AllScence.Inventory.ToString());
        _manager.InvokeEventGame();
        UISound.Instance.SetAudio();
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene(AllScence.Inventory.ToString());
        _manager.InvokeEventGame();
        UISound.Instance.SetAudio();
    }

    public void Continue()
    {
        if(UIInventory.GameScore >= _priceCountinue)
        {
            _playerData.Resurrect();
            _timeText = 8;
            _panelGameOver.SetActive(false);
            UIInventory.GameScore -= _priceCountinue;
        }
        else
        {
            _panelZeroMoney.SetActive(true);
        }
    }

    public void ChangeText()
    {
       if(_panelGameOver.activeSelf)
       {
          if(Time.time > _times + 1)
          {
              _timeText--;
              _textTime.text = _timeText.ToString();
              _times = Time.time;
            if(_timeText == 0)
            {
                _manager.InvokeEventGame();
                ReturntoMenu();
            }
          }
       }
    }
}
