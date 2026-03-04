using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerShoot playerShoot;
    private Animator animator;

    void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        PlayerInput.Instance.OnShoot += AnimateShoot;
    }

    void AnimateShoot()
    {
        animator.SetTrigger("Shoot");
    }

    void OnDestroy()
    {
        PlayerInput.Instance.OnShoot -= AnimateShoot;
    }
}
