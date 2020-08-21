using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private InputManager _inputManager;

    public GameState gameState { get; private set; }

    public GameObject platform;
    public Platform activePlatform;

    private UIController _uiController;

    private int _score;
    private int _highScore;

    public Animator scoreTextAnimator;
    private static readonly int Score = Animator.StringToHash("score");

    public GameObject gameOverMenu;

    private void OnEnable()
    {
        _inputManager = new InputManager();

        _inputManager.Game.Start.performed += StartOnPerformed;

        _inputManager.Enable();
    }

    #region Input Methods

    private void StartOnPerformed(InputAction.CallbackContext context)
    {
        if (gameState != GameState.NotStarted) return;

        _uiController.DisplayInstruction(false);
        gameState = GameState.Started;
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
        _uiController.DisplayInstruction(true, "Tap and hold to start hopping");
        gameOverMenu.SetActive(false);

        // SpawnPlatform();
    }

    private void Update()
    {
        _uiController.DisplayScore(_score);
    }

    // Spawn a new platform from the current active platform
    public void SpawnPlatform()
    {
        Vector3 platformPosition = activePlatform.transform.position;

        Vector3 spawnPosition = new Vector3(platformPosition.x - 10f, platformPosition.y, platformPosition.z + 10f);
        Quaternion spawnRotation = platform.transform.rotation;

        activePlatform = Instantiate(platform, spawnPosition, spawnRotation).GetComponent<Platform>();
    }

    // Add a value to score
    public void AddScore(int value)
    {
        _score += value;
        scoreTextAnimator.SetTrigger(Score);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameState = GameState.GameOver;
    }
}
