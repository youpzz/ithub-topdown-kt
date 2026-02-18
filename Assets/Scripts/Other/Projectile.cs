using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    private float damage = 1;

    private Rigidbody2D rb;
    private LayerMask enemyLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(float damage_, Vector2 dir, LayerMask enemyLayer_)
    {
        damage = damage_;
        rb.linearVelocity = dir.normalized * speed;
        enemyLayer = enemyLayer_;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) == 0) return;

        collision.GetComponentInParent<Health>().TakeDamage(damage);
        Destroy(gameObject);


    }
}
