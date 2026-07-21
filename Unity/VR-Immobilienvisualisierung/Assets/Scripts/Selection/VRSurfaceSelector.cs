using UnityEngine;
using UnityEngine.XR;

public class VRSurfaceSelector : MonoBehaviour
{
    [Header("Ray Origin")]
    public Transform rayOrigin;

    [Header("Raycast")]
    public float maxDistance = 20f;
    public LayerMask selectableLayers = ~0;

    [Header("Selection")]
    public SurfaceTarget currentTarget;

    private bool wasTriggerPressed;

    private void Update()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (!rightHand.isValid)
        {
            return;
        }

        bool triggerPressed = false;
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed && !wasTriggerPressed)
        {
            TrySelectSurface();
        }

        wasTriggerPressed = triggerPressed;
    }

    private void TrySelectSurface()
    {
        if (rayOrigin == null)
        {
            Debug.LogWarning("Kein Ray Origin im VRSurfaceSelector gesetzt.");
            return;
        }

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, selectableLayers))
        {
            SurfaceTarget target = hit.collider.GetComponentInParent<SurfaceTarget>();

            if (target != null)
            {
                currentTarget = target;

                Debug.Log(
                    "VR ausgewählt: " +
                    target.displayName +
                    " | Typ: " +
                    target.surfaceType +
                    " | Raum: " +
                    target.roomName
                );
            }
            else
            {
                Debug.Log("Getroffenes Objekt hat kein SurfaceTarget: " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("Keine Fläche getroffen.");
        }
    }
}