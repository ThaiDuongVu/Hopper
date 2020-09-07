using UnityEngine;

public class Coin : MonoBehaviour
{
    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void OnEnable()
    {
        SetColor();
    }

    // Set a random color
    private void SetColor()
    {
        byte r = (byte) Random.Range(0, 256);
        byte g = (byte) Random.Range(0, 256);
        byte b = (byte) Random.Range(0, 256);
        byte a = 255;

        material.color = new Color32(r, g, b, a);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
    }
}
