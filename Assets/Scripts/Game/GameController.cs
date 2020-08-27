using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private InputManager _inputManager;

    public GameState gameState { get; private set; }

    public GameObject platform;
    public Platform nextPlatform;
    public Platform currentPlatform;

    private UIController _uiController;

    private int _score;
    private int _highScore;
    private bool _newHighScore;

    private ComboSystem _comboSystem;

    public Animator scoreTextAnimator;
    private static readonly int Score = Animator.StringToHash("score");

    public GameObject gameOverMenu;

    public Player player;

    public Ad ad;
    public bool adWatched { get; set; }

    private void OnEnable()
    {
        _inputManager = new InputManager();

        _inputManager.Game.Start.performed += StartOnPerformed;
        _inputManager.Game.Test.performed += TestOnPerformed;

        _inputManager.Enable();
    }

    #region Input Methods

    private void StartOnPerformed(InputAction.CallbackContext context)
    {
        if (gameState != GameState.NotStarted) return;

        _uiController.DisplayInstruction(false);
        gameState = GameState.Started;
    }

    private void TestOnPerformed(InputAction.CallbackContext context)
    {
        player.Reset();
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
        Vector3 platformPosition = nextPlatform.transform.position;

        Vector3 spawnPosition = new Vector3(platformPosition.x - 10f, platformPosition.y, platformPosition.z + 10f);
        Quaternion spawnRotation = platform.transform.rotation;

        currentPlatform = nextPlatform;
        nextPlatform = Instantiate(platform, spawnPosition, spawnRotation).GetComponent<Platform>();
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
