using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour
{
   public void GoToMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
