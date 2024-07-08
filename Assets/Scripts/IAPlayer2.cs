using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPlayer2 : MonoBehaviour
{
    public float velocidad = 7f;  
    public Transform pelota;    
    public float retrasoReaccion = 0.2f; // Retrasa la reacción (usado para aumentar dificultad en niveles)
    public float distanciaReaccion = 5f; // Distancia a la que la IA empieza a reaccionar
    public float margenError = 0.5f; // Margen de error 

    private float tiempoProximaAccion = 0f;

    void Update()
    {
        if (Time.time >= tiempoProximaAccion)
        {
            tiempoProximaAccion = Time.time + retrasoReaccion;

            // Mover al jugador AI si está cerca de la pelota en el eje Z
            if (Mathf.Abs(transform.position.z - pelota.position.z) <= distanciaReaccion)
            {
                // Margen de error para que no sea tan perfecto
                float targetZ = pelota.position.z + Random.Range(-margenError, margenError);
                targetZ = Mathf.Clamp(targetZ, -3.5f, 3.5f); // Limitar el movimiento entre -3.5 y 3.5 en el eje Z

                Vector3 posicionObjetivo = new Vector3(transform.position.x, transform.position.y, targetZ);
                transform.position = Vector3.Lerp(transform.position, posicionObjetivo, velocidad * Time.deltaTime);
            }
        }
    }
}
