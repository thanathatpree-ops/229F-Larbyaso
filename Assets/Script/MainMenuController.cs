using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    
    public void PlayGame()
    {
        PlayerData.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credit()
    {
        Debug.Log("Credit");
        SceneManager.LoadScene("Credit");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game!"); 
        Application.Quit(); 
    }
}
