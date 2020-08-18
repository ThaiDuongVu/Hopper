using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private InputManager _inputManager;

    public GameState gameState { get; private set; } = GameState.NotStarted;

    private UIController uiController;

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
            uiController.DisplayInstruction(false);

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
        uiController = GetComponent<UIController>();
    }

    private void Start()
    {
        uiController.DisplayInstruction(true, "Tap to start delivery");
    }

    private void Update()
    {

    }
}
