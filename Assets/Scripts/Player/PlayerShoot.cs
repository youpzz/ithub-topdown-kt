using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    private bool isShooting;

    void Start()
    {
        PlayerInput.Instance.OnShoot += Shoot;
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation).GetComponent<Projectile>();
        
        Vector2 shootDir = shootPoint.up;
        projectile.Shoot(shootDir);
    }
    

    void OnDisable()
    {
        if (PlayerInput.Instance != null) PlayerInput.Instance.OnShoot -= Shoot;
    }
}
