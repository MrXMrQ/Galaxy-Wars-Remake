using UnityEngine.SceneManagement;
using UnityEngine;

public class GameModeMenu : MonoBehaviour
{
    public void LoadEndless()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadBack()
    {
        SceneManager.LoadScene(0);
    }
}
