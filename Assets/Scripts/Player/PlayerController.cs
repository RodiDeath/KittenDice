﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private float playerSpeed = 0.1f;
    private Vector3 direction = new Vector3(0, 0, 0);

    [SerializeField]
    private Dice diceBehind;

    [SerializeField]
    private Map map;

    [SerializeField]
    private int coorX, coorY;
    private int destX, destY;

    [SerializeField]
    GameObject panelChangeDiceValue;
    [SerializeField]
    Button[] buttonsDiceFace;

    private bool cantMove = false;
    private bool isMoving = false;
    private string moveDirection = "";

    [SerializeField]
    private bool isDead = false;
    [SerializeField]
    private bool hasWinned = false;
    [SerializeField]
    private bool willWin = false;

    //Android
    private static bool usesPad = false;
    private bool padPulsed = false;
    private string mDir = "";

    [SerializeField]
    private float minSwipeDistY;
    [SerializeField]
    private float minSwipeDistX;
    private Vector2 startPos;

    // Test Flag
    //public bool buttonPressed = false;


    public void RandomiceDiceBehind() // Magia 
    {
        if (!isMoving && !diceBehind.GetIsMoving())
        {
            int newUpperFace = diceBehind.GetUpperFace();
            int newFrontFace = diceBehind.GetFrontFace();

            while (newUpperFace == diceBehind.GetUpperFace())
            {
                newUpperFace = Random.Range(1, 7);
            }



            while ((newFrontFace == diceBehind.GetFrontFace()) || (7 - newFrontFace == newUpperFace) || (newFrontFace == newUpperFace))
            {
                newFrontFace = Random.Range(1, 7);
            }


            diceBehind.GetComponentInChildren<FaceDetector>().TurnDiceTo(newUpperFace, newFrontFace);

            map.DetectEquals(diceBehind);
        }
    }

    public void ShowPanelChangeDiceValue() // Magia
    {
        if (!isMoving && !diceBehind.GetIsMoving())
        {
            cantMove = true;
            foreach (var face in buttonsDiceFace)
            {
                face.interactable = true;
            }
            buttonsDiceFace[diceBehind.GetUpperFace()-1].interactable = false;
            panelChangeDiceValue.SetActive(true);
        }
    }

    public void ClosePanelChangeDiceValue() // Magia
    {
        cantMove = false;
       
        panelChangeDiceValue.SetActive(false);
    }

    public void TransformDiceBehind(int value) // Magia 
    {
        if (!isMoving && !diceBehind.GetIsMoving())
        {
            int newUpperFace = value;
            int newFrontFace = diceBehind.GetFrontFace();

            while ((newFrontFace == diceBehind.GetFrontFace()) || (7 - newFrontFace == newUpperFace) || (newFrontFace == newUpperFace))
            {
                newFrontFace = Random.Range(1, 7);
            }

            diceBehind.GetComponentInChildren<FaceDetector>().TurnDiceTo(newUpperFace, newFrontFace);

            map.DetectEquals(diceBehind);

            panelChangeDiceValue.SetActive(false);
            cantMove = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (Storage.GetControls().Equals(StorageKeys.Pad)) usesPad = true;
        else usesPad = false;

        panelChangeDiceValue.SetActive(false);

        // TEST ONLY
        map = FindObjectOfType<Map>();
        

        diceBehind = map.GetDice(levelManager.GetStartPositionX(), levelManager.GetStartPositionY());

        coorX = diceBehind.GetDiceCoorX();
        coorY = diceBehind.GetDiceCoorY();
        transform.position = new Vector3(coorX, transform.position.y, coorY);
    }

    void CheckAndroidInput()
    {
        //CheckWinLose();

        if (!isDead && !hasWinned && !willWin)
        {
            if (!isMoving && !diceBehind.GetIsMoving())
            {

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.touches[0];

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:

                            startPos = touch.position;

                            break;

                        case TouchPhase.Ended:

                            float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                            if (swipeDistVertical > minSwipeDistY)
                            {
                                float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                                if (swipeValue > 0)
                                {
                                    MoveUp();
                                }
                                else if (swipeValue < 0)
                                {
                                    MoveDown();
                                }
                            }
                            float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                            if (swipeDistHorizontal > minSwipeDistX)
                            {
                                float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                                if (swipeValue > 0)
                                {
                                    MoveRight();
                                }
                                else if (swipeValue < 0)
                                {
                                    MoveLeft();
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CheckWinLose();
        if (!usesPad) CheckAndroidInput();

        //if (!buttonPressed)
        {
            if (!isDead && !hasWinned && !willWin && !cantMove)
            {
                if (!isMoving /*&& !diceBehind.GetIsMoving()*/)
                {
                    if (!diceBehind.GetIsMoving())
                    {

                        direction = new Vector3(0, 0, 0);
                        if (Input.GetKey(KeyCode.W))
                        {
                            MoveUp();
                        }
                        else

                        if (Input.GetKey(KeyCode.S))
                        {
                            MoveDown();
                        }
                        else

                        if (Input.GetKey(KeyCode.A))
                        {
                            MoveLeft();
                        }
                        else

                        if (Input.GetKey(KeyCode.D))
                        {
                            MoveRight();
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

#if !UNITY_EDITOR
                if (padPulsed)
                {
                    if (mDir.Equals("up")) MoveUp();
                    if (mDir.Equals("down")) MoveDown();
                    if (mDir.Equals("right")) MoveRight();
                    if (mDir.Equals("left")) MoveLeft();
                }
#endif

            }
        }
        
    }

    public void PulsedPad() { padPulsed = true; }
    public void ReleasedPad() { padPulsed = false; }

    public void MoveRight()
    {
        mDir = "right";
        PulsedPad();
        if (!isDead && !hasWinned && !willWin)
        {

            if (!isMoving && !diceBehind.GetIsMoving())
            {
                if (!map.IsOutOfBounds(coorX + 1, coorY))
                {
                    if (diceBehind.GetInamovible())
                    {
                        if (!map.IsEmpty(coorX + 1, coorY))
                        {
                            direction = new Vector3(0, 0, 0);

                            direction += Vector3.right;
                            moveDirection = "right";
                            isMoving = true;
                        }
                    }
                    else
                    {
                        direction = new Vector3(0, 0, 0);

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
        
    }

    public void MoveLeft()
    {
        mDir = "left";
        PulsedPad();
        if (!isDead && !hasWinned && !willWin)
        {

            if (!isMoving && !diceBehind.GetIsMoving())
            {
                if (!map.IsOutOfBounds(coorX - 1, coorY))
                {
                    //direction = new Vector3(0, 0, 0);

                    //direction -= Vector3.right;
                    //moveDirection = "left";
                    //isMoving = true;

                    //if (map.IsEmpty(coorX - 1, coorY))
                    //{
                    //    diceBehind.MoveLeft();
                    //}

                    if (diceBehind.GetInamovible())
                    {
                        if (!map.IsEmpty(coorX - 1, coorY))
                        {
                            direction = new Vector3(0, 0, 0);

                            direction -= Vector3.right;
                            moveDirection = "left";
                            isMoving = true;
                        }
                    }
                    else
                    {
                        direction = new Vector3(0, 0, 0);

                        direction -= Vector3.right;
                        moveDirection = "left";
                        isMoving = true;

                        if (map.IsEmpty(coorX - 1, coorY))
                        {
                            diceBehind.MoveLeft();
                        }
                    }




                }
            }
        }
        
    }

    public void MoveDown()
    {
        mDir = "down";
        PulsedPad();
        if (!isDead && !hasWinned && !willWin)
        {

            if (!isMoving && !diceBehind.GetIsMoving())
            {
                if (!map.IsOutOfBounds(coorX, coorY - 1))
                {
                    //direction = new Vector3(0, 0, 0);

                    //direction -= Vector3.forward;
                    //moveDirection = "down";
                    //isMoving = true;

                    //if (map.IsEmpty(coorX, coorY - 1))
                    //{

                    //    diceBehind.MoveDown();
                    //}


                    if (diceBehind.GetInamovible())
                    {
                        if (!map.IsEmpty(coorX, coorY - 1))
                        {
                            direction = new Vector3(0, 0, 0);

                            direction -= Vector3.forward;
                            moveDirection = "down";
                            isMoving = true;
                        }
                    }
                    else
                    {
                        direction = new Vector3(0, 0, 0);

                        direction -= Vector3.forward;
                        moveDirection = "down";
                        isMoving = true;

                        if (map.IsEmpty(coorX, coorY -1))
                        {
                            diceBehind.MoveDown();
                        }
                    }






                }

            }
        }
        
    }

    public void MoveUp()
    {
        mDir = "up";
        PulsedPad();
        if (!isDead && !hasWinned && !willWin)
        {

            if (!isMoving && !diceBehind.GetIsMoving())
            {
                if (!map.IsOutOfBounds(coorX, coorY + 1))
                {
                //    direction = new Vector3(0, 0, 0);

                //    direction += Vector3.forward;
                //    moveDirection = "up";
                //    isMoving = true;

                //    if (map.IsEmpty(coorX, coorY + 1))
                //    {
                //        diceBehind.MoveUp();
                //    }




                    if (diceBehind.GetInamovible())
                    {
                        if (!map.IsEmpty(coorX, coorY + 1))
                        {
                            direction = new Vector3(0, 0, 0);

                            direction += Vector3.forward;
                            moveDirection = "up";
                            isMoving = true;
                        }
                    }
                    else
                    {
                        direction = new Vector3(0, 0, 0);

                        direction += Vector3.forward;
                        moveDirection = "up";
                        isMoving = true;

                        if (map.IsEmpty(coorX, coorY + 1))
                        {
                            diceBehind.MoveUp();
                        }
                    }

                }
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

    public Dice GetDiceBehind()
    {
        return diceBehind;
    }

    public void SetIsDead(bool isd) { isDead = isd; }
    public bool GetIsDead() { return isDead; }
    public bool GetWillWin() { return willWin; }
    public void SetWillWin(bool ww) { willWin = ww; }
    public void SetHasWinned(bool hw) { hasWinned = hw; }
}
