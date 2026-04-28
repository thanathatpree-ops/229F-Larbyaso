using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int Damage;
    public IsShootable Shooter;


    public abstract void Move();
    public abstract void OnHitWith(Character character);

    public void InitWeapon(int newDamage, IsShootable newShooter)
    {
        Damage = newDamage;
        Shooter = newShooter;
    }

    public int GetShootDirection()
    {
        float value = Shooter.ShootPoint.position.x - Shooter.ShootPoint.parent.position.x;

        if (value > 0)
            return 1; //face right
        else return -1; //face left
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            OnHitWith(other.GetComponent<Character>());
            Destroy(this.gameObject, 5f);
        }
    }



}