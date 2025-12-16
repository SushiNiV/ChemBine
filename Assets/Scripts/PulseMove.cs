using UnityEngine;
public class PulsingMovement : MonoBehaviour
{
    public float pulseAmount = 1.1f;
    public float pulseSpeed = 1f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scaleFactor = Mathf.Sin(Time.time * pulseSpeed) * (pulseAmount - 1f) + 1f;

        transform.localScale = originalScale * scaleFactor;
    }
}
