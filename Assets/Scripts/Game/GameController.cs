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
        if (_spawnDirection == 0)
        {
            _spawnDirection = 1;
        }
        else
        {
            _spawnDirection = 0;
        }

        Vector3 platformPosition = nextPlatform.transform.position;

        Vector3 spawnPosition;
        Quaternion spawnRotation = platform.transform.rotation;

        if (_spawnDirection == 0)
        {
            spawnPosition = new Vector3(platformPosition.x - 10f, platformPosition.y, platformPosition.z + 10f);
            player.direction = player.Left;
        }
        else
        {
            spawnPosition = new Vector3(platformPosition.x + 10f, platformPosition.y, platformPosition.z + 10f);
            player.direction = player.Right;
        }

        currentPlatform = nextPlatform;
        nextPlatform = Instantiate(platform, spawnPosition, spawnRotation).GetComponent<Platform>();

        mainCamera.currentDirection = _spawnDirection;
        perfectPath.spawnDirection = this._spawnDirection;
    }

    // Add a value to score
    public void AddScore(int value)
    {
        _score += value * _comboSystem.comboMultiplier;
        _comboSystem.AddCombo();

        scoreTextAnimator.SetTrigger(Score);
    }

    // Check if current score is greater than high score, if so save the new high score
    private void CheckHighScore()
    {
        if (_score <= _highScore) return;

        _highScore = _score;
        PlayerPrefs.SetInt("HighScore", _highScore);

        _uiController.UpdateHighScore();

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
    }

    public void Reset()
    {
        gameOverMenu.SetActive(false);
        gameState = GameState.Started;
    }
}
