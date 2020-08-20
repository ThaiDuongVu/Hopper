using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float _shakeDuration = 0f;
    private float _shakeAmount = 0.7f;
    private float _decreaseFactor = 1.0f;

    private Vector3 _originalPosition;

    private void OnEnable()
    {
        _originalPosition = transform.position;
    }

    private void Update()
    {
        if (_shakeDuration > 0)
        {
            transform.localPosition = _originalPosition + Random.insideUnitSphere * _shakeAmount;
            _shakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
            transform.localPosition = _originalPosition;
        }
    }

    public void Shake()
    {
        _shakeDuration = 0.2f;
        _shakeAmount = 0.3f;
        _decreaseFactor = 2f;
    }
}
