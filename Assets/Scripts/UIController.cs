using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text InstructionText;

    // Whether to display instruction or not
    public void DisplayInstruction(bool value, string message="")
    {
        InstructionText.text = message;
        InstructionText.gameObject.SetActive(value);
    }
}
