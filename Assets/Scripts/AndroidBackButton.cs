using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidBackButton : MonoBehaviour
{
    public string previousSceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToPreviousScene();
        }
    }

    private void GoToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex > 0)
            {
                SceneManager.LoadScene(currentSceneIndex - 1);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}