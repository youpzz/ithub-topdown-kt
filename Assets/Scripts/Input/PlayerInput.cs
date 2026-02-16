using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    public event Action<Vector2> OnMove;
    public event Action OnShoot;

    private InputActions inputActions;


    void Awake()
    {
        Instance = this;

        inputActions = new InputActions();
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    private void HandleShoot(InputAction.CallbackContext context)
    {
        OnShoot?.Invoke();
    }

    void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += HandleMove;
        inputActions.Player.Shoot.performed += HandleShoot;
    }

    void OnDisable()
    {
        inputActions.Player.Move.performed -= HandleMove;
        inputActions.Player.Shoot.performed -= HandleShoot;

        inputActions.Disable();
    }

}
