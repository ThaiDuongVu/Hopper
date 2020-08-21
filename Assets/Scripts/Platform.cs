using UnityEngine;

public class Platform : MonoBehaviour
{
    public float minScale;
    public float maxScale;

    private void Start()
    {
        Scale();
    }

    // Generate a random scale along the x and z axis
    private void Scale()
    {
        float newScale = Random.Range(minScale, maxScale);

        Transform transform1 = transform;
        Vector3 scale = transform1.localScale;

        transform1.localScale = new Vector3(newScale, scale.y, newScale);
    }
}
