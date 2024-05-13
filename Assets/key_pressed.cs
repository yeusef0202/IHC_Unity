using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.XR;


//Controller stuf
[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }


public class key_pressed : MonoBehaviour
{
    //public Component coisa;
    private XRInteractableSnapVolume[] snapVolumeComponents;
    private XRInteractableSnapVolume snapSimple;
    private XRInteractableSnapVolume snapGrab;
    private bool simple_grab;


    public PrimaryButtonEvent primaryButtonPress;
    public PrimaryButtonEvent yButtonPress;


    private bool lastButtonState = false;       
    private bool lastYButtonState = false; // Declare the variable lastYButtonState
    private List<InputDevice> devicesWithPrimaryButton;


    // Start is called before the first frame update
    void Start()
    {
        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        if (yButtonPress == null)
        {
            yButtonPress = new PrimaryButtonEvent();
        }

        devicesWithPrimaryButton = new List<InputDevice>();

    }



    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithPrimaryButton.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
        {
            devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithPrimaryButton.Contains(device))
            devicesWithPrimaryButton.Remove(device);
    }


    private bool pressed = false;



    void Update()
    {
        bool tempState = false;
        bool tempYButtonState = false;
        foreach (var device in devicesWithPrimaryButton)
        {
            bool primaryButtonState = false;
            bool yButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) // did get a value
                        && primaryButtonState // the value we got
                        || tempState; // cumulative result from other controllers

            tempYButtonState = device.TryGetFeatureValue(CommonUsages.secondaryButton, out yButtonState) // did get a value
                        && yButtonState // the value we got
                        || tempYButtonState; // cumulative result from other controllers
        }


        if (tempState && !lastButtonState) // Button pressed and wasn't pressed last frame
        {
            primaryButtonPress.Invoke(pressed);
            pressed = !pressed;
        }

        if (tempYButtonState && !lastYButtonState) // Y Button pressed and wasn't pressed last frame
        {
            // Invoke your Y button event here
            yButtonPress.Invoke(pressed);
            pressed = !pressed;
        }

        lastButtonState = tempState;
        lastYButtonState = tempYButtonState;
    }


}
