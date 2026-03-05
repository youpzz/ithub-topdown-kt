using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    [Space(5)]

    [Header("Reload")]
    [SerializeField] private int maxAmmo = 5;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private AudioClip reloadSound;

    private float lastShotTime = 0f;
    [Space(5)]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject[] bulletPrefabs;
    
    [Space(5)]
    [Header("Smooth Gun Rotation")]
    [SerializeField] private Transform gunToRotate;
    [SerializeField] private float gunRotateTime = 0.15f;
    [SerializeField] private SpriteRenderer gunSprite;
    
    [Space(5)]
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private AudioClip[] shootSound;
    [SerializeField] private AudioClip noAmmoSound;
    [Space(5)]
    [SerializeField] private ParticleSystem shootFx;
    private int currentShot = 0;
    private int currentAmmo = 5;
    private bool isReloading = false;

    void Start()
    {
        PlayerInput.Instance.OnShoot += Shoot;
        PlayerInput.Instance.OnReload += StartReload;
    }

    void FixedUpdate()
    {
        RotateToMouse();
    }

    void Shoot()
    {
        if (isReloading) return;
        if (Time.time < lastShotTime + fireRate) return; // проверка задержки
        if (currentAmmo <= 0) { AudioManager.Instance.PlaySound(noAmmoSound, true); return; }

        lastShotTime = Time.time;

        Projectile projectile = Instantiate(bulletPrefabs[currentShot], shootPoint.position, shootPoint.rotation).GetComponent<Projectile>();

        Vector2 shootDir = shootPoint.up;
        projectile.Shoot(damage, shootDir, enemyLayer, this.gameObject);
        PlayShootSfx();
        CameraShake.Instance.TriggerShake();
        shootFx.Play();
        currentAmmo--;
        if (currentAmmo <= 0) StartReload();
    }

    void StartReload()
    {
        if (isReloading || currentAmmo == maxAmmo) return;

        isReloading = true;
        AudioManager.Instance.PlaySound(reloadSound, false);
        DOVirtual.DelayedCall(reloadTime, FinishReload);
    }

    void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);

        gunToRotate.transform.DORotateQuaternion(targetRotation, gunRotateTime)
            .SetEase(Ease.OutSine);

        // Отражение спрайта
        bool flipX = angle > -90f && angle < 90f;
        gunSprite.flipX = flipX;


    }

    void OnDisable()
    {
        if (PlayerInput.Instance != null)
        {
            PlayerInput.Instance.OnShoot -= Shoot;
            PlayerInput.Instance.OnReload -= StartReload;
        }
    }

    private void PlayShootSfx()
    {
        currentShot++;
        if (currentShot >= bulletPrefabs.Length) currentShot = 0;
        AudioManager.Instance.PlaySound(shootSound[currentShot], true);
        AudioManager.Instance.PlaySound(shootSfx, true);
    }

    public int GetAmmo() => currentAmmo;
    public int GetMaxAmmo() => maxAmmo;
    public bool IsReloading() => isReloading;
}
