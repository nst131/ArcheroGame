using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene(AllScence.Menu.ToString());
    }

    public void Play()
    {
        SceneManager.LoadScene(AllScence.Archero.ToString());
        UISound.Instance.StopAudio();
    }
}
