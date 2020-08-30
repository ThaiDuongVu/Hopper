using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text instructionText;
    public Slider forceSlider;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public TMP_Text newHighScoreText;

    public Button adButton;

    public GameController gameController;

    private void Start()
    {
        DisplayNewHighScore(false);
    }

    private void Update()
    {
        DisplayAdButton();
    }

    // Display an instruction message
    public void DisplayInstruction(bool value, string message = "")
    {
        instructionText.text = message;
        instructionText.gameObject.SetActive(value);
    }

    // Display the player's hop force slider
    public void DisplayForceSlider(float force, float maxForce)
    {
        forceSlider.value = (force / maxForce);
    }

    // Display score text
    public void DisplayScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // Update high score text to display the latest high score
    public void UpdateHighScore()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    // Display or hide the video ad button 
    private void DisplayAdButton()
    {
        if (Ad.adReady && !gameController.adWatched)
        {
            adButton.interactable = true;
        }
        else
        {
            adButton.interactable = false;
        }
    }

    // Set whether to display the new high score text or not
    public void DisplayNewHighScore(bool value)
    {
        newHighScoreText.gameObject.SetActive(value);
    }
}
