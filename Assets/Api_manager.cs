using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.Video;



public class Api_manager : MonoBehaviour
{

    //public TextMeshProUGUI text;
    private GameObject[] allIDGameObjects = new GameObject[100];
    private Component[] allcomponents = new Component[100];

    private Root root;
    // Start is called before the first frame update
    
    //Image Control
    
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

        // Debug.Log(root.objects.Count);
        for( int i = 0; i < root.objects.Count; i++){
            foreach (GameObject go in allIDGameObjects){
                if (go!=null && go.GetComponent<TMPro.TMP_InputField>().text == root.objects[i].id){
                    Update_all_components_of_a_gameobject(go,i);
                }
            }

        }
    }


    private void Update_all_components_of_a_gameobject(GameObject go, int index)
    {
        go.transform.parent.parent.Find("--DesriptionMenu").Find("--TextDescription").GetComponent<TextMeshProUGUI>().text = root.objects[index].description;
        //go.transform.parent.parent.Find("--ImagesMenu").Find("--Image1").GetComponent<RawImage>().texture = root.objects[index].images[0];
        StartCoroutine(LoadImageFromURL(root.objects[index].images[0], go.transform.parent.parent.Find("--ImagesMenu").Find("--Image1").GetComponent<RawImage>()));
        if(root.objects[index].videos.Count > 0){
            root.objects[index].Image_index = 0;
            go.transform.parent.parent.Find("--VideosMenu").Find("Player").gameObject.SetActive(true);
            LoadVideoFromURL(root.objects[index].videos[0], go.transform.parent.parent.Find("--VideosMenu").Find("Player").Find("Content").Find("Video Player").GetComponent<VideoPlayer>());
        }else{
            go.transform.parent.parent.Find("--VideosMenu").Find("Player").gameObject.SetActive(false);
        }
    }

    public void NextVideo(){
        Debug.Log(root.objects[1].Video_index);
        for( int i = 0; i < root.objects.Count; i++){
            foreach (GameObject go in allIDGameObjects){
                if (go!=null && go.GetComponent<TMPro.TMP_InputField>().text == root.objects[i].id && root.objects[i].Video_index < root.objects[i].videos.Count-1){
                    LoadVideoFromURL(root.objects[i].videos[++root.objects[i].Video_index], go.transform.parent.parent.Find("--VideosMenu").Find("Player").Find("Content").Find("Video Player").GetComponent<VideoPlayer>());
                }
            }
        }
    }

    public void PreviousVideo(){
        Debug.Log(root.objects[1].Video_index);
        for( int i = 0; i < root.objects.Count; i++){
            foreach (GameObject go in allIDGameObjects){
                if (go!=null && go.GetComponent<TMPro.TMP_InputField>().text == root.objects[i].id && root.objects[i].Video_index > 0){
                    LoadVideoFromURL(root.objects[i].videos[--root.objects[i].Video_index], go.transform.parent.parent.Find("--VideosMenu").Find("Player").Find("Content").Find("Video Player").GetComponent<VideoPlayer>());
                }
            }
        }
    }


    void LoadVideoFromURL(string url, VideoPlayer videoPlayer)
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;
        videoPlayer.Play();
    }

    public void NextImage(){
        Debug.Log(root.objects[1].Image_index);
        for( int i = 0; i < root.objects.Count; i++){
            foreach (GameObject go in allIDGameObjects){
                if (go!=null && go.GetComponent<TMPro.TMP_InputField>().text == root.objects[i].id && root.objects[i].Image_index < root.objects[i].images.Count-1){
                    StartCoroutine(LoadImageFromURL(root.objects[i].images[++root.objects[i].Image_index], go.transform.parent.parent.Find("--ImagesMenu").Find("--Image1").GetComponent<RawImage>()));
                }
            }
        }
    }

    public void PreviousImage(){
        Debug.Log(root.objects[1].Image_index);
        for( int i = 0; i < root.objects.Count; i++){
            foreach (GameObject go in allIDGameObjects){                   
                if (go!=null && go.GetComponent<TMPro.TMP_InputField>().text == root.objects[i].id && root.objects[i].Image_index > 0){
                    StartCoroutine(LoadImageFromURL(root.objects[i].images[--root.objects[i].Image_index], go.transform.parent.parent.Find("--ImagesMenu").Find("--Image1").GetComponent<RawImage>()));
                }
            }
        }
    }

    IEnumerator LoadImageFromURL(string url, RawImage rawImage)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(uwr);
                rawImage.texture = texture;
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
        public int Image_index { get; set; }
        public int Video_index { get; set; }
    }

    public class Root
    {
        public List<Object> objects { get; set; }
    }



}
