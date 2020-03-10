using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UISettingSound : MonoBehaviour
{
    private AudioSource _audioSourceMusic;
    private Slider _sliderMusicVolume;
    private Slider _sliderSoundVolume;
    private UnityAction<float> _setMusicVolume;
    private UnityAction<float> _setSoundVolume;

    public static float MaxMusicVolume = 1;

    private void Start()
    {
        _audioSourceMusic = GameObject.FindObjectOfType<UISound>().gameObject.GetComponent<AudioSource>();
        _sliderMusicVolume = GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>();
        _sliderSoundVolume = GameObject.FindGameObjectWithTag("SliderSound").GetComponent<Slider>();

        _sliderMusicVolume.value = MaxMusicVolume;
        _sliderSoundVolume.value = PlayerSounds.MaxSoundVolume;

        _setMusicVolume += OnSetMusicVolume;
        _setSoundVolume += OnSetSoundVolume;
        _sliderMusicVolume.onValueChanged.AddListener(_setMusicVolume);
        _sliderSoundVolume.onValueChanged.AddListener(_setSoundVolume);
    }

    public void OnSetMusicVolume(float maxValue)
    {
        MaxMusicVolume = _sliderMusicVolume.value;
        _audioSourceMusic.volume = _sliderMusicVolume.value;
    }

    public void OnSetSoundVolume(float maxValue)
    {
        PlayerSounds.MaxSoundVolume = _sliderSoundVolume.value;
    }
}
