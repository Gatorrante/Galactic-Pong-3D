using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bordes : MonoBehaviour
{
    public enum ePlayer { Left, Right }
    public ePlayer player; 
    public Score score;   // Referencia al script de puntuación

    void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            // Resetea la posición de la pelota al centro
            ball.transform.position = new Vector3(0f, 1f, 0f); 

            if (player == ePlayer.Right)
            {
                // Punto para el jugador izquierdo porque el borde derecho fue golpeado
                score.scorePlayerLeft++;
                score.PlayerScoresPoint();
                Debug.Log("Punto para el jugador izquierdo");
            }
            else if (player == ePlayer.Left)
            {
                // Punto para el jugador derecho porque el borde izquierdo fue golpeado
                score.scorePlayerRight++;
                score.EnemyScoresPoint();
                Debug.Log("Punto para el jugador derecho");
            }
        }
    }
}
