using UnityEngine;
using System.Collections;
using System;

public class Storage
{
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

    // Lifes
    public static void SaveLifes(int lifes)
    {
        PlayerPrefs.SetInt(StorageKeys.Lifes, lifes);
    }

    public static int GetLifes()
    {
        if (PlayerPrefs.HasKey(StorageKeys.Lifes))
        {
            return PlayerPrefs.GetInt(StorageKeys.Lifes);
        }
        else
        {
            return 5;
        }
    }

    public static void SaveDateLastConnection()
    {
        DateTime now = DateTime.Now;

        int day = now.Day;
        int month = now.Month;
        int year = now.Year;
        int hour = now.Hour;
        int minute = now.Minute;
        int second = now.Second;

        string churroDate = year.ToString() + "*" + month.ToString() + "*" + day.ToString() + "*" + hour.ToString() + "*" + minute.ToString() + "*" + second.ToString();

        PlayerPrefs.SetString(StorageKeys.DateLastConection ,churroDate);
    }

    public static DateTime GetDateLastConnection()
    {
        if (PlayerPrefs.HasKey(StorageKeys.DateLastConection))
        {
            string[] dataDate = PlayerPrefs.GetString(StorageKeys.DateLastConection).Split('*');

            Convert.ToInt32(dataDate[0]);

            DateTime dateLastCon = new DateTime(
                Convert.ToInt32(dataDate[0]),
                Convert.ToInt32(dataDate[1]),
                Convert.ToInt32(dataDate[2]),
                Convert.ToInt32(dataDate[3]),
                Convert.ToInt32(dataDate[4]),
                Convert.ToInt32(dataDate[5])
                );

            return dateLastCon;
        }
        else
        {
            return DateTime.Now;
        }
    }

    // Seconds Passed
    public static void SaveSecondsPassed(int sp)
    {
        PlayerPrefs.SetInt(StorageKeys.SecondsPassed, sp);
    }

    public static int GetSecondsPassed()
    {
        if (PlayerPrefs.HasKey(StorageKeys.SecondsPassed))
        {
            return PlayerPrefs.GetInt(StorageKeys.SecondsPassed);
        }
        else
        {
            return 0;
        }
    }

}
