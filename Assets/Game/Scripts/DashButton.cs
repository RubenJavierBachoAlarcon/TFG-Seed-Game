using UnityEngine;
using UnityEngine.EventSystems;

public class DashButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public PlayerMovement playerMovement; // Asegúrate de asignar tu script de movimiento del jugador en el inspector de Unity

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMovement.OnDashInput(); // Este método debería comenzar el salto en tu script de jugador
    }
}