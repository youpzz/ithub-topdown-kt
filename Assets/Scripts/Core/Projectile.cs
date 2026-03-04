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
        rb.angularVelocity = Random.Range(-720, -360);
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;

        if (((1 << target.layer) & enemyLayer) == 0)
        {
            Destroy(gameObject);
            return;
        }

        target.GetComponentInParent<Health>()?.TakeDamage(damage);
        Destroy(gameObject);
    }

}
