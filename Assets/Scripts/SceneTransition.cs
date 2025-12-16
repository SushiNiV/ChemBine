using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [Header("Animator Settings")]
    public Animator transitionAnimator;
    public float slideTime = 0.8f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            canvasGroup = transitionAnimator.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = transitionAnimator.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionCoroutine(sceneName));
    }

    private IEnumerator TransitionCoroutine(string sceneName)
    {
        transitionAnimator.transform.SetAsLastSibling();


        canvasGroup.blocksRaycasts = true;

        transitionAnimator.SetTrigger("SlideDown");
        yield return new WaitForSeconds(slideTime);

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return null;

        transitionAnimator.transform.SetAsLastSibling();

        transitionAnimator.SetTrigger("SlideUp");
        yield return new WaitForSeconds(slideTime);

        canvasGroup.blocksRaycasts = false;
    }
    public static void LoadSceneStatic(string sceneName)
    {
        if (Instance != null)
        {
            Instance.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("SceneTransition instance not found in scene!");
        }
    }
}