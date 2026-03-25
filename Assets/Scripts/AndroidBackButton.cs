using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AndroidBackButton : MonoBehaviour
{
    private static string lastSceneName;

    void Awake()
    {
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GoBack();
        }
    }

    public void LoadNewScene(string sceneToLoad)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneToLoad);
    }

    private void GoBack()
    {
        if (!string.IsNullOrEmpty(lastSceneName))
        {
            SceneManager.LoadScene(lastSceneName);

            lastSceneName = null;
        }
        else
        {
            Debug.Log("No previous scene recorded. Quitting...");
            Application.Quit();
        }
    }
}