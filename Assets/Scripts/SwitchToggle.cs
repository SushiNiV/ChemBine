using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchToggle : MonoBehaviour
{
    public Toggle toggle;
    public RectTransform knob;
    public Vector2 offPosition;
    public Vector2 onPosition;
    public float speed = 10f;

    public TextMeshProUGUI textOn;
    public TextMeshProUGUI textOff;
    [Range(0f, 1f)] public float inactiveAlpha = 0.55f;

    public AudioSource sfxSource;
    public AudioClip toggleClip;

    private bool lastState;

    private void Start()
    {
        lastState = toggle.isOn;
    }

    private void Update()
    {
        Vector2 target = toggle.isOn ? onPosition : offPosition;
        knob.anchoredPosition = Vector2.Lerp(knob.anchoredPosition, target, Time.deltaTime * speed);

        if (textOn != null)
        {
            Color c = textOn.color;
            c.a = toggle.isOn ? 1f : inactiveAlpha;
            textOn.color = c;
        }

        if (textOff != null)
        {
            Color c = textOff.color;
            c.a = toggle.isOn ? inactiveAlpha : 1f;
            textOff.color = c;
        }

        if (toggle.isOn != lastState)
        {
            if (sfxSource != null && toggleClip != null)
                sfxSource.PlayOneShot(toggleClip);

            lastState = toggle.isOn;
        }
    }
}