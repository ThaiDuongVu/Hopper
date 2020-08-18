using UnityEngine;
using UnityEngine.InputSystem;

public class Letter : MonoBehaviour
{
    private InputManager _inputManager;

    public Arrow arrow;
    private Animator _animator;

    private void OnEnable()
    {
        _inputManager = new InputManager();

        _inputManager.Letter.ChangeDirection.performed += ChangeDirectionOnPerformed;
        _inputManager.Letter.ChangeDirection.canceled += ChangeDirectionOnCanceled;

        _inputManager.Enable();
    }

    #region Input Methods

    private void ChangeDirectionOnPerformed(InputAction.CallbackContext context)
    {

    }

    private void ChangeDirectionOnCanceled(InputAction.CallbackContext context)
    {

    }

    #endregion

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        arrow.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void StartChangeDirection()
    {
        arrow.gameObject.SetActive(true);
    }

    private void StopChangeDirection()
    {
        arrow.gameObject.SetActive(false);
    }
}
