using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesignMenuController : MonoBehaviour
{
    [Header("Scroll Views")]
    public GameObject wallScrollView;
    public GameObject floorScrollView;

    [Header("Tab Buttons")]
    public Button wallTabButton;
    public Button floorTabButton;

    [Header("Tab Backgrounds")]
    public Image wallTabBackground;
    public Image floorTabBackground;

    [Header("Tab Outlines")]
    public Outline wallTabOutline;
    public Outline floorTabOutline;

    [Header("Tab Labels")]
    public TMP_Text wallTabLabel;
    public TMP_Text floorTabLabel;

    [Header("Tab Icons")]
    public Transform wallIconRoot;
    public Transform floorIconRoot;

    [Header("Footer")]
    public TMP_Text selectedText;

    [Header("Tab Button Colors")]
    public Color normalColor = HexColor("4C4C4C");
    public Color highlightedColor = HexColor("40404C");
    public Color pressedColor = HexColor("4C4C4C");
    public Color selectedColor = HexColor("333333");
    public Color disabledColor = new Color(0.25f, 0.25f, 0.25f, 0.35f);

    [Header("Tab Image Colors")]
    public Color inactiveImageColor = new Color(1f, 1f, 1f, 5f / 255f);
    public Color activeImageColor = HexColor("333333");

    [Header("Tab Outline")]
    public Color outlineColor = HexColor("40404C");
    public Vector2 outlineDistance = new Vector2(1f, -1f);

    [Header("Tab Text/Icon")]
    public Color activeTextColor = Color.white;
    public Color inactiveTextColor = new Color(0.75f, 0.75f, 0.80f, 1f);

    public Color activeIconColor = Color.white;
    public Color inactiveIconColor = new Color(0.75f, 0.75f, 0.80f, 1f);

    private void Start()
    {
        SetupTabButton(wallTabButton);
        SetupTabButton(floorTabButton);

        SetupOutline(wallTabOutline);
        SetupOutline(floorTabOutline);

        ShowWalls();
        SetSelectedText("-");
    }

    public void ShowWalls()
    {
        if (wallScrollView != null)
            wallScrollView.SetActive(true);

        if (floorScrollView != null)
            floorScrollView.SetActive(false);

        ApplyTabVisuals(true);
    }

    public void ShowFloors()
    {
        if (wallScrollView != null)
            wallScrollView.SetActive(false);

        if (floorScrollView != null)
            floorScrollView.SetActive(true);

        ApplyTabVisuals(false);
    }

    public void SetSelectedText(string optionName)
    {
        if (selectedText != null)
            selectedText.text = "Ausgewählt: " + optionName;
    }

    private void SetupTabButton(Button button)
    {
        if (button == null)
            return;

        button.transition = Selectable.Transition.ColorTint;

        ColorBlock colors = button.colors;
        colors.normalColor = normalColor;
        colors.highlightedColor = highlightedColor;
        colors.pressedColor = pressedColor;
        colors.selectedColor = selectedColor;
        colors.disabledColor = disabledColor;
        colors.colorMultiplier = 1f;
        colors.fadeDuration = 0.1f;

        button.colors = colors;
    }

    private void SetupOutline(Outline outline)
    {
        if (outline == null)
            return;

        outline.effectColor = outlineColor;
        outline.effectDistance = outlineDistance;
        outline.useGraphicAlpha = true;
    }

    private void ApplyTabVisuals(bool wallActive)
    {
        SetTabVisual(
            wallTabBackground,
            wallTabLabel,
            wallIconRoot,
            wallActive
        );

        SetTabVisual(
            floorTabBackground,
            floorTabLabel,
            floorIconRoot,
            !wallActive
        );
    }

    private void SetTabVisual(Image background, TMP_Text label, Transform iconRoot, bool active)
    {
        if (background != null)
        {
            background.color = active ? activeImageColor : inactiveImageColor;
        }

        if (label != null)
        {
            label.color = active ? activeTextColor : inactiveTextColor;
            label.fontStyle = active ? FontStyles.Bold : FontStyles.Normal;
        }

        if (iconRoot != null)
        {
            Image[] icons = iconRoot.GetComponentsInChildren<Image>(true);

            foreach (Image icon in icons)
            {
                icon.color = active ? activeIconColor : inactiveIconColor;
            }
        }
    }

    private static Color HexColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
            return color;

        return Color.white;
    }
}