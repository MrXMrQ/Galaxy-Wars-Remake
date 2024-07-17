using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadGameModes()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(6);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
