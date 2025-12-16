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
    public string closeTriggerName = "Close";
    public float animationBuffer = 0.05f;
    public void CloseSettings()
    {
        if (settingsAnimator != null)
        {
            settingsAnimator.SetTrigger(closeTriggerName);
            StartCoroutine(WaitForCloseAnimation());
        }
        else
        {
            settingsPanel.SetActive(false);
            StartCoroutine(ResetAfterClose());
        }
    }
    private IEnumerator WaitForCloseAnimation()
    {
        if (settingsAnimator == null)
            yield break;

        AnimatorStateInfo stateInfo = settingsAnimator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length + animationBuffer;

        yield return new WaitForSeconds(animationLength);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        StartCoroutine(ResetAfterClose());
    }
    private IEnumerator ResetAfterClose()
    {
        yield return null;

        if (defaultTab != null)
            defaultTab.ActivateTab();
    }
    private void OnEnable()
    {
        if (defaultTab != null)
            defaultTab.ActivateTab();
    }
}