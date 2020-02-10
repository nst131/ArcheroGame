using UnityEngine;
using UnityEngine.UI;

public class MenuCharacteristic : MonoBehaviour
{
    private GameObject _player;
    private GameObject _menuCharacteristic;
    private GameObject [] _allCharacteristics;
    private GameObject _panelCharacteristic;
    private GameObject _panelDeterminateLine;
    private GameObject _panelLineSkill;
    private GameObject _panelFinalCharacteristic;
    private Image _imageFinalSkill;
    private Text _textFinalSkill;
    private Vector3 _posPanelLineSkill;

    private bool stopSpeed = false;
    private bool insert = false;
    private float playerSpeedMove;

    private float scrollMaxSpeed = -1.5f;
    private float scrollMinSpeed = 0.0f;
    private float scrollCurrentSpeed;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _menuCharacteristic = GameObject.FindGameObjectWithTag("MenuCharacteristic");
        _allCharacteristics = Resources.LoadAll<GameObject>("Prefabs/Characteristics");
        _panelCharacteristic = GameObject.FindGameObjectWithTag("Characteristic");
        _panelDeterminateLine = _panelCharacteristic.transform.GetChild(0).GetComponent<RectTransform>().gameObject;
        _panelLineSkill = _panelCharacteristic.transform.GetChild(1).GetComponent<RectTransform>().gameObject;
        _panelFinalCharacteristic = GameObject.FindGameObjectWithTag("FinalCharacteristic");
        _imageFinalSkill = _panelFinalCharacteristic.transform.GetChild(1).GetComponent<Image>();
        _textFinalSkill = _panelFinalCharacteristic.transform.GetChild(2).GetComponent<Text>();

        playerSpeedMove = _player.GetComponent<PlayerMove>().SpeedMove;
        _posPanelLineSkill = _panelLineSkill.transform.position;
        _panelFinalCharacteristic.SetActive(false);
        _menuCharacteristic.SetActive(false);
    }

    private void Update()
    {
        ScrollMove();
        Determinate();
    }

    private void InsertCharacteristics()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject skill = Instantiate<GameObject>(_allCharacteristics[Random.Range(0, _allCharacteristics.Length)]) as GameObject;
            skill.transform.SetParent(_panelLineSkill.transform);
        }
    }

    private void DeleteCharacteristics()
    {
        foreach (var skill in _panelLineSkill.GetComponentsInChildren<GameObject>())
        {
            Destroy(skill.gameObject);
        }
        _panelLineSkill.transform.position = _posPanelLineSkill;
    }

    private void ScrollMove()
    {
        if (!insert) return;

        if (scrollCurrentSpeed != scrollMaxSpeed && !stopSpeed)
        {
            scrollCurrentSpeed = Mathf.MoveTowards(scrollCurrentSpeed, scrollMaxSpeed, Time.deltaTime);
        }
        else
        {
            stopSpeed = true;
            scrollCurrentSpeed = Mathf.MoveTowards(scrollCurrentSpeed, scrollMinSpeed, Time.deltaTime);
        }

        _panelLineSkill.transform.Translate(new Vector3(scrollCurrentSpeed, 0, 0) * 1000 * Time.deltaTime);
    }

    private void Determinate()
    {
        if (!insert) return;

        Ray ray = new Ray(_posPanelLineSkill, Vector3.down);
        RaycastHit hit;

        if (scrollCurrentSpeed == scrollMinSpeed)
        {
          if (Physics.Raycast(ray, out hit, 120))
          {
                _panelFinalCharacteristic.SetActive(true);
                _imageFinalSkill.sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
                _textFinalSkill.text = hit.collider.gameObject.name;
                if(_textFinalSkill.text.Contains("Clone"))
                {
                    _textFinalSkill.text = _textFinalSkill.text.Remove(_textFinalSkill.text.IndexOf('.'));
                }
                scrollMinSpeed = 0.0f;
          }
          else
          {
                scrollMinSpeed = 0.1f;
          }
        }
    }

    public void CloseMenuCharacteristic()
    {
        _player.GetComponent<HealthHelper>().SliderInstallActive(true);
        _player.GetComponent<PlayerMove>().SpeedMove = playerSpeedMove;
        _panelFinalCharacteristic.SetActive(false);
        //DeleteCharacteristics();
        insert = false;
        _menuCharacteristic.SetActive(false);
    }

    public void StartMenuCharacteristic()
    {
        _player.GetComponent<HealthHelper>().SliderInstallActive(false);
        _player.GetComponent<PlayerMove>().SpeedMove = 0;
        InsertCharacteristics();
        insert = true;
        stopSpeed = false;
        _menuCharacteristic.SetActive(true);
    }
}
