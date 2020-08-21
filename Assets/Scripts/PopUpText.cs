using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    private Animator _animator;
    private TMP_Text _text;
    private static readonly int Pop = Animator.StringToHash("pop");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponent<TMP_Text>();
    }

    // Pop up a new message on screen
    public void Init(string message)
    {
        _text.text = message;
        _animator.SetTrigger(Pop);
    }
}
