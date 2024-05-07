using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayCast : MonoBehaviour
{

    public void Things()
    { 
        Debug.Log("Trigger Clicked on Cube!");

        // Example: Change cube color on click
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}