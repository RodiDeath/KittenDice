using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UISettingsManager : MonoBehaviour
{
    [SerializeField]
    Slider sliderControls;

    [SerializeField]
    Slider sliderMusicVol;

    [SerializeField]
    Slider sliderEffectsVol;

    [SerializeField]
    Dropdown dropdownLanguage;

    [SerializeField]
    Button btnPlay;

    [SerializeField]
    Button btnSettings;

    [SerializeField]
    MainMenuLanguageManager MMLangManager;

    // Use this for initialization
    void Start ()
    {
	    if (Storage.GetFirstTime() == 1)
        {
            Debug.Log("FirsTime");
            SaveDefaultValues();
        }
    }


    public void LoadSettings()
    {
        if (Storage.GetControls().Equals(StorageKeys.Pad)) sliderControls.value = 0;
        else sliderControls.value = 1;

        sliderEffectsVol.value = Storage.GetEffectsVolume();
        sliderMusicVol.value = Storage.GetMusicVolume();

        List<string> allLanguages = LanguageManager.GetAllLanguages();

        dropdownLanguage.options.Clear();
        foreach (string lang in allLanguages)
        {
            dropdownLanguage.options.Add(new Dropdown.OptionData() { text = lang });
        }

        dropdownLanguage.value = allLanguages.IndexOf(Storage.GetLanguage());

        Debug.Log("SettingsLoaded");

    }

    public void SaveSettings()
    {
        if (sliderControls.value == 0) Storage.SaveControls(StorageKeys.Pad);
        else Storage.SaveControls(StorageKeys.Swipe);
        
        Storage.SaveEffectsVolume(sliderEffectsVol.value);
        Storage.SaveMusicVolume(sliderMusicVol.value);

        Storage.SaveLanguage(dropdownLanguage.options[dropdownLanguage.value].text);

        LanguageManager.LoadLanguageDocument(Storage.GetLanguage());

        MMLangManager.LoadLangStrings();

        Debug.Log("Settings Stored");

        btnPlay.interactable = true;
        btnSettings.interactable = true;
        gameObject.SetActive(false);
    }

    private static void SaveDefaultValues()
    {
        Storage.SaveFirstTime(0);
        Storage.SaveControls(StorageKeys.Pad);
        Storage.SaveEffectsVolume(0.8f);
        Storage.SaveMusicVolume(0.8f);
        Storage.SaveLanguage(LanguageKeys.English);

        Debug.Log("Stored Default Values");
    }

    public void Cancel()
    {
        btnPlay.interactable = true;
        btnSettings.interactable = true;
        gameObject.SetActive(false);
    }
}
