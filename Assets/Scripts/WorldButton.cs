using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class WorldButton : MonoBehaviour
{
    public void LoadLevelSelector()
    {
        Debug.Log("WorldSelected: " + transform.name);

        WorldSelectorManager.WorldSelected = transform.name;

        if (transform.name.Equals("Air"))
        {
            SceneManager.LoadScene("LevelSelector");
        }
    }

    void FixedUpdate()
    {
        float differencePosition = Math.Abs( transform.position.x - Screen.width / 2);
        float maxDifference = Screen.width / 4;
        float maxScale = 1f;
        float minScale = 0.8f;


        float magicNumber = (-(maxScale-minScale) / maxDifference * differencePosition) + maxScale; // Formula Maxica

        if (differencePosition <= maxDifference)
        {
            transform.localScale = new Vector3(magicNumber, magicNumber, magicNumber);
        }


        //if (Math.Abs( differencePosition) < Screen.width/10)
        //{
        //    transform.localScale = new Vector3(1f,1f, 1f);
        //}
        //else
        //{
        //    transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //}

    }
}
