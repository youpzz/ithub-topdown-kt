using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        PlayerInput.Instance.OnMove += SetMoveInput;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void SetMoveInput(Vector2 input)
    {
        moveInput = input.normalized;
    }

    void OnDisable()
    {
        if (PlayerInput.Instance != null) PlayerInput.Instance.OnMove -= SetMoveInput;
    }
}
