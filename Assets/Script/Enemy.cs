using UnityEngine;

public abstract class Enemy : Character
{
    public int MpDrop { get; set; }
    public abstract void Behavior();


   
}
