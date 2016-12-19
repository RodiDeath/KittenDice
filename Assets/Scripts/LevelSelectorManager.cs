using UnityEngine;
using System.Collections;
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

    private Vector3 levelScale;

    public static int world = 1;
    int levelsPerRow = 3;
    int levelsShowed = 0;
    
    //private ScrolView


    // Use this for initialization
    void Start ()
    {
        PopulateLevelSelector(world);
    }
	
    void PopulateLevelSelector(int world)
    {
        bool modelPanelDeleted = false;
        float startPositionX = -280f;
        float startPositionY = -150f;
        float initialX = -280f;
        float initialY = -150f;

        List<string> levels = LevelManager.GetAllLevelStrings(world);

        // UI TEST ONLY /////////////////
        for (int i = 0; i < 21; i++)
        {
            int levelN = i + 4;
            levels.Add("Level" + levelN);
        }
        ////////////////////////////////



        foreach (var level in levels)
        {
            if (!modelPanelDeleted)
            {
                panelLevel = contentScrollView.GetChild(contentScrollView.childCount - 1).GetComponent<RectTransform>();
                contentScrollView.GetComponent<RectTransform>().position = new Vector3(0,0,0);

                

                //RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(panelLevel.position.x, panelLevel.position.y, 1f), Quaternion.identity) as RectTransform;

                //startPositionX = newPanelLevel.position.x;
                //startPositionY = newPanelLevel.position.y;

                initialX = -280f;
                initialY = -150f;

                startPositionX = initialX;
                startPositionY = initialY;

                //RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(panelLevel.position.x, panelLevel.position.y, 1f), Quaternion.identity) as RectTransform;
                RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(startPositionX, startPositionY, 1f), Quaternion.identity) as RectTransform;

                Debug.Log("StartX: " + startPositionX);
                //RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(startPositionX, startPositionY, 1f), Quaternion.identity) as RectTransform;

               // newPanelLevel.localPosition = new Vector3(startPositionX, startPositionY);

                newPanelLevel.name = Regex.Match(level, @"\d+").Value;
                newPanelLevel.GetComponentInChildren<Text>().text = newPanelLevel.name;
                //newPanelLevel.localScale = new Vector3(1,1,1);
                //levelScale = newPanelLevel.localScale;

                newPanelLevel.SetParent(contentScrollView, false);
                Destroy(contentScrollView.GetChild(0).gameObject);
                modelPanelDeleted = true;

                startPositionX += newPanelLevel.rect.width + 30;


                levelsShowed++;
            }
            else
            {
                panelLevel = contentScrollView.GetChild(contentScrollView.childCount - 1).GetComponent<RectTransform>();

                //RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(panelLevel.position.x, panelLevel.position.y - panelLevel.rect.height, 1f), Quaternion.identity) as RectTransform;
                RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(startPositionX, startPositionY, 1f), Quaternion.identity) as RectTransform;

                newPanelLevel.localPosition = new Vector3(startPositionX, startPositionY);

                newPanelLevel.name = Regex.Match(level, @"\d+").Value;
                newPanelLevel.GetComponentInChildren<Text>().text = newPanelLevel.name;
                //newPanelLevel.localScale = levelScale;

                newPanelLevel.SetParent(contentScrollView, false);


                Debug.Log("StartX: " + startPositionX);
                startPositionX += newPanelLevel.rect.width+30;
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

    public void Back()
    {
        SceneManager.LoadScene("WorldSelector");
    }
}
