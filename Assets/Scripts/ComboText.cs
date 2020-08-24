using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    private Animator _animator;
    public TMP_Text text;

    private static readonly int Score = Animator.StringToHash("score");

    private const float ScaleFactor = 0.25f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Add new combo
    public void AddCombo(int multiplier)
    {
        Rotate();

        _animator.SetTrigger(Score);
        text.text = "x" + multiplier.ToString();
    }

    // Scale text to combo value
    public void Scale(int multiplier, float timer)
    {
        float newScale = (text.transform.localScale.x + multiplier) * timer * ScaleFactor;
        text.transform.localScale = new Vector3(newScale, newScale, 1f);
    }

    // Set a random rotation
    private void Rotate()
    {
        text.transform.rotation = new Quaternion(text.transform.rotation.x, text.transform.rotation.y, Random.Range(-0.25f, 0.25f), text.transform.rotation.w);
    }
}
