using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManagerPractice : MonoBehaviour
{
    [SerializeField]
    private XRNode xRNode = XRNode.LeftHand;

    [SerializeField] Text text;

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    public GameObject cube;

    //to avoid repeat readings
    private bool secondaryIsPressed;
    private bool primaryIsPressed;
    private bool triggerIsPressed;
    public GameObject rig;

    public Animator transition;

    public Transform[] location;
    public GameObject prefab;
    public float spawntime;
    public float spawndelay;

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xRNode, devices);
        device = devices.FirstOrDefault();
    }

    private void Start()
    {
        //InvokeRepeating("spawn", 5, 3);
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

        bool secondaryButtonValue = false;
        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue && !secondaryIsPressed)
        {
            secondaryIsPressed = true;
            SceneManager.LoadScene("GUI");
        }
        else if (!secondaryButtonValue && secondaryIsPressed)
        {
            secondaryIsPressed = false;
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

        bool triggerButtonValue = false;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue && !triggerIsPressed)
        {
            triggerIsPressed = true;
            rig.GetComponent<CharacterController>().height = 1f;
        }
        else if (!triggerButtonValue && triggerIsPressed)
        {
            triggerIsPressed = false;
            rig.GetComponent<CharacterController>().height = 2f;
        }
    }
}
