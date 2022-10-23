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
    public static event UnityAction onInstantDrop = delegate { };
    public static event UnityAction onHold = delegate { };

    public static bool keepMoveLeft = false;
    public static bool keepMoveRight = false;

    const float BUTTON_HOLD_TIME = 0.4f;

    WaitForSeconds waitForButtonHoldTime = new WaitForSeconds(BUTTON_HOLD_TIME);

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
            StartCoroutine(nameof(KeepMoveLeftCoroutine));
        }
        if (context.canceled)
        {
            StopCoroutine(nameof(KeepMoveLeftCoroutine));
            keepMoveLeft = false;
        }
    }
    IEnumerator KeepMoveLeftCoroutine()
    {
        yield return waitForButtonHoldTime;
        keepMoveLeft = true;
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onMoveRight.Invoke();
            StartCoroutine(nameof(KeepMoveRightCoroutine));
        }
        if (context.canceled)
        {
            StopCoroutine(nameof(KeepMoveRightCoroutine));
            keepMoveRight = false;
        }
    }
    IEnumerator KeepMoveRightCoroutine()
    {
        yield return waitForButtonHoldTime;
        keepMoveRight = true;
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onRotate.Invoke();
        }
    }

    public void OnInstantDrop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           onInstantDrop.Invoke();
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onHold.Invoke();
        }
    }
}
