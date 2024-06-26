using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bordes : MonoBehaviour
{
    public enum ePlayer { Left, Right }  // Identifica a qué jugador beneficia cada gol
    public ePlayer player; 
    public Score score;   // Referencia al script de puntuación

    private bool hasScored = false; // Flag para evitar duplicar puntos durante la misma colisión

    void OnCollisionEnter(Collision col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();
        if (ball != null && !hasScored)
        {
            hasScored = true; // Asegura que esta colisión solo cuente una vez

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

    void OnCollisionExit(Collision col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            hasScored = false; // Permite que un nuevo gol sea detectado una vez que la pelota sale
        }
    }
}
