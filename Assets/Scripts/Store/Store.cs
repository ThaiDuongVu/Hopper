using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    public Player player;
    private Animator _playerAnimator;
    private int _playerModelIndex;

    public TMP_Text text;

    private int _currentHighScore;
    public TMP_Text currentHighScoreText;

    private int[] _requiredHighScores;
    public TMP_Text requiredHighScoreText;

    private void Awake()
    {
        _playerAnimator = player.GetComponent<Animator>();
    }

    private void Start()
    {
        player.ApplyModels(PlayerPrefs.GetInt("PlayerModel", 0));
        _playerModelIndex = PlayerPrefs.GetInt("PlayerModel", 0);

        _currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        currentHighScoreText.text = "Current high score: " + _currentHighScore;

        _requiredHighScores = new int[player.models.Count];
        // Required high score increase by 500 with every model
        for (int i = 0; i < player.models.Count; i++)
        {
            if (i == 0)
            {
                _requiredHighScores[0] = 0;
            }
            else
            {
                _requiredHighScores[i] = _requiredHighScores[i - 1] + 500;
            }
        }

        UpdateText();
    }

    // Toggle player model left or right based on input button
    public void TogglePlayerModel(string direction)
    {
        _playerAnimator.SetTrigger("morph");

        if (direction.Equals("left"))
        {
            if (_playerModelIndex > 0)
            {
                _playerModelIndex--;

                UpdateText();
                player.ApplyModels(_playerModelIndex);
            }
        }
        else
        {
            if (_playerModelIndex < player.models.Count - 1)
            {
                _playerModelIndex++;

                UpdateText();
                player.ApplyModels(_playerModelIndex);
            }
        }
    }

    // Apply changes if high score requirements is met
    public void Apply()
    {
        if (_currentHighScore >= _requiredHighScores[_playerModelIndex])
        {
            PlayerPrefs.SetInt("PlayerModel", _playerModelIndex);
            player.ApplyModels(PlayerPrefs.GetInt("PlayerModel", 0));
        }
    }

    // Update display text based on current selected model
    private void UpdateText()
    {
        requiredHighScoreText.text = "Required high score: " + _requiredHighScores[_playerModelIndex];
        text.text = player.models[_playerModelIndex].name;
    }
}
