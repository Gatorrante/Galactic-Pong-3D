using System.Collections.Generic;
using UnityEngine;

public class FanaticosController : MonoBehaviour
{
    public Transform pelota; //pelota
    public List<Transform> npcTransforms = new List<Transform>(); // Lista de  los NPCs
    public ePlayer jugador; // Enum para saber si es la barra izquierda o derecha

    private float distanciaMaxima = 7f; //
    private List<float> posicionesYIniciales = new List<float>();
    private float frecuenciaSaltoBase = 1f; 
    private float alturaSaltoBase = 0.25f; 
    private float frecuenciaSaltoMaxima = 15f; 
    private float emocionMinima = 0.2f;

    void Start()
    {
        // Guarda las posiciones Y iniciales de los NPCs
        foreach (Transform npc in npcTransforms)
        {
            posicionesYIniciales.Add(npc.position.y);
        }
    }

    void Update()
    {
        float distanciaMeta = 0f;
        float emocion = 0f;

        if (jugador == ePlayer.Left)
        {
            // Calcula la emoción basada en la distancia a la meta del jugador izquierdo
            distanciaMeta = Mathf.Clamp(pelota.position.x, -distanciaMaxima, 0);
            emocion = 1f - Mathf.Abs(distanciaMeta / distanciaMaxima);

            // Emoción mínima para el equipo contrario
            if (pelota.position.x > 0)
            {
                emocion = emocionMinima;
            }
        }
        else if (jugador == ePlayer.Right)
        {
            // Calcula la emoción basada en la distancia a la meta del jugador derecho
            distanciaMeta = Mathf.Clamp(pelota.position.x, 0, distanciaMaxima);
            emocion = 1f - Mathf.Abs(distanciaMeta / distanciaMaxima);

            // Emoción mínima para el equipo contrario
            if (pelota.position.x < 0)
            {
                emocion = emocionMinima;
            }
        }

        //simular saltos
        for (int i = 0; i < npcTransforms.Count; i++)
        {
            Transform npc = npcTransforms[i];
            float posicionYInicial = posicionesYIniciales[i];

            // Calculo basado en la emoción
            float alturaSaltoActual = alturaSaltoBase * emocion;
            float frecuenciaSaltoActual = Mathf.Min(frecuenciaSaltoBase * emocion * 10, frecuenciaSaltoMaxima);

            //NPC salta en eje Y
            float nuevaY = posicionYInicial + Mathf.Sin(Time.time * frecuenciaSaltoActual + i) * alturaSaltoActual;
            npc.position = new Vector3(npc.position.x, nuevaY, npc.position.z);
        }
    }
}
