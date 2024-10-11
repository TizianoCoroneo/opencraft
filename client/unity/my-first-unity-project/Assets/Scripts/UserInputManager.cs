using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ScriptableObjects/UserInputManager")]
public class UserInputManager : ScriptableObject, PlayerInputActions.IPlayerActions, PlayerInputActions.IHotKeysActions
{
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<Vector2> LookEvent;
    public event UnityAction FireEvent;

    public event UnityAction ToggleThinClientEvent;

    private PlayerInputActions gameInput;

    private void Awake() { }
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
    private void OnDisable()
    {
        gameInput.Player.Disable();
        gameInput.HotKeys.Disable();
    }
    private void OnDestroy() { }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnToggleThinClient(InputAction.CallbackContext context)
    {
        if (ToggleThinClientEvent != null && context.phase == InputActionPhase.Performed)
            ToggleThinClientEvent?.Invoke();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (FireEvent != null && context.phase == InputActionPhase.Performed)
            FireEvent.Invoke();
    }
}
