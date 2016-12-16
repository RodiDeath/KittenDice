using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class WorldSelectorManager : MonoBehaviour
{
    public static string WorldSelected;

    [SerializeField]
    private RectTransform content;

    private float contentPosX;
    private int contentPosIndex;

    private RectTransform[] worldButtons;
    private float VelocityX;


    private bool movingToTarget = false;
    private int target = 0;
    private int scrollSpeed = 100;

    void Start()
    {
        int i = 0;

        worldButtons = new RectTransform[content.transform.childCount];
        foreach (Transform wb in content.transform)
        {
            worldButtons[i] = wb.GetComponent<RectTransform>();
            i++;
        }

        //content.transform.parent.transform.parent.GetComponent<ScrollRect>().velocity = new Vector2(-2000,0);

        //movingToTarget = true;
        //target = 3;
    }

    public void StopAutoMovement()
    {
        movingToTarget = false;
    }

    private void MoveContentTo(int pos , int spd)
    {
        movingToTarget = true;
        target = pos;
        scrollSpeed = spd;
    }

    void FixedUpdate()
    {
        if (movingToTarget)
        {
            VelocityX = content.transform.parent.transform.parent.GetComponent<ScrollRect>().velocity.x;
            float targetRealX = target * -300;

            if (targetRealX - contentPosX > 0) // Destino a la izquierda
            {
                content.transform.parent.transform.parent.GetComponent<ScrollRect>().velocity = new Vector2(scrollSpeed, 0);
            }
            else // Destino a la derecha
            {
                content.transform.parent.transform.parent.GetComponent<ScrollRect>().velocity = new Vector2(-scrollSpeed, 0);
            }

            if (Math.Abs(targetRealX - contentPosX) < 10)
            {
                movingToTarget = false;
                content.transform.parent.transform.parent.GetComponent<ScrollRect>().velocity = new Vector2(0, 0);
                scrollSpeed = 100;
            }
        }
    }

    public void ResizeWorldButtons()
    {
        VelocityX = content.transform.parent.transform.parent.GetComponent<ScrollRect>().velocity.x;


        contentPosX = content.GetComponent<RectTransform>().offsetMin.x;

        contentPosIndex = Convert.ToInt32(contentPosX / worldButtons[0].rect.width);
        if (contentPosIndex < ((content.transform.childCount - 1) * -1))
        {
            contentPosIndex = content.transform.childCount - 1;
        }
        else if (contentPosIndex > 0)
        {
            contentPosIndex = 0;
        }

        contentPosIndex = Math.Abs(contentPosIndex);

        for (int i = 0; i < worldButtons.Length; i++)
        {
            if (i == contentPosIndex)
            {
                worldButtons[i].localScale = new Vector3(1, 1, 1);
            }
            else
            {
                worldButtons[i].localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }

        if (Math.Abs(VelocityX) < 50)
        {
            MoveContentTo(contentPosIndex, 100);
        }
    }

    public static void LoadLevelSelector(int worldSelected)
    {
        //SceneManager.LoadScene("LevelSelector");
        Debug.Log("World Selected: " + worldSelected);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
