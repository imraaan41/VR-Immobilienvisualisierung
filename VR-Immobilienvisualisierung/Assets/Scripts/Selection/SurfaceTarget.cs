using UnityEngine;

public class SurfaceTarget : MonoBehaviour
{
    [Header("Surface Info")]
    public SurfaceType surfaceType;
    public string roomName;
    public string displayName;

    [Header("Renderer")]
    public Renderer targetRenderer;
    public int materialSlotIndex = 0;

    private Material originalMaterial;

    private void Reset()
    {
        targetRenderer = GetComponent<Renderer>();
        displayName = gameObject.name;
    }

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            displayName = gameObject.name;
        }

        if (targetRenderer != null && targetRenderer.sharedMaterials.Length > materialSlotIndex)
        {
            originalMaterial = targetRenderer.sharedMaterials[materialSlotIndex];
        }
    }

    public void ApplyMaterial(Material newMaterial)
    {
        if (targetRenderer == null)
        {
            Debug.LogWarning("Kein Renderer gefunden auf: " + gameObject.name);
            return;
        }

        if (newMaterial == null)
        {
            Debug.LogWarning("Kein Material übergeben.");
            return;
        }

        Material[] materials = targetRenderer.sharedMaterials;

        if (materialSlotIndex < 0 || materialSlotIndex >= materials.Length)
        {
            Debug.LogWarning("Material Slot Index ungültig bei: " + gameObject.name);
            return;
        }

        materials[materialSlotIndex] = newMaterial;
        targetRenderer.sharedMaterials = materials;
    }

    public void ResetMaterial()
    {
        if (originalMaterial == null)
        {
            Debug.LogWarning("Kein Originalmaterial gespeichert bei: " + gameObject.name);
            return;
        }

        ApplyMaterial(originalMaterial);
    }
}