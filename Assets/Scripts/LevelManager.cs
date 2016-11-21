using UnityEngine;
using System;


public class LevelManager : MonoBehaviour
{
    public Map map;
    TextAsset levelData;
    TextAsset levelFrontFaces;
    Dice dice;

    int startPositionX = 0, startPositionY = 0; // Start Position of the player
    public float time = 0.0f; // Time to finish the level
    public float timerCount = 0.0f;
    public int movements = 0; // Movements to finish the level
    public string levelType = ""; // puzzle, survival, ...
    public string levelName = ""; // Name or number of the level

    bool timerActivated = false;
    

    // Use this for initialization
    void Start ()
    {
        dice = GetComponent<Dice>();
        LoadLevel(1);
	}

    public void LoadLevel(int lvl)
    {
        map.ResetBoard(); // Resets the map to all null
        levelName = "Level " + lvl;

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

                    //dice = gameObject.AddComponent<Dice>();
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
                        //dice.SetFrontFace(frontFacesLines[j][i]);
                        try
                        {
                            dice.SetFrontFace((int)Char.GetNumericValue(frontFacesLines[i].ToCharArray()[j]));
                        }
                        catch { }
                        

                        dice.SetDiceCoorX(j);
                        dice.SetDiceCoorY(i);
                        map.CreateDice(dice);
                    }

                    //Destroy(gameObject.GetComponent<Dice>());
                    j++;
                }

            }
            i++;
            j = 0;

            if (i == levelLines.Length) // Last (First after reversing) line of the txt where is stored the leven information
            {
                string[] levelDataString = line.Split('*');

                //startPositionX = (int)Char.GetNumericValue(line.ToCharArray()[0]);
                //startPositionY = (int)Char.GetNumericValue(line.ToCharArray()[1]);

                startPositionX = Convert.ToInt32(levelDataString[0]);
                startPositionY = Convert.ToInt32(levelDataString[1]);
                levelType = levelDataString[2];
                time = float.Parse(levelDataString[3]);
                timerCount = time;
                movements = Convert.ToInt32(levelDataString[4]);
            }

            
        }
        //map.LoveMeLikeYouDo();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timerActivated)
        {
            timerCount -= Time.deltaTime;
        }
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
}
