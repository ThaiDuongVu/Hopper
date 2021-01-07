using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public int ComboMultiplier { get; set; }
    private float comboTimer;
    private const float ComboDecreaseFactor = 0.5f;
    private bool isDecreasing;

    private GameController gameController;
    public ComboText comboText;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    private void Start()
    {
        ComboMultiplier = 1;
        comboTimer = 0f;

        isDecreasing = true;
    }

    private void Update()
    {
        if (gameController.GameState == GameState.Started)
        {
            SetCombo();
        }
        else
        {
            comboTimer = 0f;
            ComboMultiplier = 1;
        }

        comboText.Scale(ComboMultiplier, comboTimer);
    }

    // Add to combo when scoring
    public void AddCombo()
    {
        ComboMultiplier += 1;
        comboTimer = 1f;

        comboText.AddCombo(ComboMultiplier);
    }

    // Decrease combo timer over time and set multiplier to 0 if timer reaches 0
    private void SetCombo()
    {
        if (isDecreasing)
            comboTimer -= ComboDecreaseFactor * Time.deltaTime;

        if (comboTimer < 0f)
        {
            comboTimer = 0f;
            ComboMultiplier = 1;
        }
    }
}
