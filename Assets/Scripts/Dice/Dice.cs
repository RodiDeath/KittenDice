using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    //ATRIBUTOS
    //COORDENADAS
    public int diceCoorX;
    public int diceCoorY;
    public int upperFace;
    
    public GameObject pivot;
    public GameObject player;
    public GameObject auxGameObject;
    public Map map; // Reference to the map (board)

    public float degresAtATime = 5; // Speed of rotation, 90 % this must be 0
    private float countDegrees = 0;
    private bool isMoving = false;

    //DIRECCION
    public string moveDirection = "";
    public float speed = 100f;
    public Vector3 previusPosition;


    void Awake()
    {
        diceCoorX = 0;
        diceCoorY = 0;
    }

    // Use this for initialization
    void Start ()
    {
        player = this.gameObject;

        map = FindObjectOfType<Map>(); // Reference to the map (board)
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isMoving)
        {
            /************************************/
            // TEST ONLY
            if (Input.GetKey(KeyCode.Space))
            {
                map.PrintBoard();
            }
            /***********************************/



            //if (Input.GetKey(KeyCode.UpArrow))
            //{
            //    MoveUp();
            //}

            //if (Input.GetKey(KeyCode.DownArrow))
            //{
            //    MoveDown();
            //}

            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    MoveLeft();
            //}
            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    MoveRight();
            //}
        }

        if (isMoving)
        {
            if (countDegrees < (90 / degresAtATime)) // During movement
            {
                countDegrees += 1;
                Move(moveDirection);
            }
            else // End of movement
            {
                countDegrees = 0;
                isMoving = false;

                


                if (moveDirection.Equals("up"))
                {
                    //auxGameObject.transform.RotateAround(pivot.transform.position, Vector3.right, 90);
                    diceCoorY++;

                }

                if (moveDirection.Equals("down"))
                {
                    //auxGameObject.transform.RotateAround(pivot.transform.position, Vector3.right, -90);
                    diceCoorY--;
                }

                if (moveDirection.Equals("right"))
                {
                    //auxGameObject.transform.RotateAround(pivot.transform.position, Vector3.forward, -90);
                    diceCoorX++;
                }

                if (moveDirection.Equals("left"))
                {
                    //auxGameObject.transform.RotateAround(pivot.transform.position, Vector3.forward, 90);
                    diceCoorX--;
                }
                
                this.transform.position = new Vector3(diceCoorX,this.transform.position.y,diceCoorY);
                map.AddDice(this);
                //map.LoveMeLikeYouDo();
            }
        }

    }

    #region MOVEMENT

    public void MoveRight()
    {
        if (!isMoving)
        {
            // Coordinates of the destination cell
            int xDest = this.diceCoorX + 1;
            int yDest = this.diceCoorY + 0;

            if (map.IsEmpty(xDest, yDest)) // If the destination cell is empty...
            {
                map.RemoveDice(this);

                isMoving = true;
                moveDirection = "right";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x + 0.5f, 0, transform.position.z);
            }
        }
    }

    public void MoveLeft()
    {
        if (!isMoving)
        {
            // Coordinates of the destination cell
            int xDest = this.diceCoorX - 1;
            int yDest = this.diceCoorY + 0;

            if (map.IsEmpty(xDest, yDest)) // If the destination cell is empty...
            {
                map.RemoveDice(this);

                isMoving = true;
                moveDirection = "left";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x - 0.5f, 0, transform.position.z);
            }
        }
    }

    public void MoveDown()
    {
        if (!isMoving)
        {
            // Coordinates of the destination cell
            int xDest = this.diceCoorX + 0;
            int yDest = this.diceCoorY - 1;

            if (map.IsEmpty(xDest, yDest)) // If the destination cell is empty...
            {
                map.RemoveDice(this);

                isMoving = true;
                moveDirection = "down";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x, 0, transform.position.z - 0.5f);
            }
        }
    }

    public void MoveUp()
    {
        if (!isMoving)
        {
            // Coordinates of the destination cell
            int xDest = this.diceCoorX + 0;
            int yDest = this.diceCoorY + 1;

            if (map.IsEmpty(xDest, yDest)) // If the destination cell is empty...
            {
                map.RemoveDice(this);

                isMoving = true;
                moveDirection = "up";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x, 0, transform.position.z + 0.5f);
            }
        }
    }

    private void Move(string direction) // Direction: up, down, left, right
    {
        if (direction.Equals("up"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.right * speed * Time.deltaTime, degresAtATime);
        }

        if (direction.Equals("down"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.right * speed * Time.deltaTime, -degresAtATime);
        }

        if (direction.Equals("right"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.forward * speed * Time.deltaTime, -degresAtATime);
        }

        if (direction.Equals("left"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.forward * speed * Time.deltaTime, degresAtATime);
        }
    }

    #endregion


    public bool GetIsMoving() { return isMoving; }

    public void SetDiceCoorX(int coorX)
    {
        this.diceCoorX = coorX;       
    }
    public int GetDiceCoorX()
    {
        return diceCoorX;
    }
    public void SetDiceCoorY(int coorY)
    {
        this.diceCoorY = coorY;
    }
    public int GetDiceCoorY()
    {
        return diceCoorY;
    }
    public void SetUpperFace(int uf)
    {
        this.upperFace = uf;
    }
    public int GetUpperFace()
    {
        return upperFace;
    }
}
