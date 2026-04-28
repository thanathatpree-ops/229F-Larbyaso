using UnityEngine;
using UnityEngine.UI;

public class Player : Character , IsShootable
{
    public int MaxMP = 80;
    private int mp;
    public int MP
    {
        get { return mp; }
        set { mp = (value < 0) ? 0 : ( value > MaxMP) ? MaxMP : value; }
    }
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }

    [Header("UI Settings")]
    public Image hpBarImage; 
    private float maxHpCache;
    public Image mpBarImage;
    private float maxMPCache;



    [Header("Melee Settings (ฟันดาบ)")]
    
    [SerializeField] private Transform attackPoint;
    
    [SerializeField] private float attackRange = 1.0f;
    
    
    public float MeleeReloadTime { get; set; }
    private float meleeWaitTime;


    void Start()
    {
        base.Initialize(100, 15);
        maxHpCache = 100;
        maxMPCache = MaxMP;
        MP = 50;

        if (PlayerData.HasData == true)
        {
            this.Health = PlayerData.SavedHP; 
            this.MP = PlayerData.SavedMP;     

            
            if (this.Health <= 0) this.Health = 100;

            Debug.Log("Loaded Data from previous level!");
        }
        
                
        

        ReloadTime = 2.0f;
        WaitTime = 0.0f;
       

        
        MeleeReloadTime = 1.0f; 
        meleeWaitTime = 0.0f;
        anim = GetComponent<Animator>();
    }

    

    public void OnHitWith(Enemy enemy)
    {
        TakeDamage(enemy.Damage);
    }

    public void AddMp (int amount)
    {
        this.MP += amount;
        Debug.Log("Get MP! Current MP: " + this.MP);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            OnHitWith(enemy);
            Debug.Log($"{this.name} collides with {enemy.name}!");
        }
    }

    void Update()
    {
        if (hpBarImage != null)
        {
            
            hpBarImage.fillAmount = (float)this.Health / maxHpCache;
        }
        if (mpBarImage != null)
        {

            mpBarImage.fillAmount = (float)this.MP / maxMPCache;
        }

        WaitTime += Time.deltaTime;
        meleeWaitTime += Time.deltaTime;


        if (this.IsDead())
        {
            GameOverManager gm = FindAnyObjectByType<GameOverManager>();
            if (gm != null)
            {
                gm.ShowGameOver();
            }

            
            this.gameObject.SetActive(false);

        }



        Shoot();
        Slash(); 


    }

    private void FixedUpdate()
    {
       


     }

    public void Shoot()
    {
        
        if (Input.GetButtonDown("Fire2") && WaitTime >= ReloadTime && MP >= 10)
        {
            anim.SetTrigger("Attack2");
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
            if (bullet.TryGetComponent<PlayerSkill>(out PlayerSkill skill))
            {
                skill.InitWeapon(Damage*3/2, this);
            }

            WaitTime = 0.0f;
            MP -= 10;
            Debug.Log($"Shoot! MP Left: {MP}");
        }
    }

    
    public void Slash()
    {
        
        if (Input.GetButtonDown("Fire1") && meleeWaitTime >= MeleeReloadTime)
        {
            
            anim.SetTrigger("Attack");

            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

            
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                
                if (enemyCollider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.TakeDamage(Damage);
                    Debug.Log("Slashed " + enemy.name);
                }
            }

            
            meleeWaitTime = 0.0f;
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}