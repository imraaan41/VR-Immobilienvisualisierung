using UnityEngine;

[CreateAssetMenu(fileName = "NewDesignOption", menuName = "ImmobilienVR/Design Option")]
public class DesignOption : ScriptableObject
{
    [Header("Option Info")]
    public string optionName;

    [Header("Target")]
    public SurfaceType targetSurfaceType;

    [Header("Material")]
    public Material material;

    [Header("UI Preview")]
    public Texture2D previewTexture;
    public Color previewColor = Color.white;
}