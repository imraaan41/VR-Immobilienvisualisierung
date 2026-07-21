using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalMouseScroll : MonoBehaviour, IScrollHandler
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.15f;

    private void Awake()
    {
        if (scrollRect == null)
        {
            scrollRect = GetComponent<ScrollRect>();
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect == null)
            return;

        float delta = eventData.scrollDelta.y * scrollSpeed;

        scrollRect.horizontalNormalizedPosition -= delta;
        scrollRect.horizontalNormalizedPosition =
            Mathf.Clamp01(scrollRect.horizontalNormalizedPosition);
    }
}