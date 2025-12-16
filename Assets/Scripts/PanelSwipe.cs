using UnityEngine;

public class PanelSwipe: MonoBehaviour
{
    public RectTransform panel;
    public Vector2 openPos;
    public Vector2 closedPos;
    public float speed = 12f;

    private Vector2 touchStart;
    private bool isOpen = false;

    void Update()
    {
        panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, isOpen ? openPos : closedPos, Time.deltaTime * speed);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 delta = touch.position - touchStart;
                    if (Mathf.Abs(delta.x) > 50)
                    {
                        if (delta.x > 0)
                            Open();
                        else
                            Close();
                    }
                    break;
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) touchStart = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - touchStart;
            if (Mathf.Abs(delta.x) > 50)
            {
                if (delta.x > 0) Open();
                else Close();
            }
        }
#endif
    }

    public void Open() => isOpen = true;
    public void Close() => isOpen = false;
    public void Toggle() => isOpen = !isOpen;
}