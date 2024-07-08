using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int scorePlayerRight;
    public int scorePlayerLeft;
    public int playerLives = 4; // Vidas iniciales del jugador
    public GUIStyle livesStyle = new GUIStyle(); // Estilo para las vidas

    private int scoreToWin = 5; // Puntuación necesaria para ganar o reiniciar

    // Referencias a los objetos TextMesh
    public TextMesh puntajeRightTextMesh;
    public TextMesh puntajeLeftTextMesh;

    void Start()
    {
        // Configura el estilo de las vidas
        livesStyle.fontSize = 20;
        livesStyle.alignment = TextAnchor.MiddleCenter;
        livesStyle.normal.textColor = Color.white;

        // Asigna las referencias a los objetos TextMesh
        puntajeRightTextMesh = GameObject.Find("Puntaje_R").GetComponent<TextMesh>();
        puntajeLeftTextMesh = GameObject.Find("Puntaje_L").GetComponent<TextMesh>();

        // Inicializa los valores de los TextMesh
        UpdateScoreText();
    }

    void OnGUI()
    {
        DisplayLives();
    }

    void DisplayLives()
    {
        float x = Screen.width / 2f;
        float y = Screen.height - 40f;
        float width = 200f;
        float height = 20f;

        string livesText = "Vidas: " + playerLives;
        GUI.Label(new Rect(x - (width / 2f), y, width, height), livesText, livesStyle);
    }

    void UpdateScoreText()
    {
        // Actualiza el texto de los TextMesh con los puntajes actuales
        puntajeRightTextMesh.text = scorePlayerRight.ToString();
        puntajeLeftTextMesh.text = scorePlayerLeft.ToString();
    }

    public void PlayerScoresPoint()
    {
        scorePlayerLeft++;
        UpdateScoreText();
        CheckGameStatus();
    }

    public void EnemyScoresPoint()
    {
        scorePlayerRight++;
        UpdateScoreText();
        LoseLife();
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        if (scorePlayerLeft >= scoreToWin)
        {
            LoadNextLevel();
        }
        else if (scorePlayerRight >= scoreToWin)
        {
            RestartLevel();
        }
    }

    public void LoseLife()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            RestartLevel();
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
