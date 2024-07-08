using UnityEngine;
using UnityEngine.UI;

public class FloatingLogo : MonoBehaviour
{
    public float floatSpeed = 1f; 
    public float floatAmount = 5f; 

    private RectTransform rectTransform;
    private Vector3 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        rectTransform.anchoredPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
