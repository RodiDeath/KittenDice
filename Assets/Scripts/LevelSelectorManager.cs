using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField]
    RectTransform contentScrollView;

    [SerializeField]
    RectTransform panelLevel;
    //private ScrolView

    // Use this for initialization
    void Start ()
    {
        PopulateLevelSelector(1);
    }
	
    void PopulateLevelSelector(int world)
    {
        bool modelPanelDeleted = false;

        List<string> levels = LevelManager.GetAllLevelStrings(world);

        // UI TEST ONLY /////////////////
        for (int i = 0; i < 20; i++)
        {
            int levelN = i+3;
            levels.Add("Level" + levelN);
        }
        ////////////////////////////////

        foreach (var level in levels)
        {
            if (!modelPanelDeleted)
            {
                panelLevel = contentScrollView.GetChild(contentScrollView.childCount - 1).GetComponent<RectTransform>();
                

                RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(panelLevel.position.x, panelLevel.position.y, 1f), Quaternion.identity) as RectTransform;
                newPanelLevel.name = level;
                newPanelLevel.GetComponentInChildren<Text>().text = level;

                newPanelLevel.SetParent(contentScrollView);
                Destroy(contentScrollView.GetChild(0).gameObject);
                modelPanelDeleted = true;
            }
            else
            {
                panelLevel = contentScrollView.GetChild(contentScrollView.childCount - 1).GetComponent<RectTransform>();

                RectTransform newPanelLevel = Instantiate(panelLevel, new Vector3(panelLevel.position.x, panelLevel.position.y - panelLevel.rect.height, 1f), Quaternion.identity) as RectTransform;
                newPanelLevel.name = level;
                newPanelLevel.GetComponentInChildren<Text>().text = level;

                newPanelLevel.SetParent(contentScrollView);
            }

        }

        contentScrollView.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical , (panelLevel.rect.height * levels.Count) + 10);
    }
}
