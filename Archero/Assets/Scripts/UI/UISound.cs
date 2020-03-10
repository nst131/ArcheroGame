using UnityEngine;

public class UISound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audiosBackground;
    [SerializeField] private AudioSource _audioSourceMusic;
    public AudioSource AudioSourceMusic { get { return _audioSourceMusic; } set { _audioSourceMusic = value; } }
   
    private static UISound _instance;

    public static UISound Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UISound>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
        set { _instance = value; }
    }

    private void Start()
    {
        IntilizationInstance();
    }

    private void IntilizationInstance()
    {
        if (UISound._instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            UISound._instance = this;
            DontDestroyOnLoad(this);
            SetAudio();
        }
    }

    public void SetAudio()
    {
        AudioClip audio = _audiosBackground[Random.Range(0, _audiosBackground.Length)];
        _audioSourceMusic.PlayOneShot(audio);
    }

    public void StopAudio()
    {
        _audioSourceMusic.Stop();
    }
}
