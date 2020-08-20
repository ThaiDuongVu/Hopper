using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public bool isFollowing { get; set; }
    public Transform followTarget;

    private const float InterpolationRatio = 0.05f;

    private void Update()
    {
        if (isFollowing)
        {
            Follow(followTarget);
        }
    }   

    private void Follow(Transform target)
    {
        Vector3 targetPosition = target.position;
        Vector3 position = transform.position;

        Vector3 lerpPosition = new Vector3(targetPosition.x - 5f, position.y, targetPosition.z - 50f);
        position = Vector3.Lerp(position, lerpPosition, InterpolationRatio);

        transform.position = position;

        if (Mathf.Abs(transform.position.x - lerpPosition.x) < 0.1f)
        {
            isFollowing = false;
        }
    }
}
