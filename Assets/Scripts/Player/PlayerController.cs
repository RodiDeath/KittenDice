using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 0.1f;
    private Vector3 direction = new Vector3(0, 0, 0);

    public Dice diceBehind;
    public Map map;

    public int coorX, coorY;
    private int destX, destY;

    private bool isMoving = false;
    private string moveDirection = "";


    // Use this for initialization
    void Start ()
    {
        // TEST ONLY
        diceBehind = FindObjectOfType<Dice>();
        map = FindObjectOfType<Map>();

        coorX = diceBehind.GetDiceCoorX();
        coorY = diceBehind.GetDiceCoorY();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isMoving)
        {
            direction = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                if (map.IsEmpty(coorX, coorY+1))
                {
                    direction += Vector3.forward;
                    moveDirection = "up";
                    isMoving = true;
                    diceBehind.MoveUp();
                }
            }
            else

            if (Input.GetKey(KeyCode.S))
            {
                if (map.IsEmpty(coorX, coorY - 1))
                {
                    direction -= Vector3.forward;
                    moveDirection = "down";
                    isMoving = true;
                    diceBehind.MoveDown();
                }
            }
            else

            if (Input.GetKey(KeyCode.A))
            {
                if (map.IsEmpty(coorX - 1, coorY))
                {
                    direction -= Vector3.right;
                    moveDirection = "left";
                    isMoving = true;
                    diceBehind.MoveLeft();
                }
            }
            else

            if (Input.GetKey(KeyCode.D))
            {
                if (map.IsEmpty(coorX + 1, coorY))
                {
                    direction += Vector3.right;
                    moveDirection = "right";
                    isMoving = true;
                    diceBehind.MoveRight();
                }
            }
        }


        if (isMoving)
        {
            if ((moveDirection.Equals("right") && transform.position.x <= coorX+1) ||
                (moveDirection.Equals("left") && transform.position.x >= coorX - 1) ||
                (moveDirection.Equals("up") && transform.position.z <= coorY + 1) ||
                (moveDirection.Equals("down") && transform.position.z >= coorY - 1)
                ) // During movement
            {
                if (direction != Vector3.zero)
                {
                    transform.position += direction.normalized * playerSpeed * Time.deltaTime;
                }
            }
            else // End of movement
            {
                isMoving = false;



                if (moveDirection.Equals("up"))
                {
                    coorY++;
                    transform.position = new Vector3(transform.position.x, transform.position.y, coorY);
                }

                if (moveDirection.Equals("down"))
                {
                    coorY--;
                    transform.position = new Vector3(transform.position.x, transform.position.y, coorY);
                }

                if (moveDirection.Equals("right"))
                {
                    coorX++;
                    transform.position = new Vector3(coorX, transform.position.y, transform.position.z);
                }

                if (moveDirection.Equals("left"))
                {
                    coorX--;
                    transform.position = new Vector3(coorX, transform.position.y, transform.position.z);
                }

                
            }
        }
    }
}
