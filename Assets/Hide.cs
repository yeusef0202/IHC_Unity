using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.XR;






public class Hide : MonoBehaviour
{
    [SerializeField]
    [Tooltip("GameObject To Show or Hide")]
    GameObject m_plane;

    private bool is_active;
    private XRBaseInteractor interactor;
    //public Component coisa;
    private XRInteractableSnapVolume[] snapVolumeComponents;
    private XRInteractableSnapVolume snapSimple;
    private XRInteractableSnapVolume snapGrab;
    private bool simple_grab;

    //Controller 
   



    // Start is called before the first frame update
    void Start()
    {
        m_plane.SetActive(true);
        is_active = false;
        simple_grab= true;      //True = simple; False = grab
        //coisa.GetComponet<>
        //coisa.enabled = !coisa.enabled;
        //= GetComponent<InputHelpers.Button>();

        //To deactivate and activate 
        snapVolumeComponents = GetComponents<XRInteractableSnapVolume>();
        snapSimple = snapVolumeComponents[0];
        snapGrab = snapVolumeComponents[1];

        //Controller stuff
        //var inputDevices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        //foreach (var device in inputDevices)
        //{
        //    Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        //}

      


    }

    public void Show_or_Hide()
    {

        m_plane.SetActive(!m_plane.activeSelf);

        //if (is_active)
        //{
        //    m_plane.SetActive(false);
        //    is_active = false;
        //    
        //}
        //else
        //{
        //    m_plane.SetActive(true);
        //    is_active = true;
        //    //eak;
        //} 
        
    }

    public void Activate_or_Deactivate() 
    {
        if (simple_grab)
        {
            //Activate Simple
            snapGrab.enabled = false;
            snapSimple.enabled = false;
            snapSimple.enabled = true;
            simple_grab = false;
        }
        else 
        {
            //Activate Grab
            snapGrab.enabled = false;
            snapSimple.enabled = false;
            snapGrab.enabled = true;
            simple_grab = true;
        }

    }
    

}
