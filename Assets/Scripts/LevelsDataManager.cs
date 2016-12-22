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













    public static string[,] GetLevelsDataasdfsdf(string world)
    {
        string[,] levelDataTable;

        string levelDataPath = "Levels/LevelsData" + world; // Path of the txt level data file
        TextAsset levelData = (TextAsset)Resources.Load(levelDataPath, typeof(TextAsset)); // Stores the txt file in a TextAsset variable

        string[] splitFile = new string[] { "\r\n", "\r", "\n" }; // Set the split parameter (\r\n -> New Line)
        string[] levelDataLines = levelData.text.Split(splitFile, System.StringSplitOptions.None); // Stores in a string[] all the txt lines separately (the séparation)

        levelDataTable = new string[levelDataLines.Length, levelDataLines[0].Split('*').Length];

        for (int i = 0; i < levelDataLines.Length; i++)
        {
            for (int j = 0; j < levelDataLines[0].Split('*').Length; j++)
            {
                levelDataTable[i, j] = levelDataLines[i].Split('*')[j];
                //Debug.Log(levelDataTable[i,j]);
            }
        }

        return levelDataTable;
    }

}
