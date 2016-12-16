using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
}
