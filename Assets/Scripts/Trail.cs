using UnityEngine;

public class Trail : MonoBehaviour
{
    public Transform followTarget;
    private const float InterpolationRatio = 0.2f;

    private void Update()
    {
        if (!(followTarget.Equals(null)))
        {
            Follow(followTarget);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Follow an object ot leave a trail
    private void Follow(Transform target)
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, InterpolationRatio);
    }
}
