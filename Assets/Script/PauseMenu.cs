using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel; 
    public string mainMenuSceneName = "MainMenu"; 

    
    public static bool isPaused = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false); 
        Time.timeScale = 1f;         
        isPaused = false;
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void LoadMainMenu()
    {
        
        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}