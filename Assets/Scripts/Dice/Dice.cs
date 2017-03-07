using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    //ATRIBUTOS
    //COORDENADAS
    [SerializeField]
    private int diceCoorX;
    [SerializeField]
    private int diceCoorY;
    [SerializeField]
    private int upperFace;
    [SerializeField]
    private int frontFace = -1;

    [SerializeField]
    private bool active;
    [SerializeField]
    private float timerExplosion;
    [SerializeField]
    private int animationSpeed = 0;

    [SerializeField]
    private GameObject pivot;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject auxGameObject;
    [SerializeField]
    private Map map; // Reference to the map (board)
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private float degresAtATime = 5; // Speed of rotation, 90 % this must be 0
    private float countDegrees = 0;
    private bool isMoving = false;

    //DIRECCION
    private string moveDirection = "";
    private float speed = 100f;
    private Vector3 previusPosition;

    private int potencialScoreId = 0;


    void Awake()
    {
        diceCoorX = 0;
        diceCoorY = 0;
    }

    // Use this for initialization
    void Start ()
    {
        //degresAtATime = 5;
        player = this.gameObject;

        map = FindObjectOfType<Map>(); // Reference to the map (board)
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //degresAtATime = 5;
        if (active)
        {
            timerExplosion -= Time.deltaTime;

            if (timerExplosion <= 0)
            {
                map.RemoveDice(this);
                map.DiceDestroyed();
                DestroyDice();
            }

            GetComponent<Animator>().speed = ((4 * animationSpeed) - timerExplosion) / 2;
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

                map.RemoveDice(this);

                if (moveDirection.Equals("up"))
                {
                    diceCoorY++;

                }

                if (moveDirection.Equals("down"))
                {
                    diceCoorY--;
                }

                if (moveDirection.Equals("right"))
                {
                    diceCoorX++;
                }

                if (moveDirection.Equals("left"))
                {
                    diceCoorX--;
                }
                
                this.transform.position = new Vector3(diceCoorX,this.transform.position.y,diceCoorY);
                //map.AddDice(this);


                levelManager.PlayerMovedDice();
                map.AddDice(this);
            }
        }

    }

    public void Activate()
    {
        map = FindObjectOfType<Map>();
        if (!active) map.DiceActivated();

        active = true;
        Material redSkin = (Material) Resources.Load("Materials/UVsDiceProRed") as Material;
        gameObject.GetComponent<Renderer>().material = redSkin;

        GetComponent<Animator>().SetTrigger("Activated");
        animationSpeed = upperFace;
        
    }

    public void DestroyDice()
    {
        Debug.Log("Talolg: " + potencialScoreId);
        ScoreManager.AddScore(potencialScoreId);

        Destroy(this.gameObject);

    }

    void OnDestroy()
    {
        
    }

    public void ResetTimerExplosion()
    {
        timerExplosion = 4 * upperFace;
        animationSpeed = upperFace;
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
                isMoving = true;
                moveDirection = "right";

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
                isMoving = true;
                moveDirection = "left";

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
                isMoving = true;
                moveDirection = "down";

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
                isMoving = true;
                moveDirection = "up";

                pivot.transform.position = new Vector3(transform.position.x, 0, transform.position.z + 0.5f);
            }
        }
    }

    private void Move(string direction) // Direction: up, down, left, right 
    {
        // REVISAR
        if (direction.Equals("up"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.right, degresAtATime);
        }

        if (direction.Equals("down"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.right, -degresAtATime);
        }

        if (direction.Equals("right"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.forward, -degresAtATime);
        }

        if (direction.Equals("left"))
        {
            player.transform.RotateAround(pivot.transform.position, Vector3.forward, degresAtATime);
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
        if (!active)
        {
            timerExplosion = 4 * uf;
        }
    }
    public int GetUpperFace()
    {
        return upperFace;
    }
    public void SetFrontFace(int ff)
    {
        this.frontFace = ff;
    }
    public int GetFrontFace()
    {
        return frontFace;
    }

    public bool GetActive() { return this.active; }
    public void SetActive(bool active) { this.active = active; }

    public void SetPotencialScoreId(int id){ potencialScoreId = id;}
    public int SetPotencialScoreId() { return potencialScoreId; }
}
