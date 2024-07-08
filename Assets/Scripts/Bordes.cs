using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bordes : MonoBehaviour
{
    public enum ePlayer { Left, Right }
    public ePlayer player;
    public Score score;   // Referencia al script de puntuación
    public AudioClip goalSound;  // Sonido de gol
    private AudioSource audioSource;  // Referencia al componente AudioSource

    void Start()
    {
        // Obtener el componente AudioSource del objeto actual
        audioSource = GetComponent<AudioSource>();
        // Asignar el clip de sonido si es necesario
        if (goalSound != null)
        {
            audioSource.clip = goalSound;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            // Reproducir el sonido de gol si está asignado
            if (goalSound != null && audioSource != null)
            {
                audioSource.Play();
            }

            // Resetea la posición de la pelota al centro
            ball.transform.position = new Vector3(0f, 1f, 0f); 

            if (player == ePlayer.Right)
            {
                // Punto para el jugador izquierdo porque el borde derecho fue golpeado
                score.PlayerScoresPoint();
                Debug.Log("Punto para la tierra");
            }
            else if (player == ePlayer.Left)
            {
                // Punto para el jugador derecho porque el borde izquierdo fue golpeado
                score.EnemyScoresPoint();
                Debug.Log("Punto para los marcianos");
            }
        }
    }
}
