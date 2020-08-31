using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    public Vector3 direction;

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

    public List<GameObject> models;

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

        // Is charging
        _isCharging = true;

        // Set to charge animation
        _animator.SetTrigger(Charge);
        gameController.currentPlatform.GetComponent<Animator>().SetTrigger(Charge);
    }

    private void ChargeOnCanceled(InputAction.CallbackContext context)
    {
        if (!_isCharging) return;

        // No longer charging
        _isCharging = false;

        // Is not grounded anymore
        _isGrounded = false;

        // Set pad invisible
        pad.SetActive(false);

        // Add hop force at direction
        _rigidBody.AddForce(_hopForce * direction);

        //Set to hop animation
        _animator.ResetTrigger(Charge);
        _animator.SetTrigger(Hop);
        gameController.currentPlatform.GetComponent<Animator>().SetTrigger(Hop);

        // Shake camera
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
        ApplyModels(PlayerPrefs.GetInt("PlayerModel", 0));
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
        Vector3 nextPlatformPosition = gameController.nextPlatform.transform.position;

        transform.position = new Vector3(nextPlatformPosition.x, -10f, nextPlatformPosition.z);
        gameObject.SetActive(true);

        gameController.Reset();
    }

    // Create a fireworks particle effects
    public void Celebrate()
    {
        Vector3 position = transform.position;
        Instantiate(fireworks, new Vector3(position.x, -10f, position.z), fireworks.transform.rotation);
    }

    // Rotate to moving direction
    private void Rotate()
    {
        transform.right = new Vector3(-direction.x, 0f, -direction.z);
    }

    #region Collision Methods

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Platform")) return;

        // Spawn new platform
        gameController.SpawnPlatform();

        // Rotate player to moving direction
        Rotate();

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

    // Set player's character model based on selection
    public void ApplyModels(int modelIndex)
    {
        foreach (GameObject model in models)
        {
            model.SetActive(false);
        }

        models[modelIndex].SetActive(true);
    }
}
