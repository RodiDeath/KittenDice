using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    Text textScore;
    

    public static int score = 0;
    public static int stars = 0;

	// Use this for initialization
	void Start ()
    {
        stars = Convert.ToInt32(LevelsDataManager.GetLevelData(GameManager.world, GameManager.level)[0]);
        score = Convert.ToInt32(LevelsDataManager.GetLevelData(GameManager.world, GameManager.level)[1]);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //textScore.text = score.ToString();
        //UpdateStarsPanel();
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
        switch (stars)
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
