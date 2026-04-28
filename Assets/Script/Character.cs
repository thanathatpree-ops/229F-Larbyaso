using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private int health;
    public int Health 
    {  
        get { return health; }

        set { health = (value < 0) ? 0 : value; }
    }
    public int MaxHealth;
    public int Damage { get; set; }
    protected Animator anim;
    protected Rigidbody2D rb;


    public void Initialize(int startHealth , int thisDamage)
    {
        Health = startHealth;
        MaxHealth = startHealth;
        Damage = thisDamage;
        Debug.Log($"{this.name} health is {this.Health} and Damage is {this.Damage}.");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        this.anim.SetTrigger("isHit");
        Health -= damage;
        Debug.Log($"{this.name} take {damage} damage, Current Health {Health}");
        

        IsDead();
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            Debug.Log($"{this} Dead!");
            //Destroy(this.gameObject);
            //Destroy(this.gameObject);
            return true;
            
        }
        else { return false; }
    }



}
