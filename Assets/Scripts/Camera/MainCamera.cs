using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public bool isFollowing { get; set; }
    public Transform followTarget;

    private const float InterpolationRatio = 0.05f;

    public int currentDirection { get; set; }

    private void Update()
    {
        if (isFollowing && !(followTarget.Equals(null)))
        {
            Follow(followTarget);
        }
    }

    // Camera follow a target
    private void Follow(Transform target)
    {
        Vector3 targetPosition = target.position;
        Vector3 position = transform.position;

        Vector3 lerpPosition = currentDirection == 0 ? new Vector3(targetPosition.x - 5f, position.y, targetPosition.z - 50f) : new Vector3(targetPosition.x + 5f, position.y, targetPosition.z - 50f);
        transform.position = Vector3.Lerp(position, lerpPosition, InterpolationRatio);

        if (Mathf.Abs(transform.position.x - lerpPosition.x) < 0.1f && Mathf.Abs(transform.position.z - lerpPosition.z) < 0.1f)
        {
            isFollowing = false;
        }
    }
}
