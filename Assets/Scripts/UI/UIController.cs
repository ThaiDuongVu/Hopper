using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text instructionText;

    public Slider forceSlider;
    private Animator sliderAnimator;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public TMP_Text newHighScoreText;

    public Button adButton;

    public GameController gameController;

    private void Awake()
    {
        sliderAnimator = forceSlider.GetComponent<Animator>();
    }

    private void Start()
    {
        DisplayNewHighScore(false);
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

    // Set whether to display the new high score text or not
    public void DisplayNewHighScore(bool value)
    {
        newHighScoreText.gameObject.SetActive(value);
    }
    
    // Animator whether slider is sliding or not
    public void AnimateSlider(bool value)
    {
        sliderAnimator.SetBool("isSliding", value);
    }
}
