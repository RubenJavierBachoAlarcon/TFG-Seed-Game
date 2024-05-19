using UnityEngine;
using UnityEngine.EventSystems;

public class DashButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public PlayerMovement playerMovement; // Aseg�rate de asignar tu script de movimiento del jugador en el inspector de Unity

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMovement.OnDashInput(); // Este m�todo deber�a comenzar el salto en tu script de jugador
    }
}