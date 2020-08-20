using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private InputManager _inputManager;

    public GameState gameState { get; private set; }

    public GameObject platform;
    public Platform activePlatform;

    private UIController _uiController;

    private void OnEnable()
    {
        _inputManager = new InputManager();

        _inputManager.Game.Start.performed += StartOnPerformed;

        _inputManager.Enable();
    }

    #region Input Methods

    private void StartOnPerformed(InputAction.CallbackContext context)
    {
        if (gameState == GameState.NotStarted)
        {
            _uiController.DiplayInstruction(false);

            gameState = GameState.Started;
        }
    }

    #endregion

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _uiController = GetComponent<UIController>();
    }

    private void Start()
    {
        SpawnPlatform();
    }

    private void Update()
    {

    }

    // Spawn a new platform from the current active platform
    public void SpawnPlatform()
    {
        Vector3 spawnPosition = new Vector3(activePlatform.transform.position.x - 10f, activePlatform.transform.position.y, activePlatform.transform.position.z + 10f);
        Quaternion spawnRotation = platform.transform.rotation;

        activePlatform = Instantiate(platform, spawnPosition, spawnRotation).GetComponent<Platform>();
    }
}
