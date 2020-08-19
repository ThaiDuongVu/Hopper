using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Animator _animator;

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
    }

    // Follow a target
    private void Follow(Transform target)
    {
        Vector3 lerpPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, lerpPosition, InterpolationRatio);
    }
}
