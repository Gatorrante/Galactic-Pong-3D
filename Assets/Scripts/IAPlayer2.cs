using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPlayer2 : MonoBehaviour
{
    public float speed = 7f;  
    public Transform ball;    
    public float reactionDelay = 0.2f; // retraza la reaccion (Lo voy a usar para aumentar dificultad en niveles)
    public float reactionDistance = 5f; // Distancia a la que la IA empieza a reaccionar
    public float errorMargin = 0.5f; // Margen de error 

    private float nextActionTime = 0f;

    void Update()
    {
        
        if (Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + reactionDelay;

            // Mover al marciano si está cerca de la pelota en el eje z
            if (Mathf.Abs(transform.position.z - ball.position.z) <= reactionDistance)
            {
                //margen de error para que no sea tan perfecto
                float targetZ = ball.position.z + Random.Range(-errorMargin, errorMargin);
                Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, targetZ);
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
}
