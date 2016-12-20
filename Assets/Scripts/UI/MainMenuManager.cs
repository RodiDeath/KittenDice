using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject panelSettings;
    [SerializeField]
    Button btnPlay;
    [SerializeField]
    Button btnSettings;
    [SerializeField]
    Button btnProfile;
    [SerializeField]
    Button btnStatistics;
    [SerializeField]
    Button btnShop;
    [SerializeField]
    Button btnFacebook;
    [SerializeField]
    Button btnTwitter;

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	public void Play()
    {
        SceneManager.LoadScene("WorldSelector");
    }

    public void Settings()
    {
        btnPlay.interactable = false;
        btnSettings.interactable = false;
        btnProfile.interactable = false;
        btnStatistics.interactable = false;
        btnShop.interactable = false;
        btnFacebook.interactable = false;
        btnTwitter.interactable = false;


        panelSettings.SetActive(true);
        panelSettings.GetComponent<UISettingsManager>().LoadSettings();
    }

}
