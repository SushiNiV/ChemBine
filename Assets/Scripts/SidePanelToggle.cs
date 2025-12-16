using UnityEngine;

public class SidePanelToggle : MonoBehaviour
{
    public RectTransform panel;
    public Vector2 openPos;
    public Vector2 closedPos;
    public float speed = 12f;

    public bool IsOpen { get; private set; }

    void Update()
    {
        panel.anchoredPosition = Vector2.Lerp(
            panel.anchoredPosition,
            IsOpen ? openPos : closedPos,
            Time.deltaTime * speed
        );
    }

    public void Open()
    {
        IsOpen = true;
    }

    public void Close()
    {
        IsOpen = false;
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
    }
}