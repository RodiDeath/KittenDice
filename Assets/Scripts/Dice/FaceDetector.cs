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
            Debug.Log(this.gameObject.name);
            dice.SetUpperFace(Convert.ToInt32(this.gameObject.name));
        }
    }
}
