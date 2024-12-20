using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionsMapActions
{
    #region Class Variables
    private PlayerLocomotionInput _playerLocomotionInput;
    private PlayerState _playerState;
    public bool GatherPressed { get; private set; }
    public bool InteractPressed { get; set; }
    public bool AimingPressed { get; private set; }
    public bool AttackPressed { get; set; }
    public bool ChangeGravityPressed { get; set; }
    #endregion

    #region Startup
    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _playerState = GetComponent<PlayerState>();
    }
    private void OnEnable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized - cannot enable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.Enable();
        PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized - cannot disable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.Disable();
        PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.RemoveCallbacks(this);
    }
    #endregion

    #region Update
    private void Update()
    {
        if (_playerLocomotionInput.MovementInput != Vector2.zero ||
            _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping ||
            _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling)
        {
            GatherPressed = false;
        }
    }

    private void LateUpdate()
    {
        AttackPressed = false;
    }

    public void SetGatherPressedFalse()
    {
        GatherPressed = false;
    }

    public void SetAttackPressedFalse()
    {
        AttackPressed = false;
    }
    #endregion

    #region Input Callbacks
    public void OnGathering(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        GatherPressed = true;
    }

    public void OnAttacking(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        AttackPressed = true;
    }

    public void OnAiming(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            AimingPressed = false;
            return;
        }

        AimingPressed = true;
    }

    public void OnChangeGravity(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            ChangeGravityPressed = false;
            return;
        }

        ChangeGravityPressed = true;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            InteractPressed = false;
            return;
        }

        InteractPressed = true;
    }
    #endregion
}
