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

    public AudioPlayer audioPlayer;

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

        // Play charge sound
        audioPlayer.Play("Charge");

        // Is charging
        _isCharging = true;

        // Animate the force slider
        uiController.AnimateSlider(true);

        // Set to charge animation
        _animator.SetTrigger(Charge);
        gameController.currentPlatform.GetComponent<Animator>().SetTrigger(Charge);
    }

    private void ChargeOnCanceled(InputAction.CallbackContext context)
    {
        if (!_isCharging) return;

        // Stop charge sound
        audioPlayer.Stop("Charge");
        // Play hop sound
        audioPlayer.Play("Hop");

        // No longer charging
        _isCharging = false;

        // Is not grounded anymore
        _isGrounded = false;

        // Set pad invisible
        pad.SetActive(false);

        // Add hop force at direction
        _rigidBody.AddForce(_hopForce * direction);

        // Animate the force slider
        uiController.AnimateSlider(false);

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
            if (_hopForce > MinHopForce)
            {
                _hopForce -= _forceDelta * Time.deltaTime;
            }
        }

        if (transform.position.y < -40f)
        {
            Die();
        }
    }

    // Die 🤷‍♂️
    private void Die()
    {
        // Set game over
        gameController.GameOver();

        // Shake camera
        cameraShake.Shake();

        // Set object inactive
        gameObject.SetActive(false);

        // Play die sound
        audioPlayer.Play("Die");
    }

    // Reset player to default state
    public void Reset()
    {
        Vector3 nextPlatformPosition = gameController.nextPlatform.transform.position;

        // Reset position
        transform.position = new Vector3(nextPlatformPosition.x, -10f, nextPlatformPosition.z);

        // Set active to true
        gameObject.SetActive(true);

        Celebrate();

        // Reset game state
        gameController.Reset();
    }

    // Create a fireworks particle effects
    public void Celebrate()
    {
        // Spawn a fireworks
        Vector3 position = transform.position;
        Instantiate(fireworks, new Vector3(position.x, -10f, position.z), fireworks.transform.rotation);

        // Play celebrate sound
        audioPlayer.Play("Celebrate");
    }

    // Rotate to moving direction
    public void Rotate(int movingDirection)// 0: Left, 1: Right
    {
        Transform playerTransform = transform;

        Quaternion rotation = playerTransform.rotation;
        Vector3 rotationEuler = rotation.eulerAngles;

        // Set y rotation according to moving direction
        rotationEuler.y = movingDirection == 0 ? 45f : 135f;

        rotation.eulerAngles = rotationEuler;
        playerTransform.rotation = rotation;
    }

    #region Collision Methods

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Platform")) return;

        // Play land sound
        audioPlayer.Play("Land");
        // Reset audio pitch
        audioPlayer.ResetPitch();

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

        // Decrease path count
        perfectPath.pathCount--;

        // Add score
        gameController.AddScore(1);

        // Play a sound
        audioPlayer.Play("Coin");

        // Spawn a small explosion and shake the camera
        Instantiate(smallExplosion, other.transform.position, smallExplosion.transform.rotation);
        cameraShake.ShakeLight();

        // If a perfect path then celebrate
        if (perfectPath.pathCount <= 0)
        {
            Celebrate();
        }

        // Set coin inactive
        // Not destroy the object because it will be destroyed later
        other.gameObject.SetActive(false);
    }

    #endregion

    // Set player's character model based on selection
    public void ApplyModels(int modelIndex)
    {
        // Deactivate all models
        foreach (GameObject model in models)
        {
            model.SetActive(false);
        }

        // Activate active model
        models[modelIndex].SetActive(true);
    }
}
