using UnityEngine;

public class Platform : MonoBehaviour
{
    public float minScale;
    public float maxScale;

    private void Start()
    {
        Scale();
    }

    private void Update()
    {
        
    }

    // Generate a random scale along the x and z axis
    private void Scale()
    {
        float newScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(newScale, transform.localScale.y, newScale);
    }
}
