using UnityEngine;

public class WiggleMovement : MonoBehaviour
{
    public float wiggleAmount = 10f;
    public float wiggleSpeed = 1f;

    public float tiltAmount = 5f;
    public float tiltSpeed = 1f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float wiggleX = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
        float wiggleY = Mathf.Cos(Time.time * wiggleSpeed) * wiggleAmount;

        transform.position = originalPosition + new Vector3(wiggleX, wiggleY, 0f);

        float tiltZ = Mathf.Sin(Time.time * tiltSpeed) * tiltAmount;

        transform.rotation = Quaternion.Euler(0f, 0f, tiltZ);
    }
}
