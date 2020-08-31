using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Animator _cameraAnimator;
    private string _sceneToLoad;

    public Canvas canvas;
    private static readonly int Outro = Animator.StringToHash("outro");

    private void Awake()
    {
        if (!(Camera.main is null)) _cameraAnimator = Camera.main.transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        // If current camera animator state is "Exit" then load a scene
        if (_cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
        {
            SceneManager.LoadScene(_sceneToLoad, LoadSceneMode.Single);
        }
    }

    // Set a scene to load
    public void Load(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;

        // Disable canvas while camera is animating
        canvas.gameObject.SetActive(false);

        // Camera begin animation
        _cameraAnimator.SetTrigger(Outro);
    }

    // Restart scene
    public void Restart()
    {
        Load(SceneManager.GetActiveScene().name);
    }

    // Exit game
    public void Exit()
    {
        Application.Quit();
    }
}
