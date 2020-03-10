using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioSoundsPlayer;
    [SerializeField] private AudioSource[] _audioSourcePlayer;
    public static float MaxSoundVolume = 1;
    private bool _audioSourceWalkPlay = false;

    private void Start()
    {
        SetSoundsVolume();
    }

    private void SetSoundsVolume()
    {
        for (int i = 0; i < _audioSourcePlayer.Length; i++)
        {
            _audioSourcePlayer[i].volume = MaxSoundVolume;
        }
    }

    public void WalkStart()
    {
        if (_audioSourceWalkPlay)
            return;

        AudioClip audio = _audioSoundsPlayer[(int)Sounds.Walk];
        _audioSourcePlayer[(int)Sounds.Walk].PlayOneShot(audio);
        _audioSourceWalkPlay = true;
    }

    public void WalkStop()
    {
        if (!_audioSourcePlayer[(int)Sounds.Walk])
            return;

        _audioSourcePlayer[(int)Sounds.Walk].Stop();
        _audioSourceWalkPlay = false;
    }

    public void ShootStart()
    {
        AudioClip audio = _audioSoundsPlayer[(int)Sounds.Shoot];
        _audioSourcePlayer[(int)Sounds.Shoot].PlayOneShot(audio);
    }

    public void LevelUp()
    {
        AudioClip audio = _audioSoundsPlayer[(int)Sounds.LevelUp];
        _audioSourcePlayer[(int)Sounds.LevelUp].PlayOneShot(audio);
    }
    
    public void GameOver()
    {
        AudioClip audio = _audioSoundsPlayer[(int)Sounds.GameOver];
        _audioSourcePlayer[(int)Sounds.GameOver].PlayOneShot(audio);
    }
}
