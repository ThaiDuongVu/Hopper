using UnityEngine;

public class GameView : MonoBehaviour
{
    // The desired aspect ratio
    private const float TargetAspect = 9f / 18f;

    // The game window's current aspect ratio
    private readonly float _windowAspect = Screen.width / (float)Screen.height;

    // current viewport width and height should be scaled by this amount
    private float _scaleHeight;
    private float _scaleWidth;

    private Camera _camera;
    private Rect _rect;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _rect = _camera.rect;
    }

    private void Start()
    {
        ForceAspectRatio();
    }

    // Force the game's aspect ratio for better control of game's view
    private void ForceAspectRatio()
    {
        _scaleHeight = _windowAspect / TargetAspect;

        // If scaled height is less than current height, add letterbox
        if (_scaleHeight < 1f)
        {
            _scaleWidth = 1f;
        }
        else // Else add pillar box
        {
            _scaleWidth = 1f / _scaleHeight;
            _scaleHeight = 1f;
        }

        _rect.width = _scaleWidth;
        _rect.height = _scaleHeight;

        _rect.x = (1f - _scaleWidth) / 2f;
        _rect.y = (1f - _scaleHeight) / 2f;

        _camera.rect = _rect;
    }
}
