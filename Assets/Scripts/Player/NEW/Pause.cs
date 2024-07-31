using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    bool _isPaused;

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
        _isPaused = !_isPaused;
        pauseScreen.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
    }
    public void ResumeGame()
    {
        _isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }
}