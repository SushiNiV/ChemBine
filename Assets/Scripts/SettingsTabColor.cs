using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsTabColor : MonoBehaviour
{
    [Header("This Tab UI")]
    public Image tabBackground;
    public TMP_Text tabText;

    [Header("Other Tabs")]
    public Image[] otherTabBackgrounds;
    public TMP_Text[] otherTabTexts;

    [Header("Panels")]
    public GameObject panelToShow;
    public GameObject[] otherPanelsToHide;

    [Header("Colors")]
    public Color activeBg = new Color32(246, 213, 92, 255);
    public Color activeText = new Color32(32, 99, 155, 255);
    public Color inactiveBg = new Color32(32, 99, 155, 255);
    public Color inactiveText = Color.white;

    public void ActivateTab()
    {
        if (tabBackground != null) tabBackground.color = activeBg;
        if (tabText != null) { tabText.color = activeText; tabText.ForceMeshUpdate(); }

        for (int i = 0; i < otherTabBackgrounds.Length; i++)
        {
            if (otherTabBackgrounds[i] != null) otherTabBackgrounds[i].color = inactiveBg;
            if (otherTabTexts[i] != null) { otherTabTexts[i].color = inactiveText; otherTabTexts[i].ForceMeshUpdate(); }
        }

        if (panelToShow != null) panelToShow.SetActive(true);
        if (otherPanelsToHide != null)
        {
            for (int i = 0; i < otherPanelsToHide.Length; i++)
            {
                if (otherPanelsToHide[i] != null)
                    otherPanelsToHide[i].SetActive(false);
            }
        }
    }
}