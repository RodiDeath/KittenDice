using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField]
    RectTransform contentScrollView;

    [SerializeField]
    RectTransform panelLevel;
    
    GameObject panelStars;

    private Vector3 levelScale;

    public static string world = "Air";
    int levelsPerRow = 3;
    int levelsShowed = 0;
    string[,] levelDataTable;

    //private ScrolView


    // Use this for initialization
    void Start ()
    {
        GetLevelsData();
        PopulateLevelSelector(GameManager.world);
        
    }

    public void GetLevelsData()
    {
        string levelDataPath = "Levels/LevelsData"; // Path of the txt level data file
        TextAsset levelData = (TextAsset)Resources.Load(levelDataPath, typeof(TextAsset)); // Stores the txt file in a TextAsset variable

        string[] splitFile = new string[] { "\r\n", "\r", "\n" }; // Set the split parameter (\r\n -> New Line)
        string[] levelDataLines = levelData.text.Split(splitFile, System.StringSplitOptions.None); // Stores in a string[] all the txt lines separately (the séparation)

        levelDataTable = new string[levelDataLines.Length, levelDataLines[0].Split('*').Length];

        for (int i = 0; i < levelDataLines.Length; i++)
        {
            for (int j = 0; j < levelDataLines[0].Split('*').Length; j++)
            {
                levelDataTable[i,j] = levelDataLines[i].Split('*')[j];
                //Debug.Log(levelDataTable[i,j]);
            }
        }


    }
	
    void PopulateLevelSelector(string world)
    {
        bool modelPanelDeleted = false;
        float startPositionX = -280f;
        float startPositionY = -150f;
        float initialX = -280f;
        float initialY = -150f;

        List<string> levels = LevelManager.GetAllLevelStrings(world);

        // UI TEST ONLY /////////////////
        //for (int i = 0; i < 21; i++)
        //{
        //    int levelN = i + 4;
        //    levels.Add("Level" + levelN);
        //}
        ////////////////////////////////



        foreach (var level in levels)
        {
            if (!modelPanelDeleted)
            {
                panelLevel = contentScrollView.GetChild(contentScrollView.childCount - 1).GetComponent<RectTransform>();
                contentScrollView.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);

                initialX = -280f;
                initialY = -150f;

                startPositionX = initialX;
                startPositionY = initialY;

                RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(startPositionX, startPositionY, 1f), Quaternion.identity) as RectTransform;

                newPanelLevel.name = Regex.Match(level, @"\d+").Value;
                newPanelLevel.GetComponentInChildren<Text>().text = newPanelLevel.name;

                newPanelLevel.SetParent(contentScrollView, false);
                Destroy(contentScrollView.GetChild(0).gameObject);
                modelPanelDeleted = true;

                startPositionX += newPanelLevel.rect.width + 30;

                /*************/
                /* LevelData */
                /*************/
                // Stars
                SetStars(newPanelLevel);
                // Score
                int levelIndex = Convert.ToInt32(newPanelLevel.name);
                Transform paneScore = newPanelLevel.transform.GetChild(2);
                paneScore.GetComponent<Text>().text = levelDataTable[levelIndex - 1, 3];


                levelsShowed++;
            }
            else
            {
                panelLevel = contentScrollView.GetChild(contentScrollView.childCount - 1).GetComponent<RectTransform>();

                RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(startPositionX, startPositionY, 1f), Quaternion.identity) as RectTransform;

                newPanelLevel.localPosition = new Vector3(startPositionX, startPositionY);

                newPanelLevel.name = Regex.Match(level, @"\d+").Value;
                newPanelLevel.GetComponentInChildren<Text>().text = newPanelLevel.name;

                newPanelLevel.SetParent(contentScrollView, false);
                
                startPositionX += newPanelLevel.rect.width+30;



                /*************/
                /* LevelData */
                /*************/
                // Stars
                SetStars(newPanelLevel);
                // Score
                int levelIndex = Convert.ToInt32(newPanelLevel.name);
                Transform paneScore = newPanelLevel.transform.GetChild(2);
                paneScore.GetComponent<Text>().text = levelDataTable[levelIndex - 1, 3];


                levelsShowed++;
            }

            if (levelsShowed == levelsPerRow)
            {
                levelsShowed = 0;
                startPositionX = initialX;
                startPositionY -= panelLevel.rect.height+30;
            }

        }

        contentScrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical , (panelLevel.rect.height * levels.Count / levelsPerRow) + 250);
        contentScrollView.position = new Vector3(0,0,0);
    }

    private void SetStars(RectTransform newPanelLevel)
    {
        Transform panelStars = newPanelLevel.transform.GetChild(0);

        int levelIndex = Convert.ToInt32(newPanelLevel.name);


        int c = 0;
        switch (levelDataTable[levelIndex - 1, 2])
        {
            case "0":
                c = 0;
                foreach (Transform star in panelStars)
                {
                    if (star.name.Contains("Full"))
                    {
                        if (c == 0) star.gameObject.SetActive(false);
                        if (c == 1) star.gameObject.SetActive(false);
                        if (c == 2) star.gameObject.SetActive(false);

                        c++;
                    }
                }


                break;


            case "1":
                c = 0;
                foreach (Transform star in panelStars)
                {
                    if (star.name.Contains("Full"))
                    {
                        if (c == 0) star.gameObject.SetActive(true);
                        if (c == 1) star.gameObject.SetActive(false);
                        if (c == 2) star.gameObject.SetActive(false);
                        c++;
                    }

                }


                break;
            case "2":
                c = 0;
                foreach (Transform star in panelStars)
                {
                    if (star.name.Contains("Full"))
                    {
                        if (c == 0) star.gameObject.SetActive(true);
                        if (c == 1) star.gameObject.SetActive(true);
                        if (c == 2) star.gameObject.SetActive(false);
                        c++;
                    }

                }
                break;

            case "3":
                c = 0;
                foreach (Transform star in panelStars)
                {
                    if (star.name.Contains("Full"))
                    {
                        if (c == 0) star.gameObject.SetActive(true);
                        if (c == 1) star.gameObject.SetActive(true);
                        if (c == 2) star.gameObject.SetActive(true);
                        c++;
                    }

                }

                break;

            default:
                break;

        }
    }

    public void Back()
    {
        SceneManager.LoadScene("WorldSelector");
    }
}
