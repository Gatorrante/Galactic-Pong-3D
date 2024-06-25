using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para acceder a la información de las escenas

public class Score : MonoBehaviour
{
    public int scorePlayerRight;
    public int scorePlayerLeft;
    public GUIStyle style = new GUIStyle(); // Inicializa el estilo de texto para la GUI
    public GUIStyle levelStyle = new GUIStyle(); // Inicializa un nuevo estilo de texto para el nivel

    void Start()
    {
        // Configura el tamaño de la fuente para el nivel
        levelStyle.fontSize = 20; // Tamaño de fuente más pequeño para el nivel
        levelStyle.alignment = TextAnchor.MiddleCenter; // Centrar texto del nivel
        levelStyle.normal.textColor = Color.white; // Asegúrate de configurar un color de texto

        style.fontSize = 30; // Tamaño de fuente para el puntaje
        style.alignment = TextAnchor.MiddleCenter; // Centrar el puntaje
        style.normal.textColor = Color.white; // Configurar color de texto para el puntaje
    }

    void OnGUI()
    {
        float x = Screen.width / 2f;
        float y = Screen.height - 40f; // Posición inicial Y para el título del nivel, cerca del fondo de la pantalla

        float width = 200f; // Ancho más estrecho para el título del nivel
        float height = 20f; // Altura adecuada para el texto del nivel
        float scoreWidth = 300f; // Ancho para el puntaje
        float scoreHeight = 40f; // Altura para el puntaje

        // Obtener el nombre de la escena actual y usarlo como título del nivel
        string nivelText = SceneManager.GetActiveScene().name;

        // Mostrar el título del nivel en la parte inferior de la pantalla
        GUI.Label(new Rect(x - (width / 2f), y, width, height), nivelText, levelStyle);

        // Posición para el puntaje
        float scoreY = y - scoreHeight - 10; // Ajustar la posición Y para el puntaje para que aparezca arriba del nivel

        // Puntuación actual
        string scoreText = scorePlayerLeft + " - " + scorePlayerRight;

        // Mostrar la puntuación
        GUI.Label(new Rect(x - (scoreWidth / 2f), scoreY, scoreWidth, scoreHeight), scoreText, style);
    }
}
