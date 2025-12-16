using UnityEngine;

public class ButtonSceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        SceneTransition.LoadSceneStatic(sceneName);
    }
}