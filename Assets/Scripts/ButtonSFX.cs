using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour
{
    [Header("SFX Settings")]
    public AudioSource sfxSource;
    public AudioClip buttonClick;

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() =>
            {
                if (sfxSource != null && buttonClick != null)
                    sfxSource.PlayOneShot(buttonClick);
            });
        }
    }
}