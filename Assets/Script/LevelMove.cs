using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelMove : MonoBehaviour
{
    [Header("Settings")]
    public string nextSceneName;
    public bool useBuildIndex = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Player playerScript = other.GetComponent<Player>();

            
            if (playerScript != null)
            {
                PlayerData.SavedHP = playerScript.Health; 
                PlayerData.SavedMP = playerScript.MP;     
                PlayerData.HasData = true;                

                Debug.Log($"Saved Data: HP={PlayerData.SavedHP}, MP={PlayerData.SavedMP}");
            }

            GoToNextLevel();
        }
    }
    void GoToNextLevel()
    {
        Debug.Log("Player reached the exit! Loading next level...");

        if (useBuildIndex)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            
            SceneManager.LoadScene(nextSceneName);
        }
        
    }
}