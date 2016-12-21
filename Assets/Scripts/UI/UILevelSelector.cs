using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class UILevelSelector : MonoBehaviour
{
    public void LoadLevel()
    {
        //GameManager.world = 1;
        GameManager.level = Convert.ToInt16(transform.name);

        SceneManager.LoadScene("Game");
    }
}
