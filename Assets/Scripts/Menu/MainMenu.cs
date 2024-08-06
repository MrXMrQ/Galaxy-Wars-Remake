using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        SetToPrimaryDisplay();
    }
    void SetToPrimaryDisplay()
    {
        //* Check if there are multiple displays
        if (Display.displays.Length > 1)
        {
            //* Activate the first display (main display)
            Display.displays[0].Activate();
        }

        //* Set the main display as the target display for the game
        Screen.SetResolution(Display.displays[0].systemWidth, Display.displays[0].systemHeight, true);
    }
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
