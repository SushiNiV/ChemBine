using UnityEngine;
using System.Collections;

public class SettingsDefault : MonoBehaviour
{
    [Header("Default Tab")]
    public SettingsTabColor defaultTab;

    [Header("Settings Panel")]
    public GameObject settingsPanel;

    [Header("Optional Animator for Close Animation")]
    public Animator settingsAnimator;

    [Header("Animation Settings")]
    public string closeAnimationName = "PanelPopOut";
    public float animationDuration = 0.15f;

    public void CloseSettings()
    {
        if (settingsAnimator != null)
        {
            settingsAnimator.Play(closeAnimationName);
            StartCoroutine(WaitForCloseAnimation());
        }
        else
        {
            settingsPanel.SetActive(false);
        }
    }

    private IEnumerator WaitForCloseAnimation()
    {
        yield return new WaitForSeconds(animationDuration);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ResetToDefaultTab());
    }

    private IEnumerator ResetToDefaultTab()
    {
        yield return new WaitForEndOfFrame();
        yield return null;

        if (defaultTab != null)
            defaultTab.ActivateTab();
    }
}