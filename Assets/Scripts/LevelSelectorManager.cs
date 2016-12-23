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

    [SerializeField]
    Text textMaxStars;

    GameObject panelStars;

    private Vector3 levelScale;

    public static string world = "Air";
    int levelsPerRow = 3;
    int levelsShowed = 0;
    string[] levelsData;

    //private ScrolView


    // Use this for initialization
    void Start ()
    {
        // TEST ONLY //
        LevelsDataManager.SaveLevelData("Air", 1, 17000,3);
        LevelsDataManager.SaveLevelData("Air", 2, 17500, 2);
        LevelsDataManager.SaveLevelData("Air", 3, 18000, 1);
        LevelsDataManager.SaveLevelData("Earth", 1, 25000, 0);
        LevelsDataManager.SaveLevelData("Earth", 2, 60000, 3);
        /**********************************************************/


        // Show all stars owned
        int[] allStars = LevelsDataManager.GetAllStarsFromWorld(GameManager.world);
        textMaxStars.text = allStars[0] + "/" + allStars[1];


        //Debug.Log("World: " + GameManager.world);
        levelsData = LevelsDataManager.GetLevelsData(GameManager.world);
        PopulateLevelSelector(GameManager.world);
        
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
                //paneScore.GetComponent<Text>().text = levelDataTable[levelIndex - 1, 3];
                //Debug.Log("LevelData: " + levelsData[levelIndex - 1]);
                paneScore.GetComponent<Text>().text = levelsData[levelIndex - 1].Split('*')[1];


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
                //paneScore.GetComponent<Text>().text = levelDataTable[levelIndex - 1, 3];
                paneScore.GetComponent<Text>().text = levelsData[levelIndex - 1].Split('*')[1];


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

        //switch (levelDataTable[levelIndex - 1, 2])
        switch (levelsData[levelIndex - 1].Split('*')[0])
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
