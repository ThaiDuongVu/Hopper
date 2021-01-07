using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration;
    private float shakeIntensity;
    private float decreaseFactor;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        Randomize();
    }

    // When camera shakes, randomize its position by shake intensity
    private void Randomize()
    {
        // While shake duration is greater than 0
        if (shakeDuration > 0)
        {
            // Randomize position
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;

            // Decrease shake duration
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        // If shake duration reaches 0
        else
        {
            // Reset everything
            shakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    // Shake the camera
    public void Shake()
    {
        shakeDuration = 0.2f;
        shakeIntensity = 0.3f;
        decreaseFactor = 2f;
    }

    public void ShakeLight()
    {
        shakeDuration = 0.1f;
        shakeIntensity = 0.1f;
        decreaseFactor = 2f;
    }
}
