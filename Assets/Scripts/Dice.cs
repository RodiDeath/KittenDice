using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    public GameObject pivot;
    public GameObject player;

    private bool isMoving = false;
    private float angleCount = 0;
    private float previousAngle = 0;
    public float speed = 0.01f;

    // Use this for initialization
    void Start ()
    {
        player = this.gameObject;


        Debug.Log(player.name);
        Debug.Log(pivot.name);

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        player.transform.RotateAround(pivot.transform.position, Vector3.forward * speed * Time.deltaTime, -1);

        //player.transform.Rotate(Vector3.forward * speed * Time.deltaTime, -1);
    }

    public void Move()
    {
        previousAngle = this.transform.rotation.eulerAngles.z;
        isMoving = true;
    }
}
