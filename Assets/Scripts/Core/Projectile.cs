using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    private float damage = 1;

    [SerializeField] private GameObject[] particleOnMiss;
    [SerializeField] private GameObject[] paritcleOnHit;

    private Rigidbody2D rb;
    private LayerMask enemyLayer;
    private GameObject owner;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(float damage_, Vector2 dir, LayerMask enemyLayer_, GameObject owner_)
    {
        damage = damage_;
        rb.linearVelocity = dir.normalized * speed;
        enemyLayer = enemyLayer_;
        owner = owner_;
        Destroy(gameObject, lifeTime);
        float randomTorq = Random.Range(10, 30);
        bool dir_ = Random.Range(0, 100) <= 50;
        if (dir_) randomTorq *= -1;

        rb.AddTorque(randomTorq);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;

        if (target == owner || target.transform.IsChildOf(owner.transform))
        {
            Instantiate(particleOnMiss[UnityEngine.Random.Range(0, particleOnMiss.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (((1 << target.layer) & enemyLayer) == 0)
        {
            Instantiate(particleOnMiss[UnityEngine.Random.Range(0, particleOnMiss.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        Instantiate(paritcleOnHit[UnityEngine.Random.Range(0, paritcleOnHit.Length)], transform.position, Quaternion.identity);
        target.GetComponentInParent<Health>()?.TakeDamage(damage);
        Destroy(gameObject);
    }

}
