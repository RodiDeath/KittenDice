using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	public void PlayPuzzle()
    {
        SceneManager.LoadScene("Game");
    }

}
