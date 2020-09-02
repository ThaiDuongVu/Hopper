using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private InputManager _inputManager;

    public GameState gameState { get; private set; }

    public GameObject platform;
    public Platform nextPlatform;
    public Platform currentPlatform;
    private int _spawnDirection = 1; // 0: Left; 1: Right

    public MainCamera mainCamera;
    public PerfectPath perfectPath;

    private UIController _uiController;

    private int _score;
    private int _highScore;
    private bool _newHighScore;

    private ComboSystem _comboSystem;

    public Animator scoreTextAnimator;
    private static readonly int Score = Animator.StringToHash("score");

    public GameObject gameOverMenu;

    public Player player;

    public bool adWatched { get; set; }

    public AudioPlayer audioPlayer;

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
        _comboSystem = GetComponent<ComboSystem>();
    }

    private void Start()
    {
        // Display the instruction on startup
        _uiController.DisplayInstruction(true, "Tap and hold to start hopping");

        _uiController.UpdateHighScore();
        _highScore = PlayerPrefs.GetInt("HighScore", 0);

        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        _uiController.DisplayScore(_score);
        CheckHighScore();
    }

    // Spawn a new platform from the current active platform
    public void SpawnPlatform()
    {
        // Decide whether to spawn left or right
        _spawnDirection = _spawnDirection == 0 ? 1 : 0;

        Vector3 platformPosition = nextPlatform.transform.position;
        Quaternion spawnRotation = platform.transform.rotation;

        // Position to spawn new platform
        Vector3 spawnPosition = _spawnDirection == 0
            ? new Vector3(platformPosition.x - 10f, platformPosition.y, platformPosition.z + 10f)
            : new Vector3(platformPosition.x + 10f, platformPosition.y, platformPosition.z + 10f);

        // Set player flying direction
        player.direction = (spawnPosition - player.transform.position).normalized;
        player.direction.y = 1f;

        // Spawn platform
        currentPlatform = nextPlatform;
        nextPlatform = Instantiate(platform, spawnPosition, spawnRotation).GetComponent<Platform>();

        // Set camera direction
        mainCamera.currentDirection = _spawnDirection;

        // Set perfect path direction
        perfectPath.spawnDirection = _spawnDirection;

        // Rotate player to moving direction
        player.Rotate(_spawnDirection);
    }

    // Add a value to score
    public void AddScore(int value)
    {
        // Add score with combo
        _score += value * _comboSystem.comboMultiplier;
        // Add combo
        _comboSystem.AddCombo();

        // Pop the score text
        scoreTextAnimator.SetTrigger(Score);
    }

    // Check if current score is greater than high score, if so save the new high score
    private void CheckHighScore()
    {
        if (_score <= _highScore) return;

        // Set new high score
        _highScore = _score;
        PlayerPrefs.SetInt("HighScore", _highScore);

        // Update high score display
        _uiController.UpdateHighScore();

        // Celebrate new high score
        if (!_newHighScore)
        {
            _newHighScore = true;
            player.Celebrate();

            _uiController.DisplayNewHighScore(true);
        }
    }

    // When game over
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameState = GameState.GameOver;

        audioPlayer.Play("GameOver");
    }

    // If ad watched, then reset game
    public void Reset()
    {
        gameOverMenu.SetActive(false);
        gameState = GameState.Started;
    }
}
