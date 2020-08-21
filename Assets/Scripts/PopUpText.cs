using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    private Animator _animator;
    private TMP_Text _text;
    private static readonly int SPop = Animator.StringToHash("pop");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponent<TMP_Text>();
    }

    public void Pop(string message)
    {
        _text.text = message;
        _animator.SetTrigger(SPop);
    }
}
