using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioSoundsPlayer;
    [SerializeField] private AudioSource _audioSourceWalk;
    [SerializeField] private AudioSource _audioSourceShoot;
    private AudioSource _audioSourceLevelUp;
    private AudioSource _audioSourceGameOver;

    private bool _audioSourceWalkPlay = false;

    private void Start()
    {
        _audioSourceLevelUp = GameObject.FindGameObjectWithTag("SliderLevel").GetComponent<AudioSource>();
        _audioSourceGameOver = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }

    public void WalkStart()
    {
        if (_audioSourceWalkPlay)
            return;

        AudioClip audio = _audioSoundsPlayer[(int)Sounds.Walk];
        _audioSourceWalk.PlayOneShot(audio);
        _audioSourceWalkPlay = true;
    }

    public void WalkStop()
    {
        if (!_audioSourceWalk)
            return;

        _audioSourceWalk.Stop();
        _audioSourceWalkPlay = false;
    }

    public void ShootStart()
    {
        AudioClip audio = _audioSoundsPlayer[(int)Sounds.Shoot];
        _audioSourceShoot.PlayOneShot(audio);
    }

    public void LevelUp()
    {
        AudioClip audio = _audioSoundsPlayer[(int)Sounds.LevelUp];
        _audioSourceLevelUp.PlayOneShot(audio);
    }
    
    public void GameOver()
    {
        AudioClip audio = _audioSoundsPlayer[(int)Sounds.GameOver];
        _audioSourceGameOver.PlayOneShot(audio);
    }
}
