using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public bool isFollowing { get; set; }
    public Transform followTartget;

    private void Update()
    {
        if (isFollowing)
        {
            Follow(followTartget);
        }
    }   

    private void Follow(Transform target)
    {
        Vector3 lerpPosition = new Vector3(target.position.x - 5f, transform.position.y, target.position.z - 50f);

        transform.position = Vector3.Lerp(transform.position, lerpPosition, 0.1f);

        if (Mathf.Abs(transform.position.x - lerpPosition.x) < 0.1f)
        {
            isFollowing = false;
            Debug.Log(false);
        }
    }
}
