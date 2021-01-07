using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    public Player player;
    private Animator playerAnimator;
    private int playerModelIndex;

    public TMP_Text text;

    private int currentHighScore;
    public TMP_Text currentHighScoreText;

    private int[] requiredHighScores;
    public TMP_Text requiredHighScoreText;

    private static readonly int Morph = Animator.StringToHash("morph");

    private void Awake()
    {
        playerAnimator = player.GetComponent<Animator>();
    }

    private void Start()
    {
        player.ApplyModels(PlayerPrefs.GetInt("PlayerModel", 0));
        playerModelIndex = PlayerPrefs.GetInt("PlayerModel", 0);

        currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        currentHighScoreText.text = "Current high score: " + currentHighScore;

        requiredHighScores = new int[player.models.Count];

        // Required high score increase by 500 with every model
        for (int i = 0; i < player.models.Count; i++)
        {
            if (i == 0)
                requiredHighScores[i] = 0;
            else
                requiredHighScores[i] = requiredHighScores[i - 1] + 500;
        }

        UpdateText();
    }

    // Toggle player model left or right based on input button
    public void TogglePlayerModel(string direction)
    {
        playerAnimator.SetTrigger(Morph);

        if (direction.Equals("left"))
        {
            if (playerModelIndex > 0)
                playerModelIndex--;
        }
        else
        {
            if (playerModelIndex < player.models.Count - 1)
                playerModelIndex++;
        }

        UpdateText();
        player.ApplyModels(playerModelIndex);
    }

    // Apply changes if high score requirements is met
    public void Apply()
    {
        if (currentHighScore < requiredHighScores[playerModelIndex]) return;

        PlayerPrefs.SetInt("PlayerModel", playerModelIndex);
        player.ApplyModels(PlayerPrefs.GetInt("PlayerModel", 0));
    }

    // Update display text based on current selected model
    private void UpdateText()
    {
        requiredHighScoreText.text = "Required high score: " + requiredHighScores[playerModelIndex];
        text.text = player.models[playerModelIndex].name;
    }
}
