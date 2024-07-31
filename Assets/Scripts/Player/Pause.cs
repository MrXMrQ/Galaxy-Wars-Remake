using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    bool _is_paused;

    void Start()
    {
        pauseScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _is_paused = !_is_paused;
        pauseScreen.SetActive(_is_paused);
        Time.timeScale = _is_paused ? 0f : 1f;
    }
    public void ResumeGame()
    {
        _is_paused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}