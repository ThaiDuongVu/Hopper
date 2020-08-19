using UnityEngine;

public class Trail : MonoBehaviour
{
    public Transform followTarget;
    private const float InterpolationRatio = 0.15f;

    private void Update()
    {
        Follow(followTarget);
    }

    // Follow a target
    private void Follow(Transform target)
    {
        Vector3 position = transform.position;

        Vector3 lerpPosition = lerpPosition = new Vector3(target.position.x, target.position.y, position.z);
        position = Vector3.Lerp(position, lerpPosition, InterpolationRatio);

        transform.position = position;
    }
}
