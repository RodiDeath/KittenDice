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

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	public void Play()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void Settings()
    {
        btnPlay.interactable = false;
        btnSettings.interactable = false;

        panelSettings.SetActive(true);
        panelSettings.GetComponent<UISettingsManager>().LoadSettings();
    }

}
