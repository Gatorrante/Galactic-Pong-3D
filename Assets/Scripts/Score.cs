using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int scorePlayerRight;
    public int scorePlayerLeft;
    public int playerLives = 4; // Vidas iniciales del jugador
    public GUIStyle style = new GUIStyle();
    public GUIStyle levelStyle = new GUIStyle();
    public GUIStyle livesStyle = new GUIStyle(); // Estilo para las vidas

    private int scoreToWin = 5; // Puntuación necesaria para ganar o reiniciar

    void Start()
    {
        // Configura el estilo de las vidas
        livesStyle.fontSize = 20;
        livesStyle.alignment = TextAnchor.MiddleCenter;
        livesStyle.normal.textColor = Color.white;

        // Configura el estilo del nivel y del puntaje
        levelStyle.fontSize = 20;
        levelStyle.alignment = TextAnchor.MiddleCenter;
        levelStyle.normal.textColor = Color.white;

        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
    }

    void OnGUI()
    {
        DisplayGUI();
    }

    void DisplayGUI()
    {
        float x = Screen.width / 2f;
        float y = Screen.height - 40f;
        float width = 200f;
        float height = 20f;
        float scoreWidth = 300f;
        float scoreHeight = 40f;

        string nivelText = SceneManager.GetActiveScene().name;
        GUI.Label(new Rect(x - (width / 2f), y, width, height), nivelText, levelStyle);

        float scoreY = y - scoreHeight - 10;
        string scoreText = scorePlayerLeft + " - " + scorePlayerRight;
        GUI.Label(new Rect(x - (scoreWidth / 2f), scoreY, scoreWidth, scoreHeight), scoreText, style);

        float livesY = scoreY - height - 10;
        string livesText = "Vidas: " + playerLives;
        GUI.Label(new Rect(x - (width / 2f), livesY, width, height), livesText, livesStyle);
    }

    public void PlayerScoresPoint()
    {
        scorePlayerLeft++;
        CheckGameStatus();
    }

    public void EnemyScoresPoint()
    {
        scorePlayerRight++;
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

