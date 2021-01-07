using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private InputManager inputManager;

    public GameState GameState { get; private set; }

    public GameObject[] platformPrefabs;
    public Platform nextPlatform;
    public Platform currentPlatform;
    private int spawnDirection = 1; // 0: Left; 1: Right

    public MainCamera mainCamera;
    public PerfectPath perfectPath;

    private UIController uiController;

    private int score;
    private int highScore;
    private bool newHighScore;

    private ComboSystem comboSystem;

    public Animator scoreTextAnimator;
    private static readonly int Score = Animator.StringToHash("score");

    public GameObject gameOverMenu;

    public Player player;

    public bool AdWatched { get; set; }

    public AudioPlayer audioPlayer;

    private void OnEnable()
    {
        inputManager = new InputManager();

        inputManager.Game.Start.performed += StartOnPerformed;

        inputManager.Enable();
    }

    #region Input Methods

    private void StartOnPerformed(InputAction.CallbackContext context)
    {
        if (GameState != GameState.NotStarted) return;

        uiController.DisplayInstruction(false);
        GameState = GameState.Started;
    }

    #endregion

    private void OnDisable()
    {
        inputManager.Disable();
    }

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        comboSystem = GetComponent<ComboSystem>();
    }

    private void Start()
    {
        // Display the instruction on startup
        uiController.DisplayInstruction(true, "Tap and hold to start hopping");

        uiController.UpdateHighScore();
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        uiController.DisplayScore(score);
        CheckHighScore();
    }

    // Spawn a new platform from the current active platform
    public void SpawnPlatform()
    {
        // Decide whether to spawn left or right
        spawnDirection = spawnDirection == 0 ? 1 : 0;

        // Which platform prefab to spawn a new one
        GameObject spawnPlatform = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        Vector3 platformPosition = nextPlatform.transform.position;
        Quaternion spawnRotation = spawnPlatform.transform.rotation;

        // Position to spawn new platform
        Vector3 spawnPosition = spawnDirection == 0
            ? new Vector3(platformPosition.x - 10f, platformPosition.y, platformPosition.z + 10f)
            : new Vector3(platformPosition.x + 10f, platformPosition.y, platformPosition.z + 10f);

        // Set player flying direction
        player.direction = (spawnPosition - player.transform.position).normalized;
        player.direction.y = 1f;

        // Spawn platform
        currentPlatform = nextPlatform;
        nextPlatform = Instantiate(spawnPlatform, spawnPosition, spawnRotation).GetComponent<Platform>();

        // Set camera direction
        mainCamera.CurrentDirection = spawnDirection;

        // Set perfect path direction
        perfectPath.SpawnDirection = spawnDirection;

        // Rotate player to moving direction
        player.Rotate(spawnDirection);
    }

    // Add a value to score
    public void AddScore(int value)
    {
        // Add score with combo
        score += value * comboSystem.ComboMultiplier;
        // Add combo
        comboSystem.AddCombo();

        // Pop the score text
        scoreTextAnimator.SetTrigger(Score);
    }

    // Check if current score is greater than high score, if so save the new high score
    private void CheckHighScore()
    {
        if (score <= highScore) return;

        // Set new high score
        highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);

        // Update high score display
        uiController.UpdateHighScore();

        // Celebrate new high score
        if (!newHighScore)
        {
            newHighScore = true;
            player.Celebrate();

            uiController.DisplayNewHighScore(true);
        }
    }

    // When game over
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        GameState = GameState.GameOver;

        audioPlayer.Play("GameOver");
    }

    // If ad watched, then reset game
    public void Reset()
    {
        gameOverMenu.SetActive(false);
        GameState = GameState.Started;
    }
}
