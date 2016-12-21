using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class UILevelSelector : MonoBehaviour
{
    [SerializeField]
    private LifesManager lifeManager;

    public void LoadLevel()
    {
        if (LifesManager.lifes >= 1)
        {
            //GameManager.world = 1;
            GameManager.level = Convert.ToInt16(transform.name);

            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.Log("No creeeo ehhh");
        }
    }
}
