using UnityEngine;
using UnityEngine.SceneManagement;

public class PausMenu : MonoBehaviour
{
    public static bool gameisPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameisPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }

        }
    }

    void Paused ()
    {
        PlayerMovement.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameisPaused = true;

    }

    public void Resume()
    {
        PlayerMovement.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameisPaused = false;

    }
    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
}
