using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    public Button myButton;
    public Button creditsButton;
    public CanvasGroup canvasGroup;
    public Camera mainCamera;
    public Vector3 targetPosition = new Vector3(0.3f, 11.48f, -13.92f);
    public Quaternion targetRotation = Quaternion.Euler(44.554f, -0.069f, 0f);
    public float fadeDuration = 1f;
    public float cameraMoveDuration = 1f;
    
    void Start()
    {
        myButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(1)));
        creditsButton.onClick.AddListener(() => StartCoroutine(HandleCreditsClick()));
    }

    IEnumerator HandleButtonClick(int levelIndex)
    {
        // Difumina menú
        float fadeSpeed = 1f / fadeDuration;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0;

        // movimiento de camarita
        float elapsedTime = 0;
        Vector3 startingPosition = mainCamera.transform.position;
        Quaternion startingRotation = mainCamera.transform.rotation;

        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / cameraMoveDuration);
            mainCamera.transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / cameraMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
        mainCamera.orthographic = false;

        // Esperar 1 segundo antes de cargar el nivel
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator HandleCreditsClick()
    {
        // Difuminar el menú
        float fadeSpeed = 1f / fadeDuration;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0;

        // Espera de carga créditos
        yield return new WaitForSeconds(0.5f);

        // Cargar los créditos (index)
        SceneManager.LoadScene(4);
    }
}
