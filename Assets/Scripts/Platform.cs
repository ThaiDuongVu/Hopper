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
        Vector3 scale = transform.localScale;

        transform.localScale = new Vector3(newScale, scale.y, newScale);
    }
}
