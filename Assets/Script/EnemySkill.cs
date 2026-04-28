using UnityEngine;

public class EnemySkill : Weapon
{

    private Vector2 direction;
    [SerializeField] private float speed = 5f;

    
    void Start()
    {
        
        Player target = FindAnyObjectByType<Player>();
        if (target != null)
        {
            
            direction = (target.transform.position - transform.position).normalized;
        }
        else
        {
            direction = Vector2.left; 
        }

        
        Destroy(gameObject, 5.0f);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    public override void OnHitWith(Character character)
    {
        
        if (character is Player)
        {
            character.TakeDamage(this.Damage);
            Destroy(gameObject); 
        }
    }
}