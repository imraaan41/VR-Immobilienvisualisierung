using UnityEngine;
using UnityEngine.UI;

public class AutoHorizontalScrollbarController : MonoBehaviour
{
    [Header("References")]
    public ScrollRect scrollRect;
    public RectTransform viewport;
    public RectTransform content;
    public Scrollbar horizontalScrollbar;

    [Header("Settings")]
    public float scrollbarSpace = 14f;
    public float widthTolerance = 2f;

    private void Reset()
    {
        scrollRect = GetComponent<ScrollRect>();

        if (scrollRect != null)
        {
            viewport = scrollRect.viewport;
            content = scrollRect.content;
            horizontalScrollbar = scrollRect.horizontalScrollbar;
        }
    }

    private void Awake()
    {
        CacheReferences();
    }

    private void OnEnable()
    {
        UpdateScrollbarVisibility();
    }

    private void LateUpdate()
    {
        UpdateScrollbarVisibility();
    }

    private void CacheReferences()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();

        if (scrollRect != null)
        {
            if (viewport == null)
                viewport = scrollRect.viewport;

            if (content == null)
                content = scrollRect.content;

            if (horizontalScrollbar == null)
                horizontalScrollbar = scrollRect.horizontalScrollbar;
        }
    }

    private void UpdateScrollbarVisibility()
    {
        if (scrollRect == null || viewport == null || content == null || horizontalScrollbar == null)
            return;

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        float contentWidth = content.rect.width;
        float viewportWidth = viewport.rect.width;

        bool needsScrollbar = contentWidth > viewportWidth + widthTolerance;

        horizontalScrollbar.gameObject.SetActive(needsScrollbar);
        scrollRect.horizontal = needsScrollbar;

        if (!needsScrollbar)
        {
            scrollRect.horizontalNormalizedPosition = 0f;
        }

        UpdateViewportSpace(needsScrollbar);
    }

    private void UpdateViewportSpace(bool scrollbarVisible)
    {
        if (viewport == null)
            return;

        Vector2 offsetMin = viewport.offsetMin;
        offsetMin.y = scrollbarVisible ? scrollbarSpace : 0f;
        viewport.offsetMin = offsetMin;
    }
}