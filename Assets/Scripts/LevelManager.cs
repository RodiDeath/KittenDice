﻿using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Map map;
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

    private bool timerActivated = false;

    // UI
    [SerializeField]
    private Text textTime;
    [SerializeField]
    private Text textMoves;
    [SerializeField]
    private Text textLevelName;

    // Use this for initialization
    void Start ()
    {
        dice = GetComponent<Dice>();
        LoadLevel(1);
        textTime.text = timerCount.ToString("0");
        textMoves.text = movements.ToString();
        StartTimer();
    }

    public void LoadLevel(int lvl)
    {
        map.ResetBoard(); // Resets the map to all null
        levelName = "Level " + lvl;
        levelNumber = lvl;

        textLevelName.text = levelName.ToString();

        string levelPath = "Levels/7x7/Level" + lvl; // Path of the txt level file
        levelData = (TextAsset)Resources.Load(levelPath, typeof(TextAsset)); // Stores the txt file in a TextAsset variable

        string levelPathFrontFaces = "Levels/7x7/Level" + lvl + "FrontFaces"; // Path of the txt level front faces file
        levelFrontFaces = (TextAsset)Resources.Load(levelPathFrontFaces, typeof(TextAsset)); // Stores the txt file in a TextAsset variable


        string[] splitFile = new string[] { "\r\n", "\r", "\n" }; // Set the split parameter (\r\n -> New Line)
        string[] levelLines = levelData.text.Split(splitFile, System.StringSplitOptions.None); // Stores in a string[] all the txt lines separately (the séparation)
        
        string[] frontFacesLines = levelFrontFaces.text.Split(splitFile, System.StringSplitOptions.None);


        Array.Reverse(levelLines); // Inverts the array because china
        Array.Reverse(frontFacesLines); // Inverts the array because china


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

                player.MoveTo(startPositionX, startPositionY);
                textTime.text = timerCount.ToString("0");
                textMoves.text = movesCount.ToString();
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
        if (timerActivated)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(1);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        LoadLevel(levelNumber + 1);
    }

    public void PlayerWillWin()
    {
        StopTimer();
    }

    public void PlayerDied()
    {
        Debug.Log("Game Over - Lost");
        StopTimer();
    }

    public void PlayerWins()
    {
        Debug.Log("GameOver - Win");
        StopTimer();
    }

    public bool PlayerMovedDice()
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
}
