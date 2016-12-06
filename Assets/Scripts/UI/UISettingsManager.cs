using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        //dropdownLanguage.value = dropdownLanguage.options

        Debug.Log("SettingsLoaded");

    }

    public void SaveSettings()
    {
        if (sliderControls.value == 0) Storage.SaveControls(StorageKeys.Pad);
        else Storage.SaveControls(StorageKeys.Swipe);
        
        Storage.SaveEffectsVolume(sliderEffectsVol.value);
        Storage.SaveMusicVolume(sliderMusicVol.value);
        //Storage.SaveLanguage();

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
