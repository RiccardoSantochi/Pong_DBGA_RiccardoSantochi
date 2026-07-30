using UnityEngine;
//using System.Collections;
public class CameraShake : MonoBehaviour
{
    private Vector3 startPosition;

    private bool isShaking = false;
    private float timeRemaining;
    private float shakeIntensity;

    private void Start()
    {
        // Salvo la posizione iniziale della camera
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        // Controllo se la camera deve vibrare
        if (isShaking)
        {
            // Creo spostamenti casuali
            float xOffset = Random.Range(-shakeIntensity, shakeIntensity);
            float yOffset = Random.Range(-shakeIntensity, shakeIntensity);

            // Sposto la camera
            transform.localPosition = startPosition + new Vector3(xOffset, yOffset, 0f);

            // Diminuisco il tempo rimasto
            timeRemaining -= Time.deltaTime;

            // Controllo se la vibrazione è terminata
            if (timeRemaining <= 0f)
            {
                isShaking = false;

                // Riporto la camera alla posizione iniziale
                transform.localPosition = startPosition;
            }
        }
    }

    // uso questo metodo per richiamare la vibrazione della camera 
    public void StartShake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        timeRemaining = duration;
        isShaking = true;
    }
}
