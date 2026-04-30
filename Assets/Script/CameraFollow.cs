using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 

    [Header("Camera Settings")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; 
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    public bool useBounds = true; 
    public float minX; 
    public float maxX;    
    public float minY; 
    public float maxY;    

    void LateUpdate()
    {
        if (player != null)
        {        
            Vector3 desiredPosition = player.position + offset;

            if (useBounds)
            {
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);             
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);             
            }

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}