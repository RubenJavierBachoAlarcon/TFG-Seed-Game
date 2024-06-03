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
            if (context.started)
            {
                Debug.Log(playerMovement.IsJumping);
                if (playerMovement.IsJumping)
                {
                    playerMovement.OnDashInput();
                }
                else
                {
                    playerMovement.OnJumpInput();
                }
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
