using UnityEngine;

public class Enemywithweapon : Enemy , IsShootable
{
    [Header("Diagram Fields")]
    public Player player;
    public float attackRange;

    [Header("IsShootable Interface")]
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Initialize(25, 15);
        MpDrop = 10;

        ReloadTime = 2.0f; 
        WaitTime = 0.0f;
        attackRange = 6.0f; 

        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }
    }

    void Update()
    {
        
        WaitTime += Time.deltaTime;
    }

    
    public override void Behavior()
    {
        if (player == null) return;

        
        float distance = Vector2.Distance(transform.position, player.transform.position);

        
        if (distance <= attackRange)
        {
            Shoot();
        }
    }

    
    public void Shoot()
    {
        if (WaitTime >= ReloadTime)
        {
            
            GameObject bulletObj = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);

           
            EnemySkill skill = bulletObj.GetComponent<EnemySkill>();
            if (skill != null)
            {
                
                skill.InitWeapon(this.Damage, this );
            }

            WaitTime = 0.0f; 
        }
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
                
                Debug.Log("2. เจอ Player แล้ว ชื่อ: " + player.name);
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