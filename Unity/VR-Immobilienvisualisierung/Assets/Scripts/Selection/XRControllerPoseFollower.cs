using UnityEngine;
using UnityEngine.XR;

public class XRControllerPoseFollower : MonoBehaviour
{
    public enum ControllerHand
    {
        Left,
        Right
    }

    [Header("Controller")]
    public ControllerHand hand = ControllerHand.Right;

    private InputDevice device;

    private void OnEnable()
    {
        FindDevice();
    }

    private void Update()
    {
        if (!device.isValid)
        {
            FindDevice();
        }

        if (!device.isValid)
        {
            return;
        }

        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position))
        {
            transform.localPosition = position;
        }

        if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
        {
            transform.localRotation = rotation;
        }
    }

    private void FindDevice()
    {
        XRNode node = hand == ControllerHand.Right ? XRNode.RightHand : XRNode.LeftHand;
        device = InputDevices.GetDeviceAtXRNode(node);
    }
}