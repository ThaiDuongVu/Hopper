using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text instructionText;
    public Slider forceSlider;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    // Display an instruction message
    public void DiplayInstruction(bool value, string message = "")
    {
        instructionText.text = message;
        instructionText.gameObject.SetActive(value);
    }

    // Display the player's hop force slider
    public void DisplayForceSlider(float force, float minForce, float maxForce)
    {
        forceSlider.value = (force / maxForce);
    }
}
