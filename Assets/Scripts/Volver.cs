using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Volver : MonoBehaviour
{
  public void RegresarAlMenu()
    {
        SceneManager.LoadScene(0); // Carga la escena con índice 0 (el menú principal)
    }
    
}
