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

    public float degresAtATime = 5; // Speed of rotation, 90 % this must be 0
    private float countDegrees = 0;
    private bool isMoving = false;

    //DIRECCION
    public string moveDirection = "";
    public float speed = 100f;
    public Vector3 previusPosition;

    // Use this for initialization
    void Start ()
    {
        player = this.gameObject;
        diceCoorX = 0;
        diceCoorY = 0;

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
                isMoving = true;
                moveDirection = "up";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x, 0, transform.position.z + 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isMoving = true;
                moveDirection = "down";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x, 0, transform.position.z - 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isMoving = true;
                moveDirection = "left";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x - 0.5f, 0, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isMoving = true;
                moveDirection = "right";

                //auxGameObject.transform.position = player.transform.position;
                //auxGameObject.transform.rotation = player.transform.rotation;

                pivot.transform.position = new Vector3(transform.position.x + 0.5f, 0, transform.position.z);
            }
        }

        if (isMoving)
        {
            if (countDegrees < (90 / degresAtATime))
            {
                countDegrees += 1;
                Move(moveDirection);
            }
            else
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
            }
        }

    }

    public void StartMoving()
    {

    }

    public void Move(string direction) // Direction: up, down, left, right
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
