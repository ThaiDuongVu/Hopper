using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 direction => transform.up;

    // Direct arrow's rotation to touch's direction
    public void Redirect(Vector2 touchDirection)
    {
        transform.up = touchDirection;
    }
}
