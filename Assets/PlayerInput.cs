using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Vector2 moveInput;

    public void Jump(InputAction.CallbackContext context)
    {
        if (!PlayerMovement.isWindUpActive)
        {
            if (context.performed)
            {
                playerMovement.OnJumpInput();
            }
            if (context.canceled)
            {
                playerMovement.OnJumpUpInput();
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        playerMovement.SetMoveInput(moveInput);
    }

    public void Dash()
    {
        if (!PlayerMovement.isWindUpActive)
        {
            playerMovement.OnDashInput();
        }
    }

}
