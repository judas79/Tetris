using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    // T10 first, a representation of our gameboard, inside of an array, be able to access this from outside of our array
    // use a bunch of Transforms, so we don't have to constantly refer everything as a game object transform, but as simple transform instead
    // gameBoard is a 10 by 20 array
    public static Transform[,] gameBoard = new Transform[10, 20];

    // T10 this is temporary, and will not be in the final game.
    // T10 print out everything that the gameboard has to the MyArray, object we created
    public static void PrintArray()
    {
        // T10 set up array output
        string arrayOutput = "";

        // T10 gets -1 from the beginning of the index which is 0, for rows and columns pf array
        int iMax = gameBoard.GetLength(0) - 1;
        int jMax = gameBoard.GetLength(1) - 1;

        // T10 cycle through the array and print it all out to MyArray
        for (int j = jMax; j >= 0; j--)
        {
            for (int i = 0; i <= iMax; i++)
            {
                // T10 check inside array, if we have a null value, print out n
                // if we do not have a null value prin an x, which represents our shapes
                if (gameBoard[i, j] == null)
                {
                    // T10 output an N to our MyArray if the cell is Null, or else; it has a shape output an X
                    arrayOutput += "N ";
                }
                else
                {
                    arrayOutput += "X ";
                }                
            }
            // add empty newlines
            arrayOutput += "\n \n";
        }
        // T10 Get the gameObject MyArray text component and,
        // put the information in the MyArray component text box
        var myArrayComp = GameObject.Find("MyArray").GetComponent<Text>();
        myArrayComp.text = arrayOutput;
    }

    // T11 return bool if rows have been deleted, by cycling through every row
    // TODO create IsRowFull(), DeleteGBRow(), functions
    public static bool DeleteAllFullRows()
    {
        // T11 cycle trhough the rows
        for (int row = 0; row < 20; ++row)
        {
            // T11 check if we have a full row or not, using the IsRowFill function, passing in row
            if (IsRowFull(row))
            {
                // T11 call the Delete GameBoard Row fucntion, to delete this (row)
                DeleteGBRow(row);

                // TODO : Make Sound
                // T12 play a sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rowDelete);

                // it is true we deleted all full rows
                return true;
            }
        }
        // T11 otherwise return false, we did not deleted all full rows
        return false;
    }

    // T11 return bool, receive the row we are to investigate
    public static bool IsRowFull(int row)
    {
        // there are 9 columns 0 - 10, 10 not included
        for (int col = 0; col < 10; ++col)
        {
            // rmemeber, all columns will either contain a null or a transform, if we find a row with a null, 
            // it means the row has an empty cell, which will return a false, don't delete row
            if(gameBoard[col, row] == null)
            {
                return false;
            }
        }
        // T11 if a null is not found in the row, then its true
        return true;
    }

    // T11 passed in, the row number to be deleted
    public static void DeleteGBRow(int row)
    {
        // cycle though the row, in the Scene as well as in the Array and destroy them, 
        // null being empty as apposed to a transform, in the array
        for(int col = 0; col < 10; ++col)
        {
            Destroy(gameBoard[col, row].gameObject);
            gameBoard[col, row] = null;
        }
        // T11 increment up a row, to make the other rows go downwards
        row++;

        // T11 cycle thru all our rows
        for(int j = row; j < 20; ++j)
        {
            // T11 cycle thru all our columns
            for(int col = 0; col < 10; ++col)
            {
                // check if there is a block in the cell, move downwards 1
                if(gameBoard[col, j] != null)
                {
                    gameBoard[col, j - 1] = gameBoard[col, j];

                    // T11 delete the cube that was moved down in the array
                    gameBoard[col, j] = null;

                    // T11 change the cube that was moved down in the scenes position
                    gameBoard[col, j - 1].position += new Vector3(0, -1, 0);
                }
            }

        }
    }
}
