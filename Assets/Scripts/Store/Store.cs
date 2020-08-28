using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    public Player player;
    private Animator _playerAnimator;
    private int _playerModel;

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
        _playerModel = PlayerPrefs.GetInt("PlayerModel", 0);

        _currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        currentHighScoreText.text = "Current high score: " + _currentHighScore;

        _requiredHighScores = new int[player.models.Count];
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

        SetText();
    }

    public void TogglePlayerModel(string direction)
    {
        _playerAnimator.SetTrigger("morph");

        if (direction.Equals("left"))
        {
            if (_playerModel > 0)
            {
                _playerModel--;

                SetText();
                player.ApplyModels(_playerModel);
            }
        }
        else
        {
            if (_playerModel < player.models.Count - 1)
            {
                _playerModel++;

                SetText();
                player.ApplyModels(_playerModel);
            }
        }
    }

    public void Apply()
    {
        if (_currentHighScore >= _requiredHighScores[_playerModel])
        {
            PlayerPrefs.SetInt("PlayerModel", _playerModel);
            player.ApplyModels(PlayerPrefs.GetInt("PlayerModel", 0));
        }
    }

    private void SetText()
    {
        requiredHighScoreText.text = "Required high score: " + _requiredHighScores[_playerModel];
        text.text = player.models[_playerModel].name;
    }
}
