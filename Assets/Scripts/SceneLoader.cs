using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Animator _cameraAnimator;
    private string _sceneToLoad;

    public Canvas canvas;

    private void Awake()
    {
        _cameraAnimator = Camera.main.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
        {
            SceneManager.LoadScene(_sceneToLoad, LoadSceneMode.Single);
        }
    }

    // Load a scene
    public void Load(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
        canvas.gameObject.SetActive(false);

        _cameraAnimator.SetTrigger("outro");
    }

    // Reload scene
    public void Restart()
    {
        Load(SceneManager.GetActiveScene().name);
    }
}
