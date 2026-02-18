using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    private Rigidbody2D rb;
    private LayerMask enemyLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 dir, LayerMask enemyLayer_)
    {
        rb.linearVelocity = dir.normalized * speed;
        enemyLayer = enemyLayer_;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != enemyLayer)
        {
            Destroy(gameObject);
            // урон прикрутить
        }
    }
}
