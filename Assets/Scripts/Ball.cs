using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 ImpulsoInicial;
    public float colorChangeInterval = 0.5f; //se puede ajustar
    public Light ballLight; // luz
    private Renderer ballRenderer;
    public float maxSpeed = 30f; // Velo max.
    public Vector3 initialPosition; // Posición inicial de lota

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(ImpulsoInicial, ForceMode.Impulse);
        ballRenderer = GetComponent<Renderer>();

        // Guarda pos inicial de la lota
        initialPosition = transform.position;

        // metodos de las luces
        GameObject lightGameObject = new GameObject("Ball Light");
        ballLight = lightGameObject.AddComponent<Light>();
        ballLight.color = Color.white;
        ballLight.intensity = 2f;
        lightGameObject.transform.SetParent(transform);
        lightGameObject.transform.localPosition = Vector3.zero;

        StartCoroutine(ChangeColor());
    }

    // Coroutine que cambia color a la pelota (como en el juego pong original)
    IEnumerator ChangeColor()
    {
        while (true)
        {
            // Cambiar a un color aleatorio
            Color newColor = new Color(Random.value, Random.value, Random.value);
            ballRenderer.material.color = newColor;
            ballLight.color = newColor; // Sincronizar el color de la luz con la pelota

            // Esperar el intervalo antes de cambiar de color nuevamente
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }

    void FixedUpdate()
    {
        // limitar velocidad si la velocidad es mayor que la velocidad máxima
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Update()
    {
        //luz
        ballLight.transform.position = transform.position;
    }

    // reinicia pelota
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            // Reinicia posicion y vel
            transform.position = initialPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            //impulso inicial
            rb.AddForce(ImpulsoInicial, ForceMode.Impulse);
        }
        else
        {
           
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }


    //resetear pelota con voz
     public void ResetBall()
    {
        transform.position = initialPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(ImpulsoInicial, ForceMode.Impulse);
    }
}
