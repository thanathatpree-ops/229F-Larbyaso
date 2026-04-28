using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            Debug.Log(other.name + " Fall Dead");

            
            character.TakeDamage(999999);
        }
        else
        {
            
            Destroy(other.gameObject);
        }
    }
}