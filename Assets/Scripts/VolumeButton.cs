using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Slider volumeSlider;
    public float step = 1f;

    public void IncreaseVolume()
    {
        if (volumeSlider != null)
            volumeSlider.value = Mathf.Clamp(volumeSlider.value + step, volumeSlider.minValue, volumeSlider.maxValue);
    }

    public void DecreaseVolume()
    {
        if (volumeSlider != null)
            volumeSlider.value = Mathf.Clamp(volumeSlider.value - step, volumeSlider.minValue, volumeSlider.maxValue);
    }
}