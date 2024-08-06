using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradesMenu : MonoBehaviour
{
    void Start()
    {

    }

    public void LoadGameModeMenu()
    {
        SceneManager.LoadScene(1);
    }
}
