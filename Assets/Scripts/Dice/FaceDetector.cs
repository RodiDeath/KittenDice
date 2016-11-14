using UnityEngine;
using System.Collections;
using System;

public class FaceDetector : MonoBehaviour {

    Dice dice;
	// Use this for initialization
	void Start () {

        dice = this.transform.parent.GetComponentInParent<Dice>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Map"))
        {
            //Debug.Log(this.gameObject.name);
            dice.SetUpperFace(Convert.ToInt32(this.gameObject.name));
        }
    }

    public void TurnDiceTo(int face) // Upperface Must be 5
    {
        dice = this.transform.parent.GetComponentInParent<Dice>();
        switch (face)
        {
            case 1:
                dice.transform.Rotate(Vector3.right , -90);
                dice.SetUpperFace(face);
                break;
            case 2:
                dice.transform.Rotate(Vector3.right, 180);
                dice.SetUpperFace(face);
                break;
            case 3:
                dice.transform.Rotate(Vector3.forward, -90);
                dice.SetUpperFace(face);
                break;
            case 4:
                dice.transform.Rotate(Vector3.forward, 90);
                dice.SetUpperFace(face);
                break;
            case 5:
                dice.SetUpperFace(face);
                break;
            case 6:
                dice.transform.Rotate(Vector3.right, 90);
                dice.SetUpperFace(face);
                break;

        }
    }

}
