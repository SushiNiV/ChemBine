using UnityEngine;
using UnityEngine.UI;

public class ScrollRectTop : MonoBehaviour
{
    public ScrollRect scrollRect;

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
    }
}