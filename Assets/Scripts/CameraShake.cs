using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 _originalPosition;

    private float _shakeDuration;
    private float _shakeAmount;
    private float _decreaseFactor;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    private void Update()
    {
        Randomize();
    }

    // Randomize position when a shake method is called
    private void Randomize()
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

    #region Shake Methods

    public void ShakeMicro()
    {
        _shakeDuration = 0.2f;
        _shakeAmount = 0.2f;
        _decreaseFactor = 2f;
    }

    public void ShakeLight()
    {
        _shakeDuration = 0.3f;
        _shakeAmount = 0.4f;
        _decreaseFactor = 2f;
    }

    public void ShakeRough()
    {
        _shakeDuration = 0.4f;
        _shakeAmount = 0.6f;
        _decreaseFactor = 2f;
    }

    #endregion
}