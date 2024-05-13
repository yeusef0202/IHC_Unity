using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;


public class Activate : MonoBehaviour
{

    private XRInteractableSnapVolume[] allComponents;
    private XRInteractableSnapVolume[] snapSimple = new XRInteractableSnapVolume[100];
    private XRInteractableSnapVolume[] snapGrab = new XRInteractableSnapVolume[100];
    private bool simple_grab = true;


    // Start is called before the first frame update
    void Start()
    {
        //simple_grab= true;      //True = simple; False = grab

        allComponents = GetComponentsInChildren<XRInteractableSnapVolume>();
        int j = 0;
        int k = 0;
        //Debug.Log(allComponents.Length);
        for(int i = 0; i<allComponents.Length; i++){

            if(i%2 == 0){
                Debug.Log(allComponents[i]);
                snapSimple[j] = allComponents[i];
                j++;
            }else{
                Debug.Log(allComponents[i]);
                snapGrab[k] = allComponents[i];
                k++;
            }

        }

    }

    // Update is called once per frame
    public void Activate_or_Deactivate() 
    {
        Start();
        if (simple_grab)
        {
            //Activate Simple
            for(int i = 0; i<snapGrab.Length; i++){
                snapGrab[i].enabled = false;
                snapSimple[i].enabled = false;
                snapSimple[i].enabled = true;
                simple_grab = false;
            }
            
        }
        else 
        {
            //Activate Grab
            for(int i = 0; i<snapGrab.Length; i++){
                snapGrab[i].enabled = false;
                snapSimple[i].enabled = false;
                snapGrab[i].enabled = true;
                simple_grab = true;
            }
        }

    }
}
