using UnityEngine;
using System;


public class LevelManager : MonoBehaviour
{
    public Map map;
    TextAsset levelData;
    Dice dice;
    int startPositionX = 0, startPositionY = 0;

    // Use this for initialization
    void Start ()
    {
        dice = GetComponent<Dice>();
        LoadLevel(1);
	}

    public void LoadLevel(int lvl)
    {
        map.ResetBoard(); // Resets the map to all null

        string levelPath = "Levels/7x7/Level" + lvl; // Path of the txt level file
        levelData = (TextAsset)Resources.Load(levelPath, typeof(TextAsset)); // Stores the txt file in a TextAsset variable
        

        string[] splitFile = new string[] { "\r\n", "\r", "\n" }; // Set the split parameter (\r\n -> New Line)
        string[] levelLines = levelData.text.Split(splitFile, System.StringSplitOptions.None); // Stores in a string[] all the txt lines separately (the séparation)


        Array.Reverse(levelLines); // Inverts the array because china


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

            if (i == levelLines.Length) // Last (First after reversing) line of the txt where is stored the start position
            {
                startPositionX = (int)Char.GetNumericValue(line.ToCharArray()[0]);
                startPositionY = (int)Char.GetNumericValue(line.ToCharArray()[1]);
            }

            
        }
        //map.LoveMeLikeYouDo();

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public int GetStartPositionX()
    {
        return startPositionX;
    }

    public int GetStartPositionY()
    {
        return startPositionY;
    }
}
