using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bordes : MonoBehaviour
{
    public enum ePlayer { Left, Right }
    public ePlayer player;
    public Score score;   //puntuación
    public AudioClip goalSound;  // gol
    private AudioSource audioSource;  //AudioSource

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        
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
            // Sonido de gol
            if (goalSound != null && audioSource != null)
            {
                audioSource.Play();
            }

            // Reset
            ball.transform.position = new Vector3(0f, 1f, 0f); 

            if (player == ePlayer.Right)
            {
                // Punto para el jugador izquierdo
                score.PlayerScoresPoint();
                Debug.Log("Punto para la tierra");
            }
            else if (player == ePlayer.Left)
            {
                // Punto jugador derecho
                score.EnemyScoresPoint();
                Debug.Log("Punto para los marcianos");
            }
        }
    }
}
