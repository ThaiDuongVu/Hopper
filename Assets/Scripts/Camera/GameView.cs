using UnityEngine;

public class GameView : MonoBehaviour
{
    // The desired aspect ratio
    private const float TargetAspect = 9f / 18f;

    // The game window's current aspect ratio
    private readonly float windowAspect = Screen.width / (float)Screen.height;

    // Current viewport width and height should be scaled by this amount
    private float scaleHeight;
    private float scaleWidth;

    private new Camera camera;
    private Rect rect;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        rect = camera.rect;
    }

    private void Start()
    {
        ForceAspectRatio();
    }

    // Force the game's aspect ratio for better control of game's view
    private void ForceAspectRatio()
    {
        scaleHeight = windowAspect / TargetAspect;

        // If scaled height is less than current height, add letterbox
        if (scaleHeight < 1f)
        {
            scaleWidth = 1f;
        }
        else // Else add pillar box
        {
            scaleWidth = 1f / scaleHeight;
            scaleHeight = 1f;
        }

        rect.width = scaleWidth;
        rect.height = scaleHeight;

        rect.x = (1f - scaleWidth) / 2f;
        rect.y = (1f - scaleHeight) / 2f;

        camera.rect = rect;
    }
}
