using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    public GameObject pivot;
    public GameObject player;

    private float degreesPerSecond = 90;

    private bool isMoving = false;
    public string moveDirection = "";
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
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move("up");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move("down");
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move("left");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move("right");
            }
        }



        //transform.Rotate(Vector3.up * degreesPerSecond * Time.deltaTime);



        //player.transform.RotateAround(pivot.transform.position, Vector3.forward * speed * Time.deltaTime, -1);




        //player.transform.Rotate(Vector3.forward * speed * Time.deltaTime, -1);
    }

    public void Move(string direction) // Direction: up, down, left, right
    {
        isMoving = true;

        if (direction.Equals("up"))
        {
            pivot.transform.position = new Vector3(transform.position.x, 0, transform.position.z + 0.5f);

            player.transform.RotateAround(pivot.transform.position, Vector3.right, 90);
            isMoving = false;
        }

        if (direction.Equals("down"))
        {
            pivot.transform.position = new Vector3(transform.position.x, 0, transform.position.z - 0.5f);

            player.transform.RotateAround(pivot.transform.position, Vector3.right, -90);
            isMoving = false;
        }

        if (direction.Equals("right"))
        {
            pivot.transform.position = new Vector3(transform.position.x + 0.5f , 0, transform.position.z);

            player.transform.RotateAround(pivot.transform.position, Vector3.forward, -90);
            isMoving = false;
        }

        if (direction.Equals("left"))
        {
            pivot.transform.position = new Vector3(transform.position.x - 0.5f, 0, transform.position.z);

            player.transform.RotateAround(pivot.transform.position, Vector3.forward, 90);
            isMoving = false;
        }
    }
}
