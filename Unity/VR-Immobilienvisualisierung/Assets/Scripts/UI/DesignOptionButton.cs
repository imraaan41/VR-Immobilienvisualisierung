using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class DesignOptionButton : MonoBehaviour
{
    [Header("References")]
    public MaterialApplyManager materialApplyManager;
    public DesignMenuController menuController;

    [Header("Design Option")]
    public DesignOption designOption;

    [Header("UI")]
    public Image swatchImage;
    public TMP_Text label;
    public Image selectionBorderImage;
    public GameObject checkmarkRoot;

    [Header("Style")]
    public int swatchSpriteWidth = 128;
    public int swatchSpriteHeight = 128;
    public int swatchCornerRadius = 18;

    public Color selectionBlue = new Color(0.25f, 0.48f, 1f, 1f);
    public Color checkmarkColor = new Color(0.25f, 0.48f, 1f, 1f);

    private Button button;
    private Image buttonImage;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        button.transition = Selectable.Transition.None;
        button.onClick.AddListener(OnButtonClicked);

        EnsureCheckmark();
        ApplyVisuals();
        SetSelected(false);
    }

    private void Start()
    {
        ApplyVisuals();
        SetSelected(false);
    }

    private void ApplyVisuals()
    {
        if (buttonImage != null)
        {
            buttonImage.color = new Color(0f, 0f, 0f, 0f);
        }

        if (designOption == null)
        {
            Debug.LogWarning("Keine DesignOption gesetzt bei: " + gameObject.name);
            return;
        }

        if (label != null)
        {
            label.text = designOption.optionName;
            label.color = Color.white;
            label.fontSize = 9;
            label.enableWordWrapping = true;
            label.overflowMode = TextOverflowModes.Ellipsis;
            label.alignment = TextAlignmentOptions.Center;
        }

        if (swatchImage != null)
        {
            Texture previewTexture = GetPreviewTexture();

            if (previewTexture != null)
            {
                swatchImage.sprite = CreateRoundedSpriteFromTexture(
                    previewTexture,
                    swatchSpriteWidth,
                    swatchSpriteHeight,
                    swatchCornerRadius
                );

                swatchImage.color = Color.white;
                swatchImage.type = Image.Type.Simple;
                swatchImage.preserveAspect = false;
            }
            else
            {
                swatchImage.sprite = CreateRoundedColorSprite(
                    designOption.previewColor,
                    swatchSpriteWidth,
                    swatchSpriteHeight,
                    swatchCornerRadius
                );

                swatchImage.color = Color.white;
            }
        }

        if (selectionBorderImage != null)
        {
            selectionBorderImage.sprite = CreateRoundedColorSprite(
                selectionBlue,
                swatchSpriteWidth,
                swatchSpriteHeight,
                swatchCornerRadius + 4
            );

            selectionBorderImage.color = Color.white;
            selectionBorderImage.type = Image.Type.Simple;
            selectionBorderImage.preserveAspect = false;
        }
    }

    private Texture GetPreviewTexture()
    {
        if (designOption == null)
            return null;

        if (designOption.previewTexture != null)
            return designOption.previewTexture;

        if (designOption.material == null)
            return null;

        Material mat = designOption.material;

        if (mat.HasProperty("_BaseMap"))
        {
            Texture tex = mat.GetTexture("_BaseMap");
            if (tex != null)
                return tex;
        }

        if (mat.HasProperty("_MainTex"))
        {
            Texture tex = mat.GetTexture("_MainTex");
            if (tex != null)
                return tex;
        }

        if (mat.mainTexture != null)
            return mat.mainTexture;

        return null;
    }

    private void OnButtonClicked()
    {
        if (materialApplyManager == null || designOption == null)
            return;

        materialApplyManager.ApplyDesignOption(designOption);

        if (menuController != null)
            menuController.SetSelectedText(designOption.optionName);

        DesignOptionButton[] allButtons =
            FindObjectsByType<DesignOptionButton>(FindObjectsSortMode.None);

        foreach (DesignOptionButton optionButton in allButtons)
        {
            optionButton.SetSelected(false);
        }

        SetSelected(true);
    }

    public void SetSelected(bool selected)
    {
        if (selectionBorderImage != null)
            selectionBorderImage.gameObject.SetActive(selected);

        if (checkmarkRoot != null)
            checkmarkRoot.SetActive(selected);
    }

    private void EnsureCheckmark()
    {
        if (checkmarkRoot != null)
            return;

        GameObject root = new GameObject("Checkmark", typeof(RectTransform));
        root.transform.SetParent(transform, false);

        RectTransform rootRect = root.GetComponent<RectTransform>();
        rootRect.anchorMin = new Vector2(1f, 1f);
        rootRect.anchorMax = new Vector2(1f, 1f);
        rootRect.pivot = new Vector2(0.5f, 0.5f);
        rootRect.anchoredPosition = new Vector2(-8f, -8f);
        rootRect.sizeDelta = new Vector2(22f, 22f);

        GameObject circle = new GameObject("Circle", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        circle.transform.SetParent(root.transform, false);

        RectTransform circleRect = circle.GetComponent<RectTransform>();
        circleRect.anchorMin = new Vector2(0.5f, 0.5f);
        circleRect.anchorMax = new Vector2(0.5f, 0.5f);
        circleRect.pivot = new Vector2(0.5f, 0.5f);
        circleRect.anchoredPosition = Vector2.zero;
        circleRect.sizeDelta = new Vector2(22f, 22f);

        Image circleImage = circle.GetComponent<Image>();
        circleImage.sprite = CreateCircleSprite(checkmarkColor, 64);
        circleImage.color = Color.white;

        CreateCheckLine(root.transform, "CheckLineShort", new Vector2(-3f, -1f), new Vector2(7f, 3f), -35f);
        CreateCheckLine(root.transform, "CheckLineLong", new Vector2(3f, -1f), new Vector2(12f, 3f), 45f);

        checkmarkRoot = root;
    }

    private void CreateCheckLine(Transform parent, string name, Vector2 position, Vector2 size, float rotationZ)
    {
        GameObject line = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        line.transform.SetParent(parent, false);

        RectTransform rect = line.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;
        rect.localRotation = Quaternion.Euler(0f, 0f, rotationZ);

        Image image = line.GetComponent<Image>();
        image.sprite = CreateSolidSprite(Color.white, 8, 8);
        image.color = Color.white;
    }

    private Sprite CreateRoundedSpriteFromTexture(Texture source, int width, int height, int radius)
    {
        Texture2D readable = MakeReadableCopy(source, width, height);
        Texture2D rounded = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color color = readable.GetPixel(x, y);

                if (!IsInsideRoundedRect(x, y, width, height, radius))
                    color.a = 0f;

                rounded.SetPixel(x, y, color);
            }
        }

        rounded.Apply();

        return Sprite.Create(
            rounded,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }

    private Sprite CreateRoundedColorSprite(Color color, int width, int height, int radius)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = color;

                if (!IsInsideRoundedRect(x, y, width, height, radius))
                    pixel.a = 0f;

                texture.SetPixel(x, y, pixel);
            }
        }

        texture.Apply();

        return Sprite.Create(
            texture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }

    private Sprite CreateCircleSprite(Color color, int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Vector2 center = new Vector2(size / 2f, size / 2f);
        float radius = size / 2f;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                Color pixel = distance <= radius ? color : new Color(0, 0, 0, 0);
                texture.SetPixel(x, y, pixel);
            }
        }

        texture.Apply();

        return Sprite.Create(
            texture,
            new Rect(0, 0, size, size),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }

    private Sprite CreateSolidSprite(Color color, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        return Sprite.Create(
            texture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }

    private bool IsInsideRoundedRect(int x, int y, int width, int height, int radius)
    {
        int left = radius;
        int right = width - radius - 1;
        int bottom = radius;
        int top = height - radius - 1;

        if (x >= left && x <= right)
            return true;

        if (y >= bottom && y <= top)
            return true;

        Vector2 bottomLeft = new Vector2(left, bottom);
        Vector2 bottomRight = new Vector2(right, bottom);
        Vector2 topLeft = new Vector2(left, top);
        Vector2 topRight = new Vector2(right, top);

        Vector2 point = new Vector2(x, y);

        if (x < left && y < bottom)
            return Vector2.Distance(point, bottomLeft) <= radius;

        if (x > right && y < bottom)
            return Vector2.Distance(point, bottomRight) <= radius;

        if (x < left && y > top)
            return Vector2.Distance(point, topLeft) <= radius;

        if (x > right && y > top)
            return Vector2.Distance(point, topRight) <= radius;

        return true;
    }

    private Texture2D MakeReadableCopy(Texture source, int width, int height)
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(source, renderTexture);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Texture2D readableTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        readableTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);

        return readableTexture;
    }
}