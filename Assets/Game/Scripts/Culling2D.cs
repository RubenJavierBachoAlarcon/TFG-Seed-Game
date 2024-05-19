using Cinemachine;
using UnityEngine;

public class Culling2D : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera cinemachineCamera;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            Vector3 viewPos = mainCamera.WorldToViewportPoint(obj.transform.position);
            if (viewPos.x > 1 || viewPos.x < 0 || viewPos.y > 1 || viewPos.y < 0)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}