using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneLoader : MonoBehaviour
{
    public string sceneName;
    public Button button;

    bool isLoading;

    public void LoadScene()
    {
        if (isLoading) return;

        isLoading = true;

        if (button != null)
            button.interactable = false;

        SceneTransition.LoadSceneStatic(sceneName);
    }
}