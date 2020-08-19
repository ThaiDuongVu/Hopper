using UnityEngine;

public class Letter : MonoBehaviour
{
    private Vector2 _initTouchPosition;
    private Vector2 _currentTouchPosition;

    private Vector2 _initMousePosition;
    private Vector2 _currentMousePosition;

    public Arrow arrow;

    private float speed = 20f;
    private Vector2 _movement = Vector2.up;

    private bool _isChangingDirection;

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
            else
            {
                transform.rotation = arrow.transform.rotation;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartChangeDirection();
                _initTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Change arrow direction
                _currentTouchPosition = touch.position;
                arrow.Redirect(-(_currentTouchPosition - _initTouchPosition).normalized);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopChangeDirection();
            }
        }

        // Mouse input for debug purposes
        // Will remove in production
        if (Input.GetMouseButtonDown(0))
        {
            StartChangeDirection();

            _initMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _currentMousePosition = Input.mousePosition;
            arrow.Redirect(-(_currentMousePosition - _initMousePosition).normalized);
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopChangeDirection();
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

        _movement = arrow.direction;
        cameraShake.ShakeLight();

        _isChangingDirection = false;
    }

    // Fly towards a movement Vector
    private void Fly(Vector2 movement)
    {
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void Shake()
    {

    }
}
