using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPlayer2 : MonoBehaviour
{
    [Header("Configuración Básica")]
    public float velocidad = 7f;  
    public Transform pelota;
    
    [Header("Dificultad")]
    [Range(0f, 1f)]
    public float nivelHabilidad = 0.7f; // 0 = muy fácil, 1 = perfecto
    public float retrasoReaccion = 0.2f;
    public float distanciaActivacion = 8f; // Distancia para empezar a moverse
    
    [Header("Límites de Movimiento")]
    public float limiteZMin = -3.5f;
    public float limiteZMax = 3.5f;
    
    [Header("Predicción")]
    public bool usarPrediccion = true;
    public float factorPrediccion = 0.5f; // Qué tan adelante predice
    
    private Rigidbody rbPelota;
    private Vector3 posicionObjetivo;
    private float tiempoUltimaActualizacion;
    private bool pelotaVieneCerca;

    void Start()
    {
        rbPelota = pelota.GetComponent<Rigidbody>();
        posicionObjetivo = transform.position;
    }

    void Update()
    {
        // Verificar si la pelota se acerca a la IA
        pelotaVieneCerca = VerificarDireccionPelota();
        
        // Actualizar objetivo según el retraso de reacción
        if (Time.time >= tiempoUltimaActualizacion + retrasoReaccion)
        {
            tiempoUltimaActualizacion = Time.time;
            ActualizarPosicionObjetivo();
        }
        
        // Mover hacia el objetivo
        MoverHaciaObjetivo();
    }

    bool VerificarDireccionPelota()
    {
        if (rbPelota == null) return false;
        
        // Verificar si la pelota se mueve hacia la IA (ajusta según tu setup)
        float distancia = pelota.position.x - transform.position.x;
        bool direccionCorrecta = rbPelota.linearVelocity.x > 0; // Asume que la IA está en X positivo
        
        return Mathf.Abs(distancia) <= distanciaActivacion && direccionCorrecta;
    }

    void ActualizarPosicionObjetivo()
    {
        float targetZ;
        
        if (pelotaVieneCerca && usarPrediccion)
        {
            // Predecir dónde estará la pelota
            targetZ = PredecirPosicionPelota();
        }
        else
        {
            // Seguir la posición actual
            targetZ = pelota.position.z;
        }
        
        // Aplicar error basado en habilidad (menor habilidad = más error)
        float errorMax = (1f - nivelHabilidad) * 2f;
        targetZ += Random.Range(-errorMax, errorMax);
        
        // Limitar movimiento
        targetZ = Mathf.Clamp(targetZ, limiteZMin, limiteZMax);
        
        posicionObjetivo = new Vector3(transform.position.x, transform.position.y, targetZ);
    }

    float PredecirPosicionPelota()
    {
        if (rbPelota == null) return pelota.position.z;
        
        // Calcular tiempo hasta que llegue a la posición de la IA
        float distanciaX = Mathf.Abs(pelota.position.x - transform.position.x);
        float velocidadX = Mathf.Abs(rbPelota.linearVelocity.x);
        
        if (velocidadX < 0.1f) return pelota.position.z; // Evitar división por cero
        
        float tiempoLlegada = distanciaX / velocidadX;
        
        // Predecir posición futura en Z
        float posicionPredecidaZ = pelota.position.z + (rbPelota.linearVelocity.z * tiempoLlegada * factorPrediccion);
        
        // Simular rebotes en los límites (básico)
        while (posicionPredecidaZ < limiteZMin || posicionPredecidaZ > limiteZMax)
        {
            if (posicionPredecidaZ < limiteZMin)
                posicionPredecidaZ = limiteZMin + (limiteZMin - posicionPredecidaZ);
            else if (posicionPredecidaZ > limiteZMax)
                posicionPredecidaZ = limiteZMax - (posicionPredecidaZ - limiteZMax);
        }
        
        return posicionPredecidaZ;
    }

    void MoverHaciaObjetivo()
    {
        // Solo moverse si la pelota viene cerca o ya está activa
        if (!pelotaVieneCerca && Vector3.Distance(transform.position, posicionObjetivo) < 0.5f)
        {
            return; // No perseguir si la pelota no viene
        }
        
        // Velocidad ajustada por habilidad
        float velocidadEfectiva = velocidad * Mathf.Lerp(0.5f, 1f, nivelHabilidad);
        
        // Movimiento suave
        float newZ = Mathf.MoveTowards(transform.position.z, posicionObjetivo.z, velocidadEfectiva * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    // Método opcional para ajustar dificultad dinámicamente
    public void AjustarDificultad(int nivel)
    {
        switch(nivel)
        {
            case 1: // Fácil
                nivelHabilidad = 0.4f;
                retrasoReaccion = 0.3f;
                velocidad = 5f;
                break;
            case 2: // Medio
                nivelHabilidad = 0.6f;
                retrasoReaccion = 0.2f;
                velocidad = 7f;
                break;
            case 3: // Difícil
                nivelHabilidad = 0.85f;
                retrasoReaccion = 0.1f;
                velocidad = 9f;
                break;
        }
    }
}