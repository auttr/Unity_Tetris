using System.Collections;
using System.Collections.Generic;
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
    public void OnDrop(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
