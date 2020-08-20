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

    private Vector3 direction;

    public GameController gameController;
    public UIController uiController;

    public MainCamera mainCamera;

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
        _isCharging = true;
    }

    private void ChargeOnCanceled(InputAction.CallbackContext context)
    {
        _isCharging = false;

        _rigidBody.AddForce(_hopForce * direction);
        _hopForce = MinHopForce;
    }

    #endregion

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        direction = new Vector3(-0.7f, 1f, 0.7f);
    }

    private void Update()
    {
        uiController.DisplayForceSlider(_hopForce, MinHopForce, MaxHopForce);

        if (_isCharging)
        {
            _hopForce += 500f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            gameController.SpawnPlatform();
            mainCamera.isFollowing = true;
        }
    }
}
