using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject myObject;
    public GameObject parentObject;

    public Camera mainCamera;
    public float distanceFromCamera = 2.0f;


    void Start()
    {
        // Set mainCamera to the main camera in the scene
        mainCamera = Camera.main;
    }
    public void Spawn()
    {
        if (PlayerPrefs.GetInt("IsLogedIn", 0) == 1)
        {
            GameObject spawnedObject = Instantiate(myObject);
            spawnedObject.transform.SetParent(parentObject.transform);

            spawnedObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
        }

    }
}
