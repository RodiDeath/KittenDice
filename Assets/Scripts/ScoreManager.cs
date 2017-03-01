using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    static Text textActualScore;

    [SerializeField]
    Text textActualScoreAux;

    [SerializeField]
    Text textRecordScore;

    static Dictionary<int, int> potencialScoreList;
    static int potencialScoreId = 0;

    public static int scoreRecord = 0;
    public static int starsRecord = 0;

    public static int score = 0;
    public static int stars = 0;

    private static int numberOfDice = 0;
    private int maxScore = 0;

    // Use this for initialization
    void Start ()
    {
        textActualScore = textActualScoreAux;

        potencialScoreList = new Dictionary<int, int>();
        potencialScoreId = 0;

        starsRecord = Convert.ToInt32(LevelsDataManager.GetLevelData(GameManager.world, GameManager.level)[0]);
        scoreRecord = Convert.ToInt32(LevelsDataManager.GetLevelData(GameManager.world, GameManager.level)[1]);

        textRecordScore.text = scoreRecord.ToString();
        textActualScore.text = "0";

        numberOfDice = FindObjectsOfType<Dice>().Length;
        numberOfDice--;
        maxScore = numberOfDice * 6 * 6;
    }

    public static bool IsNewRecord()
    {
        if (score > scoreRecord) return true;
        else return false;
    }

    public static void ResetActualScore()
    {
        score = 0;
        textActualScore.text = "0";
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //textScore.text = score.ToString();
        //UpdateStarsPanel();
	}

    public static void AddScore(int potId)
    {
        if (potencialScoreList.ContainsKey(potId))
        {
            score += potencialScoreList[potId];
            textActualScore.text = score.ToString();

            potencialScoreList.Remove(potId);


            if (score >= numberOfDice * 5 * 5 * 100)
            {
                stars = 3;
            }
            else if (score >= numberOfDice * 3 * 3 * 100)
            {
                stars = 2;
            }else
            {
                stars = 1;
            }

            if (stars > starsRecord) starsRecord = stars;

            Debug.Log("Stars: " + stars);
            Debug.Log("StarsRecord: " + starsRecord);
        }
    }

    public static int AddPotencialScore(int potencialScore)
    {
        potencialScoreId++;
        potencialScoreList.Add(potencialScoreId, potencialScore);

        return potencialScoreId;
    }

    public static void UpdateDataLevelInPanel(Transform panelStars, Text textScore)
    {
        UpdateStarsPanel(panelStars);
        UpdateTextScore(textScore);
    }

    private static void UpdateTextScore(Text txtScore)
    {
        txtScore.text = score.ToString();
    }

    private static void UpdateStarsPanel(Transform panelStars)
    {
        int c = 0;
        Debug.Log("Funcion para: " + panelStars.name);

        //switch (levelDataTable[levelIndex - 1, 2])
        switch (starsRecord)
        {
            case 0:
                c = 0;
                foreach (Transform star in panelStars)
                {
                    Debug.Log(star.name);
                    if (star.name.Contains("Full"))
                    {
                        if (c == 0) star.gameObject.SetActive(false);
                        if (c == 1) star.gameObject.SetActive(false);
                        if (c == 2) star.gameObject.SetActive(false);

                        c++;
                    }
                }


                break;


            case 1:
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
            case 2:
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

            case 3:
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
}
