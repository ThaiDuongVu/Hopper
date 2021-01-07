using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Animator cameraAnimator;
    private string sceneToLoad;

    public Canvas canvas;
    private static readonly int Outro = Animator.StringToHash("outro");

    private void Awake()
    {
        if (!(Camera.main is null)) cameraAnimator = Camera.main.transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        // If current camera animator state is "Exit" then load a scene
        if (cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }

    // Set a scene to load
    public void Load(string scene)
    {
        // Set scene to load
        sceneToLoad = scene;

        // Disable canvas while camera is animating
        canvas.gameObject.SetActive(false);

        // Camera begin animation
        cameraAnimator.SetTrigger(Outro);
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
