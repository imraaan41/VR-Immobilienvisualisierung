using UnityEngine;

public class DesignOptionButton : MonoBehaviour
{
    [Header("References")]
    public MaterialApplyManager materialApplyManager;

    [Header("Design Option")]
    public DesignOption designOption;

    public void ApplyOption()
    {
        if (materialApplyManager == null)
        {
            Debug.LogWarning("MaterialApplyManager fehlt beim Button.");
            return;
        }

        if (designOption == null)
        {
            Debug.LogWarning("DesignOption fehlt beim Button.");
            return;
        }

        materialApplyManager.ApplyDesignOption(designOption);
    }
}