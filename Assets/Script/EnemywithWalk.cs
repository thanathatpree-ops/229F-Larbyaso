using UnityEngine;

public class EnemywithWalk : Enemy
{
    [Header("Settings")]
    public Player player;       
    public float speed = 2.0f;  

    
    [SerializeField] private float detectRange = 5.0f;

    void Start()
    {
        
        base.Initialize(25, 10);
        this.MpDrop = 5;

        
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }
    }

    
    public override void Behavior()
    {
        if (player == null) return;

        
        float distance = Vector2.Distance(transform.position, player.transform.position);


        if (distance <= detectRange)
        {
            ChasePlayer();

            
            if (anim != null) anim.SetBool("isRunning", true);
        }
        else
        {
            
            if (anim != null) anim.SetBool("isRunning", false);
        }
    }

    private void ChasePlayer()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);

        
        if (player.transform.position.x > transform.position.x && transform.localScale.x < 0)
        {
            Flip();
        }
        
        else if (player.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    
    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void FixedUpdate()
    {
        Behavior();

        if (this.IsDead())
        {

            Debug.Log("1. Enemy ตายแล้ว (เข้า if IsDead)");

            Player player = FindAnyObjectByType<Player>();

            if (player != null)
            {
                // จุด B: เจอ Player
                Debug.Log("2. เจอ Player แล้ว ชื่อ: " + player.name);
                player.AddMp(this.MpDrop);
                Destroy(this.gameObject);
            }
            else
            { // จุด C: หา Player ไม่เจอ
                Debug.LogError("3. หา Player ไม่เจอ!!! (player เป็น null)");
            }

        }
    }
    }
