using UnityEngine;


public static class PlayerData
{
    public static int SavedHP; 
    public static int SavedMP; 
    public static bool HasData = false; 

    
    public static void ResetData()
    {
        HasData = false;
    }
}