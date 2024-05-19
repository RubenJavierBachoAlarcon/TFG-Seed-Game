using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public PlayerMovement playerMovement; // Asegúrate de asignar tu script de movimiento del jugador en el inspector de Unity

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMovement.OnJumpInput(); // Este método debería comenzar el salto en tu script de jugador
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerMovement.OnJumpUpInput(); // Este método debería detener el salto en tu script de jugador
    }
}