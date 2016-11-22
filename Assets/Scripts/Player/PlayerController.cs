using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public LevelManager levelManager;
    public float playerSpeed = 0.1f;
    private Vector3 direction = new Vector3(0, 0, 0);

    public Dice diceBehind;
    public Map map;

    public int coorX, coorY;
    private int destX, destY;

    private bool isMoving = false;
    private string moveDirection = "";

    public bool isDead = false;
    public bool hasWinned = false;
    public bool willWin = false;


    // Use this for initialization
    void Start ()
    {
        
        // TEST ONLY
        map = FindObjectOfType<Map>();
        

        diceBehind = map.GetDice(levelManager.GetStartPositionX(), levelManager.GetStartPositionY());

        coorX = diceBehind.GetDiceCoorX();
        coorY = diceBehind.GetDiceCoorY();
        transform.position = new Vector3(coorX, transform.position.y, coorY);
    }

    //public void InitializePlayer()
    //{
    //    map = FindObjectOfType<Map>();
    //    diceBehind = map.GetDice(levelManager.GetStartPositionX(), levelManager.GetStartPositionY());

    //    coorX = diceBehind.GetDiceCoorX();
    //    coorY = diceBehind.GetDiceCoorY();
    //    transform.position = new Vector3(coorX, transform.position.y, coorY);
    //}
	
	// Update is called once per frame
	void Update ()
    {
        CheckWinLose();

        if (!isDead && !hasWinned && !willWin)
        {

            if (!isMoving && !diceBehind.GetIsMoving())
            {
                direction = new Vector3(0, 0, 0);
                if (Input.GetKey(KeyCode.W))
                {
                    if (!map.IsOutOfBounds(coorX, coorY + 1))
                    {
                        direction += Vector3.forward;
                        moveDirection = "up";
                        isMoving = true;

                        if (map.IsEmpty(coorX, coorY + 1))
                        {
                            diceBehind.MoveUp();
                        }
                    }

                }
                else

                if (Input.GetKey(KeyCode.S))
                {
                    if (!map.IsOutOfBounds(coorX, coorY - 1))
                    {
                        direction -= Vector3.forward;
                        moveDirection = "down";
                        isMoving = true;

                        if (map.IsEmpty(coorX, coorY - 1))
                        {

                            diceBehind.MoveDown();
                        }
                    }
                }
                else

                if (Input.GetKey(KeyCode.A))
                {
                    if (!map.IsOutOfBounds(coorX - 1, coorY))
                    {
                        direction -= Vector3.right;
                        moveDirection = "left";
                        isMoving = true;

                        if (map.IsEmpty(coorX - 1, coorY))
                        {
                            diceBehind.MoveLeft();
                        }
                    }
                }
                else

                if (Input.GetKey(KeyCode.D))
                {
                    if (!map.IsOutOfBounds(coorX + 1, coorY))
                    {
                        direction += Vector3.right;
                        moveDirection = "right";
                        isMoving = true;

                        if (map.IsEmpty(coorX + 1, coorY))
                        {
                            diceBehind.MoveRight();
                        }
                    }
                }
            }
        }


        if (isMoving)
        {
            if ((moveDirection.Equals("right") && transform.position.x <= coorX + 1) ||
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
            else // End of movement of player
            {




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

                diceBehind = map.GetDice(coorX, coorY);

                isMoving = false;


            }
        }
        
    }

    public void CheckWinLose()
    {
        if (map.AllDicesActivated())
        {
            if (!willWin) Debug.Log("Will win...");
            willWin = true;
            levelManager.PlayerWillWin();
        }



        if (diceBehind == null && !isDead)
        {
            if (willWin)
            {
                if (map.GetDiceCount() == 0 && !hasWinned)
                {
                    hasWinned = true;
                    levelManager.PlayerWins();
                }
            }
            else
            if (diceBehind == null)
            {

                isDead = true;
                GetComponent<Renderer>().material.color = Color.red;

                levelManager.PlayerDied();
            }
        }
    }

    public void MoveTo(int x, int y)
    {
        coorX = x;
        coorY = y;

        transform.position = new Vector3(coorX, transform.position.y, coorY);
        diceBehind = map.GetDice(x,y);
    }

    public void SetIsDead(bool isd) { isDead = isd; }
    public bool GetIsDead() { return isDead; }
    public bool GetWillWin() { return willWin; }
    public void SetWillWin(bool ww) { willWin = ww; }
    public void SetHasWinned(bool hw) { hasWinned = hw; }
}
