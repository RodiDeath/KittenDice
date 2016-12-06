using UnityEngine;
using System.Collections;

public class Storage : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}

    // Language
    public static void SaveLanguage(string lang)
    {
        PlayerPrefs.SetString(StorageKeys.Language, lang);
    }

    public static string GetLanguage()
    {
        if (PlayerPrefs.HasKey(StorageKeys.Language))
        {
            return PlayerPrefs.GetString(StorageKeys.Language);
        }
        else
        {
            return "";
        }
    }
    

    // Controls
    public static void SaveControls(string type)
    {
        PlayerPrefs.SetString(StorageKeys.Controls, type);
    }

    public static string GetControls()
    {
        if (PlayerPrefs.HasKey(StorageKeys.Controls))
        {
            return PlayerPrefs.GetString(StorageKeys.Controls);
        }
        else
        {
            return "";
        }
    }


    // Music Volume
    public static void SaveMusicVolume(float vol)
    {
        PlayerPrefs.SetFloat(StorageKeys.MusicVolume, vol);
    }

    public static float GetMusicVolume()
    {
        if (PlayerPrefs.HasKey(StorageKeys.MusicVolume))
        {
            return PlayerPrefs.GetFloat(StorageKeys.MusicVolume);
        }
        else
        {
            return 1.0f;
        }
    }


    // Effects Volume
    public static void SaveEffectsVolume(float vol)
    {
        PlayerPrefs.SetFloat(StorageKeys.EffectsVolume, vol);
    }

    public static float GetEffectsVolume()
    {
        if (PlayerPrefs.HasKey(StorageKeys.EffectsVolume))
        {
            return PlayerPrefs.GetFloat(StorageKeys.EffectsVolume);
        }
        else
        {
            return 1.0f;
        }
    }


    // First Time
    public static void SaveFirstTime(int ft)
    {
        PlayerPrefs.SetInt(StorageKeys.FirstTime, ft);
    }

    public static int GetFirstTime()
    {
        if (PlayerPrefs.HasKey(StorageKeys.FirstTime))
        {
            return PlayerPrefs.GetInt(StorageKeys.FirstTime);
        }
        else
        {
            return 0;
        }
    }
}
