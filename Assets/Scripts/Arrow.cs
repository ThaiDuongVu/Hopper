using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _angularSpeed = 50f;
    private Quaternion _defaultRotation;

    private void Start()
    {
        _defaultRotation = transform.rotation;
    }

    private void Update()
    {
        Rotate();
    }

    // Rotate arrow left & right
    private void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, _angularSpeed) * Time.deltaTime, Space.Self);
    }

    // Reset rotation
    public void Reset()
    {
        transform.rotation = _defaultRotation;
    }

    private void ChangeRotationDirection()
    {

    }
}
