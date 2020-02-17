using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _setting;

    private void Start()
    {
        _setting.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Archero");
    }

    public void Setting()
    {
        _mainMenu.SetActive(false);
        _setting.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Low()
    {
        QualitySettings.SetQualityLevel(1);
    }

    public void Medium()
    {
        QualitySettings.SetQualityLevel(2);
    }

    public void High()
    {
        QualitySettings.SetQualityLevel(5);
    }

    public void Back()
    {
        _mainMenu.SetActive(true);
        _setting.SetActive(false);
    }
}
