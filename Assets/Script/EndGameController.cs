using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{    
    public void GoToCredits()
    {
        
        SceneManager.LoadScene("Credit");
    }
}