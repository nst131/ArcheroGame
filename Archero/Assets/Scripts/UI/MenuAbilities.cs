using UnityEngine;
using UnityEngine.UI;

public class MenuAbilities : MonoBehaviour
{ 
    [SerializeField] private GameObject _panelAbilities;
    [SerializeField] private GameObject _panelCharacteristic;
    [SerializeField] private GameObject _panelDeterminateLine;
    [SerializeField] private GameObject _panelLineSkill;
    [SerializeField] private GameObject _panelFinalCharacteristic;
    [SerializeField] private Image _imageFinalSkill;
    [SerializeField] private Text _textFinalSkill;
    [SerializeField] private GameObject[] _allCharacteristics;
    private event ReproductionSounds _soundLevelUp;
    private PlayerSounds _playerSounds;
    private GameObject _player;
    private HealthHelper _playerHealth;
    private Player _playerMove;
    private Vector3 _posPanelLineSkill;
    
    private bool _stopSpeed = false;
    private bool _insert = false;
    private bool _add = false;
    private float _currentPlayerSpeed;

    private float _scrollMaxSpeed = -1.5f;
    private float _scrollMinSpeed = 0.0f;
    private float _scrollCurrentSpeed;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
        _playerMove = _player.GetComponent<Player>();
        _playerSounds = _player.GetComponent<PlayerSounds>();
        _currentPlayerSpeed = _playerMove.SpeedMove;


        _posPanelLineSkill = _panelLineSkill.transform.position;
        _panelFinalCharacteristic.SetActive(false);
        _panelAbilities.SetActive(false);
            
        _soundLevelUp += _playerSounds.LevelUp;
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
        foreach (var skill in _panelLineSkill.GetComponentsInChildren<AddSkill>())
        {
            Destroy(skill.gameObject);
        }
        _panelLineSkill.transform.position = _posPanelLineSkill;
    }

    private void ScrollMove()
    {
        if (!_insert) return;

        if (_scrollCurrentSpeed != _scrollMaxSpeed && !_stopSpeed)
        {
            _scrollCurrentSpeed = Mathf.MoveTowards(_scrollCurrentSpeed, _scrollMaxSpeed, Time.deltaTime);
        }
        else
        {
            _stopSpeed = true;
            _scrollCurrentSpeed = Mathf.MoveTowards(_scrollCurrentSpeed, _scrollMinSpeed, Time.deltaTime);
        }

        _panelLineSkill.transform.Translate(new Vector3(_scrollCurrentSpeed, 0, 0) * 1000 * Time.deltaTime);
    }

    private void Determinate()
    {
        if (!_insert || _add) return;

        Ray ray = new Ray(_posPanelLineSkill, Vector3.down);
        RaycastHit hit;

        if (_scrollCurrentSpeed == _scrollMinSpeed)
        {
          if (Physics.Raycast(ray, out hit, 120))
          {
                _soundLevelUp.Invoke();
                _panelFinalCharacteristic.SetActive(true);
                _imageFinalSkill.sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
                _textFinalSkill.text = hit.collider.gameObject.name;
                hit.collider.gameObject.GetComponent<AddSkill>().PlusSkill();
                _add = true;

                _scrollMinSpeed = 0.0f;
          }
          else
          {
                _scrollMinSpeed = 0.1f;
          }
        }
    }

    public void StopAbilities()
    {
        _playerHealth.SliderInstallActive(true);
        _playerMove.SpeedMove = _currentPlayerSpeed;
        _panelFinalCharacteristic.SetActive(false);
        DeleteCharacteristics();
        _insert = false;
        _add = false;
        _panelAbilities.SetActive(false);
    }

    public void StartAbilities()
    {
        _playerHealth.SliderInstallActive(false);
        _playerMove.SpeedMove = 0;
        InsertCharacteristics();
        _insert = true;
        _stopSpeed = false;
        _panelAbilities.SetActive(true);
    }
}
