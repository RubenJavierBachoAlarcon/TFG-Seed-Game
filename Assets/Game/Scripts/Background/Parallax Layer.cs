using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{

    [Range(0.1f, 2.0f)] // Intervalo típico para el factor de parallax
    public float parallaxFactor = 0.5f;

    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * (parallaxFactor / 6.5f);

        transform.localPosition = newPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
