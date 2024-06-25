using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreLevelManager : MonoBehaviour
{
    public int scoreToNextLevel = 5; // Puntos necesarios para pasar al siguiente nivel
    private int currentScore = 0;    // Puntuación actual, inicializada a 0

    // Método para incrementar la puntuación y verificar la condición del nivel
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        CheckForLevelCompletion();
    }

    // Verifica si los puntos alcanzados son suficientes para cambiar de nivel
    private void CheckForLevelCompletion()
    {
        if (currentScore >= scoreToNextLevel)
        {
            LoadNextLevel();
        }
    }

    // Carga el siguiente nivel
    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
