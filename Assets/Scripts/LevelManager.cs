using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Map map;
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private GameManager gameManager;

    private TextAsset levelData;
    private TextAsset levelFrontFaces;
    private Dice dice;
    [SerializeField]
    private PlayerController player;
    private bool playerWillWin = false;

    private int startPositionX = 0, startPositionY = 0; // Start Position of the player

    [SerializeField]
    private float time = 0.0f; // Time to finish the level
    [SerializeField]
    private float timerCount = 0.0f;
    [SerializeField]
    private int movements = 0; // Movements to finish the level
    [SerializeField]
    private int movesCount = 0;
    [SerializeField]
    private string levelType = ""; // puzzle, survival, ...
    [SerializeField]
    private string levelName = ""; // Name or number of the level
    [SerializeField]
    private int levelNumber = 1;
    [SerializeField]
    private int worldNumber = 1;
    [SerializeField]
    private int worldCount;
    [SerializeField]
    private int levelCount;

    private bool infiniteTime = false;
    private bool infiniteMoves = false;

    private bool timerActivated = false;

    // Map
    int levelHeight;
    int levelWidth;

    // UI
    [SerializeField]
    private Text textTime;
    [SerializeField]
    private Text textMoves;
    [SerializeField]
    private Text textLevelName;

    [SerializeField]
    private GameObject infiniteIconMoves;
    [SerializeField]
    private GameObject infiniteIconTime;

    // Use this for initialization
    void Start ()
    {
        

        dice = GetComponent<Dice>();
        //LoadLevel(1);
        //textTime.text = timerCount.ToString("0");
        //textMoves.text = movements.ToString();
        StartTimer();

        GetAllLevelStrings("Air");
    }

    public static List<string> GetAllLevelStrings(string world)
    {
        List<string> levelsList = new List<string>();
        
        UnityEngine.Object[] allResources = Resources.LoadAll("Levels/World"+ world, typeof(TextAsset));

        foreach (UnityEngine.Object obj in allResources)
        {
            if (!obj.name.Contains(".meta") && !obj.name.Contains("FrontFaces"))
            {
                levelsList.Add(obj.name.Split('.')[0]);
            }
        }
        return levelsList;
    }

    public void CalculateMapSize(string world, int lvl)
    {
        levelName = "Level " + lvl;
        levelNumber = lvl;

        textLevelName.text = levelName.ToString();

        string levelPath = "Levels/World"+ world+"/Level" + lvl; // Path of the txt level file
        levelData = (TextAsset)Resources.Load(levelPath, typeof(TextAsset)); // Stores the txt file in a TextAsset variable

        string levelPathFrontFaces = "Levels/World" + world + "/Level" + lvl + "FrontFaces"; // Path of the txt level front faces file
        levelFrontFaces = (TextAsset)Resources.Load(levelPathFrontFaces, typeof(TextAsset)); // Stores the txt file in a TextAsset variable


        string[] splitFile = new string[] { "\r\n", "\r", "\n" }; // Set the split parameter (\r\n -> New Line)
        string[] levelLines = levelData.text.Split(splitFile, System.StringSplitOptions.None); // Stores in a string[] all the txt lines separately (the séparation)

        string[] frontFacesLines = levelFrontFaces.text.Split(splitFile, System.StringSplitOptions.None);


        Array.Reverse(levelLines); // Inverts the array because china
        Array.Reverse(frontFacesLines); // Inverts the array because china

        levelHeight = levelLines.Length - 1;
        levelWidth = levelLines[1].Length;


    }

    public void LoadLevel(string world, int lvl)
    {
        infiniteIconTime.SetActive(false);
        infiniteIconMoves.SetActive(false);

        map.ResetBoard(); // Resets the map to all null
        levelName = "Level " + lvl;
        levelNumber = lvl;

        textLevelName.text = levelName.ToString();

        string levelPath = "Levels/World" + world + "/Level" + lvl; // Path of the txt level file
        levelData = (TextAsset)Resources.Load(levelPath, typeof(TextAsset)); // Stores the txt file in a TextAsset variable

        string levelPathFrontFaces = "Levels/World" + world + "/Level" + lvl + "FrontFaces"; // Path of the txt level front faces file
        levelFrontFaces = (TextAsset)Resources.Load(levelPathFrontFaces, typeof(TextAsset)); // Stores the txt file in a TextAsset variable


        string[] splitFile = new string[] { "\r\n", "\r", "\n" }; // Set the split parameter (\r\n -> New Line)
        string[] levelLines = levelData.text.Split(splitFile, System.StringSplitOptions.None); // Stores in a string[] all the txt lines separately (the séparation)
        
        string[] frontFacesLines = levelFrontFaces.text.Split(splitFile, System.StringSplitOptions.None);


        Array.Reverse(levelLines); // Inverts the array because china
        Array.Reverse(frontFacesLines); // Inverts the array because china

        levelHeight = levelLines.Length - 1;
        levelWidth = levelLines[1].Length;

        mapGenerator.CreateMap(levelHeight, levelWidth); // Creates the map (ground)


        int i = 0;
        int j = 0;
        foreach (var line in levelLines)
        {
            foreach (var car in line)
            {
                if (i < levelLines.Length -1)
                {
                    dice = GetComponent<Dice>();

                    switch (car)
                    {
                        case '0':
                            dice = null;
                            break;

                        case '1':
                            dice.SetUpperFace(1);
                            break;

                        case '2':
                            dice.SetUpperFace(2);
                            break;

                        case '3':
                            dice.SetUpperFace(3);
                            break;

                        case '4':
                            dice.SetUpperFace(4);
                            break;

                        case '5':
                            dice.SetUpperFace(5);
                            break;

                        case '6':
                            dice.SetUpperFace(6);
                            break;
                    }
                    

                    if (dice != null)
                    {
                        try
                        {
                            dice.SetFrontFace((int)Char.GetNumericValue(frontFacesLines[i].ToCharArray()[j]));
                        }
                        catch { }
                        

                        dice.SetDiceCoorX(j);
                        dice.SetDiceCoorY(i);
                        map.CreateDice(dice);
                    }
                    j++;
                }

            }
            i++;
            j = 0;

            if (i == levelLines.Length) // Last (First after reversing) line of the txt where is stored the leven information
            {
                string[] levelDataString = line.Split('*');

                startPositionX = Convert.ToInt32(levelDataString[0]);
                startPositionY = Convert.ToInt32(levelDataString[1]);
                levelType = levelDataString[2];
                time = float.Parse(levelDataString[3]);
                timerCount = time;
                movements = Convert.ToInt32(levelDataString[4]);
                movesCount = movements;

                textTime.text = timerCount.ToString("0");
                textMoves.text = movesCount.ToString();

                if (movements == 0)
                {
                    infiniteMoves = true;
                    textMoves.text = "";
                    infiniteIconMoves.SetActive(true);
                }

                if (time == 0)
                {
                    infiniteTime = true;
                    textTime.text = "";
                    infiniteIconTime.SetActive(true);
                }

                player.MoveTo(startPositionX, startPositionY);
                
                player.SetIsDead(false);
                player.SetHasWinned(false);
                player.SetWillWin(false);

                StartTimer();

            }
        }
    }

    
	
	// Update is called once per frame
	void Update ()
    {
        if (timerActivated && !infiniteTime)
        {
            timerCount -= Time.deltaTime;
            textTime.text = timerCount.ToString("0");

            if (timerCount <= 0)
            {
                player.SetIsDead(true);
                PlayerDied();
                ResetTimer();
                StopTimer();
            }
        }

        
    }

    //public void LoadNextLevel()
    //{
    //    LoadLevel(worldNumber, levelNumber + 1);
    //}

    public void PlayerWillWin()
    {
        StopTimer();
    }

    public void PlayerDied()
    {
        gameManager.GameOver();
        Debug.Log("Game Over - Lost");
        StopTimer();
    }

    public void PlayerWins()
    {
        gameManager.Completed();
        Debug.Log("GameOver - Win");
        StopTimer();
    }

    public bool PlayerMovedDice()
    {
        if (!infiniteMoves)
        {
            movesCount--;
            textMoves.text = movesCount.ToString();

            if (movesCount <= 0)
            {
                player.CheckWinLose();

                if (!player.GetWillWin())
                {
                    PlayerDied();
                    player.SetIsDead(true);
                    return false;
                }
            }
        }

        return true;
    }

    public void StartTimer() { timerActivated = true; }
    public void StopTimer() { timerActivated = false; }
    public void ResetTimer() { timerCount = time; }

    public int GetStartPositionX()
    {
        return startPositionX;
    }

    public int GetStartPositionY()
    {
        return startPositionY;
    }

    public int GetLevelNumber() { return levelNumber; }

    public int GetBoardHeight() { return levelHeight;}
    public int GetBoardWidth() { return levelWidth; }
}
