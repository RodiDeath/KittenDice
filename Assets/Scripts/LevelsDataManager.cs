using UnityEngine;
using System.Collections;

public class LevelsDataManager : MonoBehaviour
{
    public static void SaveLevelData(string world, int level, int score, int stars)
    {

        //Debug.Log("SAVING: key:"+ world + "*" + level + "|value:"+ stars + "*" + score);
        PlayerPrefs.SetString(world +  "*" + level, stars + "*" + score); // ("1*1" , "3*123456789")

    }

    public static string[] GetLevelsData(string world)
    {
        string[] levelsData;

        int levelCount = LevelManager.GetAllLevelStrings(world).Count;

        levelsData = new string[levelCount];

        for (int i = 1; i <= levelCount; i++)
        {
            
            if (PlayerPrefs.HasKey(world+ "*" + i))
            {
                levelsData[i-1] = PlayerPrefs.GetString(world + "*" + i);
            }
            else
            {
                PlayerPrefs.SetString(world+i, "0*0");
                levelsData[i-1] = PlayerPrefs.GetString(world + "*" + i);
            }

            //levelsData[i] = PlayerPrefs.GetString(world + "*" + i);
            //Debug.Log("GETTING: key:" + world + "*" + i + "|value:" + levelsData[i-1]);
        }


        return levelsData;
    }

    public static string[] GetLevelData(string world, int level)
    {
        string []levelsData;

        
        levelsData = PlayerPrefs.GetString(world + "*" + level).Split('*');

        return levelsData;
    }

}
