using UnityEngine;
using System.Collections;

public class DiceCameraController : MonoBehaviour {
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Dice focusedDice;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerController.GetDiceBehind() != null)
        {
            focusedDice.transform.rotation = playerController.GetDiceBehind().transform.rotation;
        }

    }
}
