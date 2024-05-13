using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
using System;
using UnityEditor.UI;
using UnityEngine.Assertions.Must;



public class Api_manager : MonoBehaviour
{

    //public TextMeshProUGUI text;
    private GameObject[] allIDGameObjects = new GameObject[100];
    private Component[] allcomponents = new Component[100];

    private Root root;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("https://ihc.rui2015.me/api/items"));


        // Get all the ID components
        int i = 0;
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Hitbox"))
            {
                Debug.Log(child.name);
                allIDGameObjects[i++] = child.Find("API_ID").Find("ID").gameObject;
            }
        }
        // 
        // for (int k = 0; k < i; k++)
        // {
        //     Debug.Log(((TMPro.TMP_InputField)allcomponents[k]).text);
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateComponents()
    {
        StartCoroutine(Update_Components());
    }

    IEnumerator Update_Components()
    {
        // wait 2 seconds to be sure that the "ID" Camp is updated
        yield return new WaitForSecondsRealtime(2);
        // Rerun to see if new things have appeared
        Start();

        Debug.Log(root.objects.Count);
        for( int i = 0; i < root.objects.Count; i++){
            foreach (GameObject go in allIDGameObjects){
                if(go != null){
                    Debug.Log(go.name);
                }
                if (go!=null && go.GetComponent<TMPro.TMP_InputField>().text == root.objects[i].id){
                    Debug.Log(go.GetComponent<TMPro.TMP_InputField>().text);
                    Debug.Log(go.transform.parent.name);
                    Debug.Log(go.transform.parent.parent.name);
                    Debug.Log("-----------------------------------");
                    Debug.Log(go.transform.parent.parent.Find("--DescriptionMenu"));
                    go.transform.parent.parent.Find("--DesriptionMenu").Find("--TextDescription").GetComponent<TextMeshProUGUI>().text = root.objects[i].description;
                    Debug.Log("CHANGED");
                }
            }

        }
    }


    IEnumerator GetRequest(String uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {

                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    root = JsonConvert.DeserializeObject<Root>(webRequest.downloadHandler.text);
                    //Debug.Log(root.objects[0].description);
                    //text.text = root.objects[0].description;
                    break;
            }
        }
    }

    public class Object
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<string> images { get; set; }
        public List<string> videos { get; set; }
    }

    public class Root
    {
        public List<Object> objects { get; set; }
    }



}