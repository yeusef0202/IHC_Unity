using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Login_Manager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("GameObject To Show or Hide")]
    GameObject m_plane;


    public GameObject usernameInput;
    public GameObject passwordInput;

    public GameObject Activate;
    public static Login_Manager Instance { get; private set; }


    void Start()
    {
    }

    public void OnLoginButtonClicked()
    {
        // Debug.Log(usernameInput.GetComponentAtIndex(3));
        // Debug.Log(usernameInput.GetComponent<TMPro.TMP_InputField>());

        string username = usernameInput.GetComponent<TMPro.TMP_InputField>().text;
        string password = passwordInput.GetComponent<TMPro.TMP_InputField>().text;

        if ((username == "admin" && password == "admin"))
        {
            //Debug.Log("Login successful!");
            // Perform actions after successful login, like loading the main game scene
            m_plane.SetActive(!m_plane.activeSelf);
            PlayerPrefs.SetInt("IsLogedIn", 1);
            Debug.Log("Logged In:");
            Debug.Log(PlayerPrefs.GetInt("IsLogedIn"));
            Activate.GetComponent<Activate>().Activate_or_Deactivate();
            
        }
        else
        {
            Debug.Log("Login failed. Invalid username or password.");
            // Display error message to the user
            // PlayerPrefs.SetInt("IsLogedIn", 0);

        }
    }

    public void LogOut(){
        PlayerPrefs.SetInt("IsLogedIn", 0);
        m_plane.SetActive(!m_plane.activeSelf);
        Activate.GetComponent<Activate>().Activate_or_Deactivate();
    }


}
