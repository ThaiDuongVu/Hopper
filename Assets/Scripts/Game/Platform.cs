using UnityEngine;

public class Platform : MonoBehaviour
{
    public float minScale;
    public float maxScale;

    private void Start()
    {
        Scale();
    }

    // Generate a random scale along the x and z axis to increase game variety
    private void Scale()
    {
        float newScale = Random.Range(minScale, maxScale);

        Transform platformTransform = transform;
        Vector3 scale = platformTransform.localScale;

        platformTransform.localScale = new Vector3(newScale, scale.y, newScale);
    }
}
