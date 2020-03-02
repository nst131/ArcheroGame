using UnityEngine;
using UnityEngine.UI;

public class UIHealthHelper : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Text _textHp;

    private HealthHelper _target;
    public HealthHelper Target { get { return _target; } set { _target = value; } }
    private CapsuleCollider _targetCapsuleCollider;
    public CapsuleCollider TargetCapsuleCollider {get { return _targetCapsuleCollider; } set { _targetCapsuleCollider = value; } }
    private string _textHealth;

    private void Update()
    {
        InitializationSliderHp();
        ChangeSliderPosition();
    }

    private void InitializationSliderHp()
    {
        if (_target == null || _target.Dead)
            return;

        if (_slider.maxValue != _target.MaxHp)
            _slider.maxValue = _target.MaxHp;
        if (_slider.value != _target.Hp)
            _slider.value = _target.Hp;

        _textHealth = _target.TextHp.ToString();
        if (_textHp.text != _textHealth)
        {
            if(_textHealth.Contains(","))
            {
                _textHp.text = _textHealth.Remove(_textHealth.IndexOf(','));
            }
            else
            {
                _textHp.text = _textHealth;
            }
        }
    }

    private void ChangeSliderPosition()
    {
        if (_target == null || _target.Dead)
            return;

        if (gameObject.tag == "Enemy")
        {
            Vector3 _newPosSlider = new Vector3(_target.transform.position.x, _target.transform.position.y
                + _targetCapsuleCollider.bounds.size.y * 1.3f, _target.transform.position.z);
            _rectTransform.position = Camera.main.WorldToScreenPoint(_newPosSlider);
        }
        if (gameObject.tag == "Player")
        {
            Vector3 _newPosSlider = new Vector3(_target.transform.position.x, _target.transform.position.y
                + _targetCapsuleCollider.bounds.size.y * 1.5f, _target.transform.position.z);
            _rectTransform.position = Camera.main.WorldToScreenPoint(_newPosSlider);
        }
    }
}
