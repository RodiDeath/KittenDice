using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    public int[,] grid;
    public int boardSize = 7;


	// Use this for initialization
	void Start ()
    {
        grid = new int[boardSize, boardSize];

        ResetBoard();
        //FillBoard();
    }

    public void FillBoard() // Finds all objects of type Dice and add them to the grid
    {
        ResetBoard();

        Dice[] diceInTable = FindObjectsOfType<Dice>();

        foreach (var dice in diceInTable)
        {
            grid[dice.GetDiceCoorX(), dice.GetDiceCoorY()] = dice.GetUpperFace();
        }

        PrintBoard();
    }

    public void PrintBoard() // Prints in Console the grid (only for debug purposes)
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Debug.Log("(" + i + "," + j + ") -> " + grid[i, j]);
            }
        }
    }

    private void ResetBoard() // Resets the board, all the cells to value 0
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                grid[i, j] = 0;
            }
        }
    }

    public int GetCellValue(int x, int y) // Returns the value of an especified board cell
    {
        return grid[x, y];
    }

    public bool IsEmpty(int x, int y) // Returns true if the cell's value is 0 and false if it's other value or if the coordinates are out of range
    {
        if (x < boardSize && y < boardSize && x >= 0 && y >= 0)
        {
            if (grid[x, y] == 0) return true;
            else return false;
        }
        else
        {
            return false;
        }
    }

    public void AddDice(Dice dice) // Adds logically a dice value to the board 
    {
        if (GetCellValue(dice.GetDiceCoorX(), dice.GetDiceCoorY()) == 0)
        {
            grid[dice.GetDiceCoorX(), dice.GetDiceCoorY()] = dice.GetUpperFace();
        }
    }

    public void RemoveDice(Dice dice) // Removes logically a dice value from the board
    {
        grid[dice.GetDiceCoorX(), dice.GetDiceCoorY()] = 0;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
