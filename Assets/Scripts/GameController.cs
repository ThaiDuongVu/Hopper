using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameState gameState { get; private set; } = GameState.NotStarted;

    private UIController _uiController;

    private void Awake()
    {
        _uiController = GetComponent<UIController>();
    }

    private void Start()
    {
        _uiController.DisplayInstruction(true, "Hold and drag to start delivery");
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (gameState == GameState.NotStarted)
            {
                _uiController.DisplayInstruction(false);

                gameState = GameState.Started;
            }
        }

        // Mouse input for debug purposes
        // Will remove in production
        if (Input.GetMouseButtonDown(0))
        {
            if (gameState == GameState.NotStarted)
            {
                _uiController.DisplayInstruction(false);

                gameState = GameState.Started;
            }
        }
    }
}
