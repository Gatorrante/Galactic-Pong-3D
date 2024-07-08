using UnityEngine;

public class RotateEarth : MonoBehaviour
{
    public float TierraRotacionVel = 1f; 
    public float skyboxRotacionVel = 1f; 

    void Update()
    {
        // Rotación de la Tierra
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(-44.186f, currentRotation.eulerAngles.y + TierraRotacionVel * Time.deltaTime, -69.408f);
        transform.rotation = targetRotation;

        // Rotación del Skybox
        float skyboxRotation = skyboxRotacionVel * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") + skyboxRotation);
    }
}
