using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SurfaceSelector : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;

    [Header("Selection")]
    public SurfaceTarget currentTarget;

    public event Action<SurfaceTarget> OnSelectionChanged;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectWithMouse();
        }
    }

    private void TrySelectWithMouse()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera fehlt im SurfaceSelector.");
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            SurfaceTarget target = hit.collider.GetComponentInParent<SurfaceTarget>();

            if (target != null)
            {
                SelectTarget(target);
            }
        }
    }

    public void SelectTarget(SurfaceTarget target)
    {
        currentTarget = target;

        Debug.Log(
            "Ausgewählt: "
            + target.displayName
            + " | Typ: "
            + target.surfaceType
            + " | Raum: "
            + target.roomName
        );

        OnSelectionChanged?.Invoke(currentTarget);
    }

    public void ClearSelection()
    {
        currentTarget = null;
        OnSelectionChanged?.Invoke(null);
    }
}