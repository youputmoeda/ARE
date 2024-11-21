using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerUIInput : MonoBehaviour, PlayerControls.IPlayerUIInteractionMapActions
{

    public bool escapePressed { get; private set; }
    
    private void OnEnable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized - cannot enable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.PlayerUIInteractionMap.Enable();
        PlayerInputManager.Instance.PlayerControls.PlayerUIInteractionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized - cannot disable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.PlayerUIInteractionMap.Disable();
        PlayerInputManager.Instance.PlayerControls.PlayerUIInteractionMap.RemoveCallbacks(this);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Tecla de pausa pressionada!");
        if (!context.performed)
            return;

        escapePressed = !escapePressed;

    }
}
