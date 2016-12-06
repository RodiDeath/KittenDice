using UnityEngine;

public class UIPadManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerControler;

    private bool isPressed = false;

	// Use this for initialization
	void Start ()
    {
	    if (!Storage.GetControls().Equals(StorageKeys.Pad))
        {
            gameObject.SetActive(false);
        }
	}

    public void onPointerDownPad()
    {
        isPressed = true;
    }

    public void onPointerUpPad()
    {
        isPressed = false;
    }
}
