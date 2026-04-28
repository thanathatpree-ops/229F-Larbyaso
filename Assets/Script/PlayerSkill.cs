using UnityEngine;

public class PlayerSkill : Weapon
{

    [SerializeField] private float speed;

    public override void Move()
    {
        float newX = transform.position.x + speed * Time.fixedDeltaTime;
        float newY = transform.position.y;
        Vector2 newPosition = new Vector2(newX, newY);
        transform.position = newPosition;
    }

    public override void OnHitWith(Character character)
    {
        if (character is Enemy)
            character.TakeDamage(this.Damage);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 8.0f * GetShootDirection();
        
    }

    private void FixedUpdate()
    {
        Move();
    }
}
