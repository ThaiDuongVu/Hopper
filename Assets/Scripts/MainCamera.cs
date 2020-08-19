using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Animator _animator;
    private bool _isFocusing = true;

    public Transform followTarget;
    private const float InterpolationRatio = 0.2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Follow(followTarget);
    }

    // Set camera focus state
    public void SetFocus(bool value)
    {
        _animator.SetBool("focus", value);
        _isFocusing = value;
    }

    // Follow a target
    private void Follow(Transform target)
    {
        Vector3 position = transform.position;

        Vector3 lerpPosition;
        if (!_isFocusing)
        {
            lerpPosition = new Vector3(0f, target.position.y, position.z);
        } 
        else
        {
            lerpPosition = new Vector3(target.position.x, target.position.y, position.z);
        }

        position = Vector3.Lerp(position, lerpPosition, InterpolationRatio);

        transform.position = position;
    }
}
