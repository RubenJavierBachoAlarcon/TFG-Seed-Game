using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public PlayerMovement playerMovement; // Aseg�rate de asignar tu script de movimiento del jugador en el inspector de Unity

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMovement.OnJumpInput(); // Este m�todo deber�a comenzar el salto en tu script de jugador
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerMovement.OnJumpUpInput(); // Este m�todo deber�a detener el salto en tu script de jugador
    }
}