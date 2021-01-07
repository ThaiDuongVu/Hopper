using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    private Animator animator;
    private TMP_Text text;
    private static readonly int Pop = Animator.StringToHash("pop");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<TMP_Text>();
    }

    // Pop up a new message on screen
    public void Init(string message)
    {
        text.text = message;
        animator.SetTrigger(Pop);
    } 
}
