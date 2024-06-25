using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bordes : MonoBehaviour
{
    public enum ePlayer { Left, Right }
    public ePlayer player; 
    public Score score;   // Referencia al script de puntuación
    public ScoreLevelManager scoreManager;  // Referencia al manager de cambio de nivel

    void OnCollisionEnter(Collision col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            // Resetea la posición de la pelota al centro
            ball.transform.position = new Vector3(0f, 1f, 0f);

            // Verifica qué jugador anotó y actualiza la puntuación correspondiente
            if (player == ePlayer.Right)
            {
                score.scorePlayerLeft++;
                scoreManager.AddScore(1);  // Añade puntuación al manager de nivel
                Debug.Log("Punto para el jugador izquierdo");
            }
            else if (player == ePlayer.Left)
            {
                score.scorePlayerRight++;
                scoreManager.AddScore(1);  // Añade puntuación al manager de nivel
                Debug.Log("Punto para el jugador derecho");
            }
        }
    }
}
