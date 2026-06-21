using UnityEngine;

public class MaterialApplyManager : MonoBehaviour
{
    [Header("References")]
    public SurfaceSelector surfaceSelector;

    public void ApplyDesignOption(DesignOption option)
    {
        if (option == null)
        {
            Debug.LogWarning("Keine DesignOption übergeben.");
            return;
        }

        if (surfaceSelector == null)
        {
            Debug.LogWarning("SurfaceSelector fehlt im MaterialApplyManager.");
            return;
        }

        SurfaceTarget selectedTarget = surfaceSelector.currentTarget;

        if (selectedTarget == null)
        {
            Debug.LogWarning("Keine Fläche ausgewählt.");
            return;
        }

        if (selectedTarget.surfaceType != option.targetSurfaceType)
        {
            Debug.LogWarning(
                "Diese Option passt nicht. Ausgewählt: "
                + selectedTarget.surfaceType
                + ", Option ist für: "
                + option.targetSurfaceType
            );
            return;
        }

        selectedTarget.ApplyMaterial(option.material);

        Debug.Log(
            "Material angewendet: "
            + option.optionName
            + " auf "
            + selectedTarget.displayName
        );
    }

    public void ResetSelectedMaterial()
    {
        if (surfaceSelector == null)
        {
            Debug.LogWarning("SurfaceSelector fehlt im MaterialApplyManager.");
            return;
        }

        if (surfaceSelector.currentTarget == null)
        {
            Debug.LogWarning("Keine Fläche ausgewählt.");
            return;
        }

        surfaceSelector.currentTarget.ResetMaterial();
    }
}