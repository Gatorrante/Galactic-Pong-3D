using System.Collections.Generic;
using UnityEngine;

public class FanaticosController : MonoBehaviour
{
    public Transform pelota; // Referencia a la pelota
    public List<Transform> npcTransforms = new List<Transform>(); // Lista de transforms de los NPCs
    public ePlayer jugador; // Enum para saber si es la barra izquierda o derecha

    private float distanciaMaxima = 7f; // Distancia máxima a la meta
    private List<float> posicionesYIniciales = new List<float>(); // Lista de posiciones Y iniciales de los NPCs
    private float frecuenciaSaltoBase = 1f; // Frecuencia base de los saltos (ajustada a la mitad)
    private float alturaSaltoBase = 0.25f; // Altura base de los saltos (ajustada a la mitad)
    private float frecuenciaSaltoMaxima = 15f; // Frecuencia máxima de los saltos (ajustada a la mitad)
    private float emocionMinima = 0.2f; // Emoción mínima del equipo contrario

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

        // Ajusta la posición Y de los NPCs para simular saltos
        for (int i = 0; i < npcTransforms.Count; i++)
        {
            Transform npc = npcTransforms[i];
            float posicionYInicial = posicionesYIniciales[i];

            // Calcula la altura y frecuencia del salto basado en la emoción
            float alturaSaltoActual = alturaSaltoBase * emocion;
            float frecuenciaSaltoActual = Mathf.Min(frecuenciaSaltoBase * emocion * 10, frecuenciaSaltoMaxima);

            // Mueve el NPC en el eje Y para simular un salto
            float nuevaY = posicionYInicial + Mathf.Sin(Time.time * frecuenciaSaltoActual + i) * alturaSaltoActual;
            npc.position = new Vector3(npc.position.x, nuevaY, npc.position.z);
        }
    }
}
