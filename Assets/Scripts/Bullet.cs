using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerManager manager;

    [SerializeField] float speed = 12f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        manager = FindFirstObjectByType<PlayerManager>();
    }

    
    public void SetDirection(Vector2 dir)
    {
        if (rb == null) return;

        rb.linearVelocity = dir.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<EnemyHealth>() != null)
                collision.GetComponent<EnemyHealth>().TakeDamage();
            DestroySelf();
        }
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > 20f || Mathf.Abs(transform.position.y) > 20f)
        {
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        if (manager != null)
        {
            manager.OnBulletDestroyed(gameObject);
        }

        Destroy(gameObject);
    }
}