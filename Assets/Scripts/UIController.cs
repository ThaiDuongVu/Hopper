using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text instructionText;

    // Whether to display instruction or not
    public void DisplayInstruction(bool value, string message="")
    {
        instructionText.text = message;
        instructionText.gameObject.SetActive(value);
    }
}
