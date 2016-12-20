using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuLanguageManager : MonoBehaviour
{
    private LanguageManager languageManager;

    [SerializeField]
    private Button btnPlay;
    [SerializeField]
    private Button btnSettings;
    [SerializeField]
    private Text txtSettings;
    [SerializeField]
    private Text txtControls;
    [SerializeField]
    private Text txtMusicVolume;
    [SerializeField]
    private Text txtEffectsVolume;
    [SerializeField]
    private Text txtLanguage;
    [SerializeField]
    private Button btnCancelSettings;
    [SerializeField]
    private Button btnOKSettings;

    void Start()
    {
        LoadLangStrings();
    }

    public void LoadLangStrings()
    {
        languageManager = FindObjectOfType<LanguageManager>();

        //btnPlay.GetComponentInChildren<Text>().text = languageManager.GetString("play");
        //btnSettings.GetComponentInChildren<Text>().text = languageManager.GetString("settings");
        btnCancelSettings.GetComponentInChildren<Text>().text = languageManager.GetString("cancel");
        btnOKSettings.GetComponentInChildren<Text>().text = languageManager.GetString("ok");

        txtSettings.text = languageManager.GetString("settings");
        txtControls.text = languageManager.GetString("controls");
        txtMusicVolume.text = languageManager.GetString("music_volume");
        txtEffectsVolume.text = languageManager.GetString("effects_volume");
        txtLanguage.text = languageManager.GetString("language");
    }
}
