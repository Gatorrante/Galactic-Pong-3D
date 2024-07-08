using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int scorePlayerRight;
    public int scorePlayerLeft;
    public int playerLives = 4; //Vidas 
    public GUIStyle livesStyle = new GUIStyle(); 

    private int scoreToWin = 5; 
    public TextMesh puntajeRightTextMesh;
    public TextMesh puntajeLeftTextMesh;

    void Start()
    {
        // Configura el estilo de las vidas
        livesStyle.fontSize = 20;
        livesStyle.alignment = TextAnchor.MiddleCenter;
        livesStyle.normal.textColor = Color.white;

        //Texto referenciados
        puntajeRightTextMesh = GameObject.Find("Puntaje_R").GetComponent<TextMesh>();
        puntajeLeftTextMesh = GameObject.Find("Puntaje_L").GetComponent<TextMesh>();

        // Inicializa los valores de TextMesh
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
        // Actualiza textmesh a actuales
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
