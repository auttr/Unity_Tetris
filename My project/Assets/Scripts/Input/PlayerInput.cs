using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, InputActions.IGameplayActions
{
    public static event UnityAction onMoveLeft = delegate { };
    public static event UnityAction onMoveRight = delegate { };
    public static event UnityAction onDrop = delegate { };
    public static event UnityAction onCancelDrop = delegate { };
    public static event UnityAction onRotate = delegate { };

    static InputActions inputActions;
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Gameplay.SetCallbacks(this);
    }
    private void OnEnable()
    {
        EnableGameplayInputs();
    }
    private void OnDisable()
    {
        DisableGameplayInputs();
    }
    public static void EnableGameplayInputs()
    {
        inputActions.Gameplay.Enable();
    }
    public static void DisableGameplayInputs()
    {
        inputActions.Gameplay.Disable();
    }
    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onDrop.Invoke();
        }
        if (context.canceled)
        {
            onCancelDrop.Invoke();  
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onMoveLeft.Invoke();
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onMoveRight.Invoke();
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onRotate.Invoke();
        }
    }
}
