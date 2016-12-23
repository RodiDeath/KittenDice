using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class WorldButton : MonoBehaviour
{
    [SerializeField]
    Text textAllStars;

    void Start()
    {
        // Show all stars owned
        int[] allStars = LevelsDataManager.GetAllStarsFromWorld(transform.name);
        textAllStars.text = allStars[0] + " / " + allStars[1];
    }

    public void LoadLevelSelector()
    {
        //Debug.Log("WorldSelected: " + transform.name);

        GameManager.world = transform.name;


        WorldSelectorManager.WorldSelected = transform.name;

        if (transform.name.Equals("Air") || transform.name.Equals("Earth")) // PARCHE CHUNGO TEST ONLY
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
