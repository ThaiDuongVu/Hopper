using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float _shakeDuration;
    private float _shakeIntensity;
    private float _decreaseFactor;

    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
    }

    private void Update()
    {
        Randomize();
    }

    // When camera shakes, randomize its position by shake intensity
    private void Randomize()
    {
        if (_shakeDuration > 0)
        {
            transform.localPosition = _originalPosition + Random.insideUnitSphere * _shakeIntensity;
            _shakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
            transform.localPosition = _originalPosition;
        }
    }

    // Shake the camera
    public void Shake()
    {
        _shakeDuration = 0.2f;
        _shakeIntensity = 0.3f;
        _decreaseFactor = 2f;
    }

    public void ShakeLight()
    {
        _shakeDuration = 0.1f;
        _shakeIntensity = 0.1f;
        _decreaseFactor = 2f;
    }
}
