using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmProyectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
    }
}
