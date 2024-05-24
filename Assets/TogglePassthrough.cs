using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TogglePassthrough : MonoBehaviour
{
   private ARCameraManager arCameraManager;
 
   private void Awake()
   {
      arCameraManager = FindAnyObjectByType<ARCameraManager>();
      if(arCameraManager)
         arCameraManager.enabled = !arCameraManager.enabled;
   }

}