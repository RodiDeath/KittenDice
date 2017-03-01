using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;

    private Dice[,] grid;
    [SerializeField]
    private int boardSize = 7;

    [SerializeField]
    private int boardHeight;
    [SerializeField]
    private int boardWidth;

    [SerializeField]
    private GameObject dicePrefab;

    [SerializeField]
    private Transform DicesFolder;

    [SerializeField]
    private int diceCount = 0;
    [SerializeField]
    private int diceActivatedCount = 0;

    void Awake()
    {
        
    }

    public void InitializeMap(int height, int width)
    {
        ResetBoard();
        boardHeight = width;
        boardWidth = height;

        grid = new Dice[width, height];
        
    }

    public void PrintBoard() // Prints in Console the grid (only for debug purposes)
    {
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                if (grid[i,j] == null) Debug.Log("(" + i + "," + j + ") -> 0");
                else Debug.Log("(" + i + "," + j + ") -> " + grid[i, j].GetUpperFace());
            }
        }
    }

    public void ResetBoard() // Resets the board, all the cells to value 0
    {
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                //Debug.Log("i: " + i + ", j: " + j);
                if (grid[i, j] != null)
                {
                    Destroy(grid[i, j].gameObject);
                    grid[i, j] = null;
                }
            }
        }

        diceCount = 0;
        diceActivatedCount = 0;
    }

    public int GetCellValue(int x, int y) // Returns the value of an especified board cell
    {
        //Debug.Log("X: " + x + ", Y: " + y);
        
        if (grid[x, y] == null)
        {
            return 0;
        }
        else
        {
            return grid[x, y].GetUpperFace();
        }
        
    }

    public bool IsEmpty(int x, int y) // Returns true if the cell's value is 0 and false if it's other value or if the coordinates are out of range
    {
        if (x < boardHeight && y < boardWidth && x >= 0 && y >= 0)
        {
            if (grid[x, y] == null) return true;
            else return false;
        }
        else
        {
            return false;
        }
    }

    public bool IsOutOfBounds(int x, int y)
    {
        if (x < boardHeight && y < boardWidth && x >= 0 && y >= 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public void AddDice(Dice dice) // Adds logically a dice value to the board 
    {
        if (GetCellValue(dice.GetDiceCoorX(), dice.GetDiceCoorY()) == 0)
        {
            grid[dice.GetDiceCoorX(), dice.GetDiceCoorY()] = dice;
            DetectEquals(dice);
            diceCount++;

        }
    }

    public void RemoveDice(Dice dice) // Removes logically a dice value from the board
    {
        grid[dice.GetDiceCoorX(), dice.GetDiceCoorY()] = null;
        diceCount--;
    }

    public bool IsFull()
    {
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                if (grid[i, j] == null) return false;
            }
        }
        return true;
    }

    public Dice GetDice(int x, int y)
    {
        return grid[x, y];
    }

    public void CreateDice(Dice dice)
    {
        GameObject newDice = Instantiate(dicePrefab, new Vector3(dice.GetDiceCoorX(), 0.5f, dice.GetDiceCoorY()), Quaternion.identity) as GameObject;
        Dice newDiceScript = newDice.GetComponent<Dice>();
        newDiceScript.SetDiceCoorX(dice.GetDiceCoorX());
        newDiceScript.SetDiceCoorY(dice.GetDiceCoorY());
        
        newDiceScript.SetUpperFace(dice.GetUpperFace());
        newDiceScript.SetFrontFace(dice.GetFrontFace());

        newDice.transform.GetChild(1).transform.GetChild(0).GetComponent<FaceDetector>().TurnDiceTo(dice.GetUpperFace(), dice.GetFrontFace());
        newDice.transform.SetParent(DicesFolder);

        AddDice(newDiceScript);
    }

    public void DetectEquals(Dice originDice)
    {
        List<Dice> diceList = new List<Dice>();

        diceList.Add(originDice);

        for (int i = 0; i < diceList.Count; i++)
        {
            DetectNearEquals(diceList[i], diceList);
        }

        if (originDice.GetUpperFace() == 1 && diceList.Count >= 2)
        {
            foreach (var dice in diceList)
            {
                dice.Activate();
                dice.ResetTimerExplosion();
            }
        }
        else
        if ((diceList.Count >= originDice.GetUpperFace()) && (originDice.GetUpperFace() != 1))
        {
            foreach (var dice in diceList)
            {
                dice.Activate();
                dice.ResetTimerExplosion();
            }
           // Debug.Log("Hay " + diceList.Count + " dados de cara " + diceList[0].GetUpperFace().ToString());

            /***********************************/
            // Calcualte SCORE !!!!!!!!!!!!!!!
            /***************************************/
            int score = diceList.Count * diceList[0].GetUpperFace() * diceList[0].GetUpperFace() * 100;


            int potId = ScoreManager.AddPotencialScore(score);

            foreach (var dice in diceList)
            {
                dice.SetPotencialScoreId(potId);
            }
        }
        
    }

    private void DetectNearEquals(Dice originDice, List<Dice> diceList)
    {
        if (!IsEmpty(originDice.GetDiceCoorX() - 1, originDice.GetDiceCoorY()) &&
                    !IsOutOfBounds(originDice.GetDiceCoorX() - 1, originDice.GetDiceCoorY())) // Check Left Cell
        {
            Dice letfDice = GetDice(originDice.GetDiceCoorX() - 1, originDice.GetDiceCoorY());

            if (letfDice.GetUpperFace() == originDice.GetUpperFace())
            {
                if (!diceList.Contains(letfDice)) diceList.Add(letfDice);
            }
        }

        if (!IsEmpty(originDice.GetDiceCoorX() + 1, originDice.GetDiceCoorY()) &&
            !IsOutOfBounds(originDice.GetDiceCoorX() + 1, originDice.GetDiceCoorY())) // Check Right Cell
        {
            Dice rightDice = GetDice(originDice.GetDiceCoorX() + 1, originDice.GetDiceCoorY());

            if (rightDice.GetUpperFace() == originDice.GetUpperFace())
            {
                if (!diceList.Contains(rightDice)) diceList.Add(rightDice);
            }
        }

        if (!IsEmpty(originDice.GetDiceCoorX(), originDice.GetDiceCoorY() + 1) &&
            !IsOutOfBounds(originDice.GetDiceCoorX(), originDice.GetDiceCoorY() + 1)) // Check Up Cell
        {
            Dice upDice = GetDice(originDice.GetDiceCoorX(), originDice.GetDiceCoorY() + 1);

            if (upDice.GetUpperFace() == originDice.GetUpperFace())
            {
                if (!diceList.Contains(upDice)) diceList.Add(upDice);
            }
        }

        if (!IsEmpty(originDice.GetDiceCoorX(), originDice.GetDiceCoorY() - 1) &&
            !IsOutOfBounds(originDice.GetDiceCoorX(), originDice.GetDiceCoorY() - 1)) // Check Down Cell
        {
            Dice downDice = GetDice(originDice.GetDiceCoorX(), originDice.GetDiceCoorY() - 1);

            if (downDice.GetUpperFace() == originDice.GetUpperFace())
            {
                if (!diceList.Contains(downDice)) diceList.Add(downDice);
            }
        }
    }


    public int GetDiceCount() { return diceCount; }
    public void SetDiceCount(int count) { diceCount = count; }
    public void DiceActivated() { diceActivatedCount++; }
    public void DiceDestroyed() { diceActivatedCount--; }
    public int GetBoardSize() { return boardSize; }
    public void SetBoardSize(int bs) { boardSize = bs; }

    public bool AllDicesActivated()
    {
        if (diceActivatedCount >= diceCount) return true;
        else return false;
    }
}
