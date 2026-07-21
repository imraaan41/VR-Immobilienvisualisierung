using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class VRUIButtonClicker : MonoBehaviour
{
    [Header("Ray Origin")]
    public Transform rayOrigin;

    [Header("Raycast")]
    public float maxDistance = 10f;
    public LayerMask uiLayers = ~0;

    [Header("Click Settings")]
    public float clickCooldown = 0.35f;

    private bool wasTriggerPressed;
    private float lastClickTime = -999f;

    private void Update()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (!rightHand.isValid)
            return;

        bool triggerPressed = false;
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed && !wasTriggerPressed)
        {
            TryClickUIButton();
        }

        wasTriggerPressed = triggerPressed;
    }

    private void TryClickUIButton()
    {
        if (Time.time - lastClickTime < clickCooldown)
            return;

        if (rayOrigin == null)
        {
            Debug.LogWarning("Kein Ray Origin für VRUIButtonClicker gesetzt.");
            return;
        }

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 1f);

        RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, uiLayers);
        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            Button button = hit.collider.GetComponentInParent<Button>();

            if (button != null && button.interactable && button.gameObject.activeInHierarchy)
            {
                lastClickTime = Time.time;

                Debug.Log("VR UI Button geklickt: " + button.name);
                button.onClick.Invoke();

                return;
            }
        }

        Debug.Log("Kein UI Button getroffen.");
    }
}