using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public int ComboMultiplier { get; set; }
    private float _comboTimer;
    private const float ComboDecreaseFactor = 0.5f;
    private bool _isDecreasing;

    private GameController _gameController;
    public ComboText comboText;

    private void Awake()
    {
        _gameController = GetComponent<GameController>();
    }

    private void Start()
    {
        ComboMultiplier = 1;
        _comboTimer = 0f;

        _isDecreasing = true;
    }

    private void Update()
    {
        if (_gameController.GameState == GameState.Started)
        {
            SetCombo();
        }
        else
        {
            _comboTimer = 0f;
            ComboMultiplier = 1;
        }

        comboText.Scale(ComboMultiplier, _comboTimer);
    }

    // Add to combo when scoring
    public void AddCombo()
    {
        ComboMultiplier += 1;
        _comboTimer = 1f;

        comboText.AddCombo(ComboMultiplier);
    }

    // Decrease combo timer over time and set multiplier to 0 if timer reaches 0
    private void SetCombo()
    {
        if (_isDecreasing)
            _comboTimer -= ComboDecreaseFactor * Time.deltaTime;

        if (_comboTimer < 0f)
        {
            _comboTimer = 0f;
            ComboMultiplier = 1;
        }
    }
}
