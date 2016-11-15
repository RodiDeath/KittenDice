﻿using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    public Dice[,] grid;
    public int boardSize = 7;


	// Use this for initialization
	void Start ()
    {
        grid = new Dice[boardSize, boardSize];

        ResetBoard();
        //FillBoard();
    }

    //public void FillBoard() // Finds all objects of type Dice and add them to the grid
    //{
    //    ResetBoard();

    //    Dice[] diceInTable = FindObjectsOfType<Dice>();

    //    foreach (var dice in diceInTable)
    //    {
    //        grid[dice.GetDiceCoorX(), dice.GetDiceCoorY()] = dice.GetUpperFace();
    //    }

    //    PrintBoard();
    //}

    public void PrintBoard() // Prints in Console the grid (only for debug purposes)
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (grid[i,j] == null) Debug.Log("(" + i + "," + j + ") -> 0");
                else Debug.Log("(" + i + "," + j + ") -> " + grid[i, j].GetUpperFace());
            }
        }
    }

    private void ResetBoard() // Resets the board, all the cells to value 0
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                grid[i, j] = null;
            }
        }
    }

    public int GetCellValue(int x, int y) // Returns the value of an especified board cell
    {
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
        if (x < boardSize && y < boardSize && x >= 0 && y >= 0)
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
        if (x < boardSize && y < boardSize && x >= 0 && y >= 0)
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
        }
    }

    public void RemoveDice(Dice dice) // Removes logically a dice value from the board
    {
        grid[dice.GetDiceCoorX(), dice.GetDiceCoorY()] = null;
    }

    public bool IsFull()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
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

    public void LoveMeLikeYouDo()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                int currentUpperFace=0;
                int currentUpperFaceUp = 0;
                int currentUpperFaceRight = 0;


                if (grid[i, j] == null) currentUpperFace = 0;
                else currentUpperFace = grid[i, j].GetUpperFace();

                if (grid[i+1, j] == null) currentUpperFaceUp = 0;
                else currentUpperFaceUp = grid[i, j].GetUpperFace();

                if (IsEmpty(i, j+1)) currentUpperFaceRight = 0;
                else currentUpperFaceRight = grid[i, j].GetUpperFace();
                

                if (currentUpperFace == currentUpperFaceUp && currentUpperFaceUp!=0 && currentUpperFace!=0)
                {
                    Destroy(grid[i, j].gameObject);
                    Destroy(grid[i+1, j].gameObject);
                    RemoveDice(grid[i, j]);
                    RemoveDice(grid[i+1, j]);
                }

                if (currentUpperFace == currentUpperFaceRight && currentUpperFaceRight != 0 && currentUpperFace != 0)
                {
                    Destroy(grid[i, j].gameObject);
                    Destroy(grid[i, j+1].gameObject);
                    RemoveDice(grid[i, j]);
                    RemoveDice(grid[i, j+1]);
                }
            }
        }
    }
}
