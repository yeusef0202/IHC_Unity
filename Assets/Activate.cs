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
                snapGrab[j] = allComponents[i];
                j++;
            }else{
                Debug.Log(allComponents[i]);
                snapSimple[k] = allComponents[i];
                k++;
            }

        }

    }

    // Update is called once per frame
    public void Activate_or_Deactivate() 
    {
        Start();
        if (PlayerPrefs.GetInt("IsLogedIn") == 1)
        {
            //Activate Grab
            Debug.Log("Grab");
            for(int i = 0; i<snapGrab.Length; i++){
                if(snapGrab[i] == null){
                    break;
                }
                snapGrab[i].enabled = false;
                snapSimple[i].enabled = false;
                snapGrab[i].enabled = true;
                // Debug.Log(snapGrab[i].gameObject);
                // Debug.Log("Enabled");
            }
        }
        else 
        {
            //Activate Simple
            Debug.Log("Simple");
            for(int i = 0; i<snapGrab.Length; i++){
                if(snapGrab[i] == null){
                    break;
                }
                snapGrab[i].enabled = false;
                snapSimple[i].enabled = false;
                snapSimple[i].enabled = true;

            }
            
        }

    }
}
