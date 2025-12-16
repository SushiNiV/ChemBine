using UnityEngine;
using UnityEngine.UI;

public class HighContrastToggle : MonoBehaviour
{
    public Toggle highContrastToggle;
    public Image highContrastOverlay;
    public float fadeSpeed = 5f;
    [Range(0f, 1f)] public float targetAlpha = 0.5f;

    private float currentAlpha = 0f;

    private void Start()
    {
        if (highContrastToggle != null)
            highContrastToggle.onValueChanged.AddListener(OnToggleChanged);

        currentAlpha = highContrastToggle.isOn ? targetAlpha : 0f;
        UpdateOverlayAlpha(currentAlpha);
    }

    private void Update()
    {
        float desiredAlpha = highContrastToggle.isOn ? targetAlpha : 0f;
        if (Mathf.Abs(currentAlpha - desiredAlpha) > 0.01f)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, desiredAlpha, Time.deltaTime * fadeSpeed);
            UpdateOverlayAlpha(currentAlpha);
        }
    }

    private void OnToggleChanged(bool isOn)
    {
    }

    private void UpdateOverlayAlpha(float alpha)
    {
        if (highContrastOverlay != null)
        {
            Color c = highContrastOverlay.color;
            c.a = alpha;
            highContrastOverlay.color = c;
            highContrastOverlay.enabled = alpha > 0f;
        }
    }

    private void OnDestroy()
    {
        if (highContrastToggle != null)
            highContrastToggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}