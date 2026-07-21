using UnityEngine;
using UnityEngine.XR;

public class VRPanelToggle : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelRoot;

    [Header("Controller")]
    public XRNode controllerNode = XRNode.RightHand;

    [Header("Button")]
    public bool useBButton = true;

    private bool wasButtonPressed;

    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (!device.isValid)
            return;

        bool buttonPressed = false;

        if (useBButton)
        {
            // Rechter Controller: B-Taste
            device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed);
        }
        else
        {
            // Rechter Controller: A-Taste
            device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed);
        }

        if (buttonPressed && !wasButtonPressed)
        {
            TogglePanel();
        }

        wasButtonPressed = buttonPressed;
    }

    public void TogglePanel()
    {
        if (panelRoot == null)
        {
            Debug.LogWarning("Kein Panel Root im VRPanelToggle gesetzt.");
            return;
        }

        panelRoot.SetActive(!panelRoot.activeSelf);
    }

    public void ShowPanel()
    {
        if (panelRoot != null)
            panelRoot.SetActive(true);
    }

    public void HidePanel()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);
    }
}