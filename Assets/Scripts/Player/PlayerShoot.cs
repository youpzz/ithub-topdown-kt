using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    [Space(5)]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [Space(5)]
    [Header("Smooth Gun Rotation")]
    [SerializeField] private Transform gunToRotate;
    [SerializeField] private float gunRotateTime = 0.15f;
    
    [Space(5)]
    [SerializeField] private AudioClip[] shootSound;
    private int currentShootSound = 0;

    void Start()
    {
        PlayerInput.Instance.OnShoot += Shoot;
    }

    void FixedUpdate()
    {
        RotateToMouse();
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation).GetComponent<Projectile>();
        
        Vector2 shootDir = shootPoint.up;
        projectile.Shoot(damage, shootDir, enemyLayer);
        PlayShootSfx();
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);

        gunToRotate.transform.DORotateQuaternion(targetRotation, gunRotateTime)
            .SetEase(Ease.OutSine);
    }

    void OnDisable()
    {
        if (PlayerInput.Instance != null) PlayerInput.Instance.OnShoot -= Shoot;
    }

    private void PlayShootSfx()
    {
        currentShootSound++;
        if (currentShootSound >= shootSound.Length) currentShootSound = 0;
        AudioManager.Instance.PlaySound(shootSound[currentShootSound], true);
    }
}
