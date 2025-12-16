using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class MixerVolume : MonoBehaviour
{
    [Header("Audio Components")]
    public AudioMixer mixer;
    public string exposedParameter = "MasterVolume";

    [Header("UI Components")]
    public Slider volumeSlider;
    public TMP_Text volumeText;
    private void Start()
    {
        volumeSlider.minValue = 0;
        volumeSlider.maxValue = 100;
        volumeSlider.wholeNumbers = true;

        UpdateVolume(volumeSlider.value);

        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float sliderValue)
    {
        float linear = Mathf.Clamp(sliderValue / 100f, 0.0001f, 1f);

        float dB = Mathf.Log10(linear) * 20f;

        mixer.SetFloat(exposedParameter, dB);

        UpdateText(sliderValue);
    }

    private void UpdateText(float sliderValue)
    {
        volumeText.text = Mathf.RoundToInt(sliderValue) + "%";
    }
}