using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// A scriptable object that manages possible user inputs.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/UserInputManager")]
public class UserInputManager : ScriptableObject, PlayerInputActions.IPlayerActions, PlayerInputActions.IHotKeysActions
{
    /// <summary>
    /// Represents an input that changes the user's avatar location.
    /// </summary>
    public event UnityAction<Vector2> MoveEvent;

    /// <summary>
    /// Represents an input that changes the orientation of the user's camera.
    /// </summary>
    public event UnityAction<Vector2> LookEvent;

    /// <summary>
    /// Represents an input that interacts with the object the camera is
    /// currently looking at, or fires a weapon in the current direction.
    /// </summary>
    public event UnityAction FireEvent;

    /// <summary>
    /// Represents an input that makes the client switch between being a regular
    /// client and a thin client.
    /// </summary>
    [Obsolete("Switching between client and thin client should happen using HTTP requests.")]
    public event UnityAction ToggleThinClientEvent;

    /// <summary>
    /// Can be queried for player inputs and can be used to register callbacks
    /// on certain inputs.
    /// </summary>
    private PlayerInputActions gameInput;

    private void Awake() { }

    /// <summary>
    /// Enables inputs and registers callbacks.
    /// </summary>
    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new PlayerInputActions();
            gameInput.Player.SetCallbacks(this);
            gameInput.HotKeys.SetCallbacks(this);
        }
        gameInput.Player.Enable();
        gameInput.HotKeys.Enable();
    }

    /// <summary>
    ///  Disables user inputs.
    /// </summary>
    private void OnDisable()
    {
        gameInput.Player.Disable();
        gameInput.HotKeys.Disable();
    }
    private void OnDestroy() { }

    /// <summary>
    /// Invokes the callbacks registered on <see cref="MoveEvent"/> when a move
    /// input is received.
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }


    /// <summary>
    /// Invokes the callbacks registered on <see cref="LookEvent"/> when a look
    /// input is received.
    /// </summary>
    /// <param name="context"></param>
    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }


    /// <summary>
    /// Toggles this client between being a regular client and a thin client
    /// when a <see cref="ToggleThinClientEvent"/> input is received.
    /// </summary>
    /// <param name="context"></param>
    [Obsolete("Switching between client and thin client should happen using HTTP requests.")]
    public void OnToggleThinClient(InputAction.CallbackContext context)
    {
        if (ToggleThinClientEvent != null && context.phase == InputActionPhase.Performed)
            ToggleThinClientEvent?.Invoke();
    }

    /// <summary>
    /// Invokes the callbacks registered on <see cref="FireEvent"/> when a fire
    /// input is received.
    /// </summary>
    /// <param name="context"></param>
    public void OnFire(InputAction.CallbackContext context)
    {
        if (FireEvent != null && context.phase == InputActionPhase.Performed)
            FireEvent.Invoke();
    }
}
