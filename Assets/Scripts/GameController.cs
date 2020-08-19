using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameState gameState { get; private set; } = GameState.NotStarted;

    private UIController uiController;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
    }

    private void Start()
    {
        uiController.DisplayInstruction(true, "Tap to start delivery");
    }
}
