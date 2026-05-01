using UnityEngine;

public class EnemyBoss : Enemy
{
        [Header("Settings")]
        public Player player;
        public float speed = 2.0f;


        [SerializeField] private float detectRange = 5.0f;

        void Start()
        {

            base.Initialize(80, 20);
            this.MpDrop = 10;


            if (player == null)
            {
                player = FindAnyObjectByType<Player>();
            }
        }
    
    public void AnimAttack()
    {
        anim.SetTrigger("Attack");
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

        //private void OnCollisionEnter2D(Collision2D other)
        //{
        //    if (other.gameObject.TryGetComponent<Player>(out Player player))
        //    {
        //        anim.SetTrigger("Attack");
        //    }
        //}

        private void FixedUpdate()
        {
            Behavior();

            if (this.IsDead())
            {

                Debug.Log("1. Enemy ������� (��� if IsDead)");

                Player player = FindAnyObjectByType<Player>();

                if (player != null)
                {
                    // �ش B: �� Player
                    Debug.Log("2. �� Player ���� ����: " + player.name);
                    player.AddMp(this.MpDrop);
                    Destroy(this.gameObject);
                }
                else
                { // �ش C: �� Player �����
                    Debug.LogError("3. �� Player �����!!! (player �� null)");
                }

            }
        }
    }
  