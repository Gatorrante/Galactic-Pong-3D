using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bordes : MonoBehaviour
{
    public ePlayer player; 
    public Score score;   

    void OnCollisionEnter(Collision col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            ball.transform.position = new Vector3(0f, 1f, 0f);

            if (player == ePlayer.Right)
            {
                score.scorePlayerLeft++;
                Debug.Log("Punto para el jugador izquierdo");
            }
            else if (player == ePlayer.Left)
            {
                score.scorePlayerRight++;
                Debug.Log("Punto para el jugador derecho");
            }
        }
    }
}

