using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private XRNode xRNode = XRNode.LeftHand;

    [SerializeField] Text text;

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    public GameObject cube;

    //to avoid repeat readings
    private bool triggerIsPressed;
    private bool primaryIsPressed;
    private bool gripIsPressed;

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xRNode, devices);
        device = devices.FirstOrDefault();
    }

    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }

    void Update()
    {
        if (!device.isValid)
        {
            GetDevice();
        }

        // capturing trigger button press and release    
        bool triggerButtonValue = false;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue && !triggerIsPressed)
        {
            triggerIsPressed = true;
            cube.transform.position = transform.position;
            cube.transform.rotation = transform.rotation;
            cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cube.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else if (!triggerButtonValue && triggerIsPressed)
        {
            triggerIsPressed = false;
        }

        bool primaryButtonValue = false;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue && !primaryIsPressed)
        {
            primaryIsPressed = true;
        }
        else if (!primaryButtonValue && primaryIsPressed)
        {
            primaryIsPressed = false;
        }

        bool gripButtonValue = false;
        if (device.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonValue) && gripButtonValue && !gripIsPressed)
        {
            gripIsPressed = true;
        }
        else if (!gripButtonValue && gripIsPressed)
        {
            gripIsPressed = false;
        }
    }
    }
