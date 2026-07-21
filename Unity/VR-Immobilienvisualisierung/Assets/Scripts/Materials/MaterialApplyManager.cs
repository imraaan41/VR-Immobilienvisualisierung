using UnityEngine;

public class MaterialApplyManager : MonoBehaviour
{
    [Header("References")]
    public SurfaceSelector surfaceSelector;
    public VRSurfaceSelector vrSurfaceSelector;

    public void ApplyDesignOption(DesignOption option)
    {
        Debug.Log("ApplyDesignOption wurde aufgerufen.");

        if (option == null)
        {
            Debug.LogWarning("Keine DesignOption übergeben.");
            return;
        }

        SurfaceTarget target = GetCurrentTarget();

        if (target == null)
        {
            Debug.LogWarning("Keine Fläche ausgewählt.");
            return;
        }

        if (target.surfaceType != option.targetSurfaceType)
        {
            Debug.LogWarning(
                "Falscher Typ. Ausgewählt: " +
                target.surfaceType +
                ", Option ist für: " +
                option.targetSurfaceType
            );
            return;
        }

        if (option.material == null)
        {
            Debug.LogWarning("Kein Material in DesignOption: " + option.optionName);
            return;
        }

        target.ApplyMaterial(option.material);

        Debug.Log("Material angewendet: " + option.optionName + " auf " + target.displayName);
    }

    private SurfaceTarget GetCurrentTarget()
    {
        if (vrSurfaceSelector != null && vrSurfaceSelector.currentTarget != null)
        {
            return vrSurfaceSelector.currentTarget;
        }

        if (surfaceSelector != null && surfaceSelector.currentTarget != null)
        {
            return surfaceSelector.currentTarget;
        }

        return null;
    }
}