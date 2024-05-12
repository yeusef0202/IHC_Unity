using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
using System;



public class Api_manager : MonoBehaviour
{

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(String uri){
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch(webRequest.result) {

                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Root root = JsonConvert.DeserializeObject<Root>(webRequest.downloadHandler.text);
                    Debug.Log(root.objects[0].description);
                    text.text = root.objects[0].description;
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
