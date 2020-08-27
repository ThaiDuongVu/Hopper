using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;

    private Rigidbody _rigidBody;

    private float _hopForce;
    private float _forceDelta = 500f;
    private const float MinHopForce = 0f;
    private const float MaxHopForce = 1000f;

    private bool _isCharging;
    private bool _isGrounded;

    private readonly Vector3 _direction = new Vector3(-0.7f, 1f, 0.7f);
    public GameObject pad;

    public GameController gameController;
    public UIController uiController;

    public MainCamera mainCamera;
    public CameraShake cameraShake;

    public ParticleSystem explosion;

    public PopUpText popUpText;
    public string[] quotes;

    private Animator _animator;
    private static readonly int Charge = Animator.StringToHash("charge");
    private static readonly int Hop = Animator.StringToHash("hop");
    private static readonly int Land = Animator.StringToHash("land");

    public PerfectPath perfectPath;
    public ParticleSystem smallExplosion;

    public ParticleSystem fireworks;

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
        if (!_isGrounded) return;

        _isCharging = true;

        _animator.SetTrigger(Charge);
        gameController.currentPlatform.GetComponent<Animator>().SetTrigger(Charge);
    }

    private void ChargeOnCanceled(InputAction.CallbackContext context)
    {
        if (!_isCharging) return;

        _isCharging = false;
        _isGrounded = false;

        pad.SetActive(false);

        _rigidBody.AddForce(_hopForce * _direction);

        _animator.ResetTrigger(Charge);
        _animator.SetTrigger(Hop);
        gameController.currentPlatform.GetComponent<Animator>().SetTrigger(Hop);

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
        pad.SetActive(false);
    }

    private void Update()
    {
        uiController.DisplayForceSlider(_hopForce, MaxHopForce);

        if (_isCharging)
        {
            _hopForce += _forceDelta * Time.deltaTime;

            // If the current force is greater than max value
            // or less than min value
            // then change force delta direction
            if ((_hopForce >= MaxHopForce && _forceDelta > 0f) || (_hopForce <= MinHopForce && _forceDelta < 0f))
            {
                _forceDelta = -_forceDelta;
            }
        }
        else
        {
            if (_hopForce > MinHopForce) _hopForce -= _forceDelta * Time.deltaTime;
        }

        Vector3 position = transform.position;
        if (position.y < -40f) Die();
    }

    // Die 🤷‍♂️
    private void Die()
    {
        gameController.GameOver();
        cameraShake.Shake();

        gameObject.SetActive(false);
    }

    // Reset player to default state
    public void Reset()
    {
        transform.position = new Vector3(gameController.nextPlatform.transform.position.x, -10f, gameController.nextPlatform.transform.position.z);
        gameObject.SetActive(true);

        gameController.Reset();
    }

    public void Celebrate()
    {
        Instantiate(fireworks, new Vector3(transform.position.x, -10f, transform.position.z), fireworks.transform.rotation);
    }

    #region Collision Methods

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Platform")) return;

        // Spawn new platform
        gameController.SpawnPlatform();

        // Trigger landing animations
        _animator.SetTrigger(Land);
        gameController.currentPlatform.GetComponent<Animator>().SetTrigger(Land);

        // Camera start following player
        mainCamera.isFollowing = true;
        _isGrounded = true;

        // Pad active
        pad.SetActive(true);

        // Calculate next perfect path
        perfectPath.CalculateNextPath(gameController.nextPlatform, this);

        // Add an explosion
        Instantiate(explosion, transform.position, explosion.transform.rotation);

        if (gameController.gameState == GameState.Started)
        {
            // Init pop up text
            popUpText.Init(quotes[Random.Range(0, quotes.Length)]);

            // Add score
            gameController.AddScore(1);
        }

        // Shake camera
        cameraShake.Shake();
    }

    #endregion

    #region Trigger Methods

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Coin")) return;

        perfectPath.pathCount--;
        gameController.AddScore(1);

        Instantiate(smallExplosion, other.transform.position, smallExplosion.transform.rotation);
        cameraShake.ShakeLight();

        if (perfectPath.pathCount <= 0) Celebrate();

        other.gameObject.SetActive(false);
    }

    #endregion
}
