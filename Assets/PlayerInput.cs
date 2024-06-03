using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Vector2 moveInput;
    public string level;
    private PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

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

    public void NextLevel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = 1;
            FadeManager.Instance.FadeToScene(level);
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.timeScale == 0)
            {
                pauseMenu.ResumeGame();
            }
            else
            {
                pauseMenu.PauseGame();
            }
        }
    }
}
