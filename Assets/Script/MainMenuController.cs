using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    
    public void PlayGame()
    {
        PlayerData.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
    public void QuitGame()
    {
        Debug.Log("Quit Game!"); 
        Application.Quit(); 
    }
}
