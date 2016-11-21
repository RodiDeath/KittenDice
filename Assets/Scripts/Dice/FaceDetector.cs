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
            if (dice != null)
            {
                dice.SetUpperFace(Convert.ToInt32(this.gameObject.name));
            }
        }
    }

    public void TurnDiceTo(int face, int frontFace) // Upperface Must be 5 and frontFace must be 6 on the prefab
    {
        dice = this.transform.parent.GetComponentInParent<Dice>();
        switch (face)
        {
            case 1:
                //dice.transform.Rotate(Vector3.right , -90);
                dice.SetUpperFace(face);

                switch(frontFace)
                {
                    case 2:
                        dice.transform.eulerAngles = new Vector3(-90, 180, 0);
                        break;
                    case 3:
                        dice.transform.eulerAngles = new Vector3(-90, -90, 0);
                        break;
                    case 4:
                        dice.transform.eulerAngles = new Vector3(-90, 90, 0);
                        break;
                    case 5:
                        dice.transform.eulerAngles = new Vector3(-90, 0, 0);
                        break;
                    default:
                        dice.transform.eulerAngles = new Vector3(-90, 180, 0);
                        break;

                }
                break;

            case 2:
                //dice.transform.Rotate(Vector3.right, 180);
                dice.SetUpperFace(face);

                switch (frontFace)
                {
                    case 1:
                        dice.transform.eulerAngles = new Vector3(180, 0, 0);
                        break;
                    case 3:
                        dice.transform.eulerAngles = new Vector3(180, -90, 0);
                        break;
                    case 4:
                        dice.transform.eulerAngles = new Vector3(180, 90, 0);
                        break;
                    case 6:
                        dice.transform.eulerAngles = new Vector3(180, 180, 0);
                        break;

                    default:
                        dice.transform.eulerAngles = new Vector3(180, 0, 0);
                        break;
                }
                break;

            case 3:
                //dice.transform.Rotate(Vector3.forward, -90);
                dice.SetUpperFace(face);

                switch (frontFace)
                {
                    case 1:
                        //dice.transform.Rotate(Vector3.up, 180);
                        dice.transform.eulerAngles = new Vector3(0, 180, -90);
                        break;
                    case 2:
                       // dice.transform.Rotate(Vector3.up, -90);
                        dice.transform.eulerAngles = new Vector3(0, -90, -90);
                        break;
                    case 5:
                        //dice.transform.Rotate(Vector3.up, 90);
                        dice.transform.eulerAngles = new Vector3(0, 90, -90);
                        break;
                    case 6:
                        dice.transform.eulerAngles = new Vector3(0, 0, -90);
                        break;
                    default:
                        dice.transform.eulerAngles = new Vector3(0, 180, -90);
                        break;
                }
                break;

            case 4:
                dice.SetUpperFace(face);

                switch (frontFace)
                {
                    case 1:
                        //dice.transform.Rotate(Vector3.up, 180);
                        dice.transform.eulerAngles = new Vector3(0, 180, 90);
                        break;
                    case 2:
                        //dice.transform.Rotate(Vector3.up, 90);
                        dice.transform.eulerAngles = new Vector3(0, 90, 90);
                        break;
                    case 5:
                        dice.transform.eulerAngles = new Vector3(0, -90, 90);
                        break;
                    case 6:
                        dice.transform.eulerAngles = new Vector3(0, 0, 90);
                        break;
                    default:
                        dice.transform.eulerAngles = new Vector3(0, 180, 90);
                        break;
                }
                break;

            case 5:
                dice.SetUpperFace(face);

                switch (frontFace)
                {
                    case 1:
                        //dice.transform.Rotate(Vector3.up, 180);
                        dice.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                    case 3:
                        //dice.transform.Rotate(Vector3.up, -90);
                        dice.transform.eulerAngles = new Vector3(0, -90, 0);
                        break;
                    case 4:
                        //dice.transform.Rotate(Vector3.up, 90);
                        dice.transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 6:
                        dice.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    default:
                        dice.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                }
                break;

            case 6:
                //dice.transform.Rotate(Vector3.right, 90);
                dice.SetUpperFace(face);

                switch (frontFace)
                {
                    case 2:
                        dice.transform.eulerAngles = new Vector3(90, 0, 0);
                        break;
                    case 3:
                        //dice.transform.Rotate(Vector3.up, -90);
                        dice.transform.eulerAngles = new Vector3(90, -90, 0);
                        break;
                    case 4:
                        //dice.transform.Rotate(Vector3.up, 90);
                        dice.transform.eulerAngles = new Vector3(90, 90, 0);
                        break;
                    case 5:
                        //dice.transform.Rotate(Vector3.up, 180);
                        dice.transform.eulerAngles = new Vector3(90, 180, 0);
                        break;
                    default:
                        dice.transform.eulerAngles = new Vector3(90, 0, 0);
                        break;
                }
                break;

        }
    }

}
