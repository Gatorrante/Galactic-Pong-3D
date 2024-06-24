using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este es el comportamiento de la camara similar a Pong de play 1 xD
public class Camara : MonoBehaviour
{
    public Transform target; 

    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target); // asegurar que mire al objeto
        }
    }
}
