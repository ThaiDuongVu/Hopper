using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;

    private Rigidbody _rigidBody;

    private float _hopForce;
    private const float MinHopForce = 0f;
    private const float MaxHopForce = 1000f;

    private bool _isCharging;
    private bool _isGrounded;

    private Vector3 _direction;

    public GameController gameController;
    public UIController uiController;

    public MainCamera mainCamera;
    public CameraShake cameraShake;

    public ParticleSystem explosion;

    public PopUpText popUpText;
    public string[] quotes;

    private Animator _animator;

    private void OnEnable()
    {
        _inputManager = new InputManager();

        _inputManager.Player.Charge.performed += ChargeOnPerformed;
        _inputManager.Player.Charge.canceled += ChargeOnCanceled;

        _inputManager.Enable();
    }

    #region Input Methods

    private void ChargeOnPerformed(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _isCharging = true;
            _animator.SetTrigger("charge");
        }
    }

    private void ChargeOnCanceled(InputAction.CallbackContext context)
    {
        _isCharging = false;
        _isGrounded = false;

        _rigidBody.AddForce(_hopForce * _direction);
        _hopForce = MinHopForce;

        _animator.SetTrigger("hop");

        cameraShake.Shake();
    }

    #endregion

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _direction = new Vector3(-0.7f, 1f, 0.7f);
    }

    private void Update()
    {
        uiController.DisplayForceSlider(_hopForce, MaxHopForce);

        if (_isCharging)
        {
            _hopForce += 500f * Time.deltaTime;
        }

        Vector3 position = transform.position;
        if (position.y < -40f)
        {
            gameController.GameOver();
            cameraShake.Shake();

            Destroy(gameObject);
        }
    }

    #region Collision Methods

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            gameController.SpawnPlatform();
            mainCamera.isFollowing = true;

            // TODO: More comprehensive score system
            gameController.AddScore(1);
            _isGrounded = true;

            Instantiate(explosion, transform.position, explosion.transform.rotation);
            if (gameController.gameState == GameState.Started)
            {
                popUpText.Pop(quotes[Random.Range(0, quotes.Length)]);
            }

            _animator.SetTrigger("land");

            cameraShake.Shake();
        }
    }

    #endregion
}
