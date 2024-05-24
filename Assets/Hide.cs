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

    [SerializeField]
    [Tooltip("The Menu with all the Hitboxes")]
    public GameObject Main_Menu;

    public GameObject keyboard;
    private bool is_active;
    private XRBaseInteractor interactor;
    //public Component coisa;
    private XRInteractableSnapVolume[] snapVolumeComponents;
    private XRInteractableSnapVolume snapSimple;
    private XRInteractableSnapVolume snapGrab;



    //Controller 




    // Start is called before the first frame update
    void Start()
    {
        m_plane.SetActive(false);
        is_active = false;
        // PlayerPrefs.DeleteAll();          //True = simple; False = grab

        //To deactivate and activate 
        snapVolumeComponents = GetComponents<XRInteractableSnapVolume>();
        if (snapVolumeComponents.Length == 2)
        {
            snapSimple = snapVolumeComponents[0];
            snapGrab = snapVolumeComponents[1];
        }

    }

    public void Show_or_Hide()
    {

        m_plane.SetActive(!m_plane.activeSelf);
        try
        {
            m_plane.transform.parent.Find("--ImagesMenu").gameObject.SetActive(false);
            m_plane.transform.parent.Find("--DesriptionMenu").gameObject.SetActive(false);
            m_plane.transform.parent.Find("--VideosMenu").gameObject.SetActive(false);

        }
        catch (System.Exception)
        {
            return;
        }

    }

    public void Show_or_Hide_Login()
    {
        Debug.Log(PlayerPrefs.GetInt("IsLogedIn"));
        GameObject loginButton = m_plane.transform.Find("Login_Button").gameObject;
        GameObject logoutButton = m_plane.transform.Find("LogOut_Button").gameObject;
        GameObject canvas = m_plane.transform.Find("Canvas").gameObject;
        if (PlayerPrefs.GetInt("IsLogedIn") == 1)
        {
            m_plane.SetActive(!m_plane.activeSelf);
            // Hide the login button and show the logout button
            loginButton.SetActive(false);
            logoutButton.SetActive(true);
            canvas.SetActive(false);
            keyboard.SetActive(false);

        }
        else
        {
            m_plane.SetActive(!m_plane.activeSelf);

            loginButton.SetActive(true);
            logoutButton.SetActive(false);
            canvas.SetActive(true);
            keyboard.SetActive(false);
        }
    }

    public void Show_or_Hide_Ids_and_Hitboxes()
    {
        foreach (Transform child in Main_Menu.transform)
        {
            if (child.name.StartsWith("Hitbox") & PlayerPrefs.GetInt("IsLogedIn") == 1)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = true;
                child.Find("API_ID").gameObject.SetActive(true);
                child.Find("Delete").gameObject.SetActive(true);
            }
            else if (child.name.StartsWith("Hitbox") & PlayerPrefs.GetInt("IsLogedIn") == 0)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = false;
                child.Find("API_ID").gameObject.SetActive(false);
                child.Find("Delete").gameObject.SetActive(false);
            }
        }

    }

}
