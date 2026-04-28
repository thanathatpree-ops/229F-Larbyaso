using UnityEngine;

public class EnemyWalk : Enemy
{

    public Vector2 velosity;
    public Transform[] movePoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Initialize(25, 10);

        MpDrop = 10;
        velosity = new Vector2(1.0f, 0.0f);
    }

    public override void Behavior()
    {
        anim.SetBool("isRunning", true);
        rb.MovePosition(rb.position + velosity * Time.fixedDeltaTime);
        
        if (velosity.x < 0 && rb.position.x <= movePoints[0].position.x)
        {
            Flip();
        }
        
        if (velosity.x > 0 && rb.position.x >= movePoints[1].position.x)
        {
            Flip();
        }

    }
    public void Flip()
    {
        velosity.x *= -1; //change direction of movement
                          //Flip the image
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void FixedUpdate()
    {
        Behavior();

        if (this.IsDead())
        {

            

            Player player = FindAnyObjectByType<Player>();

            if (player != null)
            {
                
                player.AddMp(this.MpDrop);
                Destroy(this.gameObject);
            }
            else
            { 
                Debug.LogError("3. หา Player ไม่เจอ!!! (player เป็น null)");
            }

        }


       
    }
}
