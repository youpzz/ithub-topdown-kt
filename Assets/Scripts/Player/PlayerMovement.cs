using UnityEngine;
using UnityEngine.InputSystem;

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
        RotateToMouse();
    }

    void SetMoveInput(Vector2 input)
    {
        moveInput = input.normalized;
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f;
    }

    void OnDisable()
    {
        if (PlayerInput.Instance != null) PlayerInput.Instance.OnMove -= SetMoveInput;
    }
}
