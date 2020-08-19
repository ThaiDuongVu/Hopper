using UnityEngine;

public class Letter : MonoBehaviour
{
    public Arrow arrow;

    private float _speed = 20f;
    private Vector2 _movement = Vector2.up;

    private bool _isChangingDirection = false;

    private Animator _animator;

    public MainCamera mainCamera;
    public CameraShake cameraShake;

    public GameController gameController;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        arrow.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameController.gameState == GameState.Started)
        {
            if (!_isChangingDirection)
            {
                Fly(_movement);
            }
        }
    }

    // Start & stop changing letter direction
    private void StartChangeDirection()
    {
        arrow.gameObject.SetActive(true);

        mainCamera.SetFocus(true);
        _animator.SetTrigger("prepareFly");

        _movement = Vector2.zero;

        _isChangingDirection = true;
    }

    private void StopChangeDirection()
    {
        arrow.gameObject.SetActive(false);

        mainCamera.SetFocus(false);
        _animator.SetTrigger("fly");

        _movement = arrow.transform.up;
        cameraShake.ShakeLight();

        arrow.Reset();

        _isChangingDirection = false;
    }

    // Fly towards a movement Vector
    private void Fly(Vector2 movement)
    {
        transform.Translate(movement * _speed * Time.deltaTime, Space.World);
    }
}
