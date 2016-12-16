using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WorldSelectorManager : MonoBehaviour
{

    public void LoadLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
