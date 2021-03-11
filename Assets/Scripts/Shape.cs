using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// T12 to change scenes from main scene to another scene
using UnityEngine.SceneManagement;
// T11 to add the ability to increase the score
using UnityEngine.UI;

public class Shape : MonoBehaviour
{
    
    // T9 figure out what sort of things we are going to do with this shape
    // Increase the speed as difficulty increases
    // Monitor when user clicks on different keys, We will go back to Unity to define the inputs
    //

    // T9 Increase the speed as difficulty increases, Define speed
    // T12 changed speed to static
    //public float speed = 1.0f;
    public static float speed = 1.0f;

    // keep track of the last movement down, of the current Shape, will be incremented once a second
    float lastMoveDown = 0;

    int textUIComp2;
    int score = 0;
    //public int passScore;

    // Start is called before the first frame update
    void Start()
    {
        

        // T12 if at any time a shapes shows up and its not in the grid, that means it is messed up in some way,
        // such as being an overlapping shape, like when they overlap on the top, and spawn on top of eachother,
        // because the play area is full, will be game over
        if (!IsInGrid())
        {
            // T12 play game over sound
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShapeMove);
            
            // T12 Show our GameOver Scene, by calling a function
            Invoke("OpenGameOverScene", .5f);
        }
        // T12 call the IncreaseSpeed function, to speed up the falling of the shapes as game progresses
        // wait 2 seconds, then after that, every 2 seconds call IncreaseSpeed
        InvokeRepeating("IncreaseSpeed", 2.0f, 2.0f);
    }

    // T12 open up our GameOver Scene
    void OpenGameOverScene()
    {
        // so if play again is chosen in the game over scene, the games speed starts at 1 again
        speed = 1.0f;

        // T12 Destroy our Shapes
        Destroy(gameObject);

        // call IncreaseTextUIScore() to get updated value of score from textUIComp2
        IncreaseTextUIScore();

        // put the score into playorefs and store it as passScore to pass value to the GameOver Script
        PlayerPrefs.SetInt("passScore", textUIComp2);

        // T12 call the SceneManager and load the scene
        SceneManager.LoadScene("GameOver");
    }

    // T12 function to increase speed
    void IncreaseSpeed()
    {
        Shape.speed -= .0001f;
    }

    // Update is called once per frame
    void Update()
    {
        // if escape key is pressed the game end abruptly
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // T9 we will do the same kind of if statement for all four input keys we created

        // T9 Monitor if an input key is pressed one time
        // and its the 'a' key
        if (Input.GetKeyDown("a"))
        {
            // T9 Then we want the position, to change 1 step to the left, for our Shape
            // it will know we mean the Shape object
            transform.position += new Vector3(-1, 0, 0);

            // we know we want to prevent the shape from leaving the play area,
            // but do not know how, so we will instead get a read out onthe screen,
            // of our transport. position, to aid us in figuring it out
            //Debug.Log(transform.position);

            // T9 stop the movement, or rotation of shape, if it is hitting a border
            // by returning the shape to its last position, when it hits the border, 
            // using NOT IsInBorder, to do the opposite of what we just did,
            // we pass transform.position to IsInBorder, to give it the position
            // if your run the gme now, you will notice it stops one too late, on the border, 
            // so subract 1, or add one, depending what the inversive of the coordinate was, 
            // BELOW (in the IsInBorder() function)
            //if (!IsInBorder(transform.position))
            // T9 we have to change this, to use our IsInGrid, instead of using the shape position
            // we will use the child center of square position, to calculate our position to the walls and bottom
            if (!IsInGrid())
            {
                transform.position += new Vector3(1, 0, 0);
            }
            // T10 otherwise call UpdateGameBoard() to update it while tracking the movements
            else
            {
                UpdateGameBoard();

                // T12 play a sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShapeMove);
            }
        }

        // T9 Monitor if an input key is pressed one time
        // and its the 'd' key
        if (Input.GetKeyDown("d"))
        {
            // T9 Then we want the position, to change 1 step to the right, for our Shape
            // it will know we mean the Shape object
            transform.position += new Vector3(1, 0, 0);

            // we know we want to prevent the shape from leaving the play area,
            // but do not know how, so we will instead get a read out onthe screen,
            // of our transport. position, to aid us in figuring it out
            //Debug.Log(transform.position);

            // T9 stop the movement, or rotation of shape, if it is hitting a border
            // by returning the shape to its last position, when it hits the border, 
            // using NOT IsInBorder, to do the opposite of what we just did,
            // we pass transform.position to IsInBorder, to give it the position
            // if your run the gme now, you will notice it stops one too late, on the border, 
            // so subract 1, or add one, depending what the inversive of the coordinate was, 
            // BELOW (in the IsInBorder() function)
            //if (!IsInBorder(transform.position))
            // T9 we have to change this, to use our IsInGrid, instead of using the shape position
            // we will use the child center of square position, to calculate our position to the walls and bottom
            if (!IsInGrid())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            // T10 otherwise call UpdateGameBoard() to update it while tracking the movements
            else
            {
                UpdateGameBoard();

                // T12 play a sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShapeMove);
            }
        }

        // T9 Monitor if an input key is pressed one time
        // and its the 's' key, 
        // or if its been one second since the shape last moved, 
        // the first time thu value will equal 1 then increment by 1, every second
        // then subtract the lastMoveDown, and compare that it is more thanf 1, to know that 1 second has past
        // T12 to increase the speed as game progresses we changed the 1 to our Shape.speed, so the speed changes over time, faster
        //if (Input.GetKeyDown("s") || Time.time - lastMoveDown >= 1)
        if (Input.GetKeyDown("s") || Time.time - lastMoveDown >= Shape.speed)
        {
            // T9 Then we want the position, to change 1 step down, for our Shape
            // it will know we mean the Shape object
            transform.position += new Vector3(0, -1, 0);

            // we know we want to prevent the shape from leaving the play area,
            // but do not know how, so we will instead get a read out onthe screen,
            // of our transport. position, to aid us in figuring it out
            //Debug.Log(transform.position);

            // T9 stop the movement, or rotation of shape, if it is hitting a border
            // by returning the shape to its last position, when it hits the border, 
            // using NOT IsInBorder, to do the opposite of what we just did
            // we pass transform.position to IsInBorder, to give it the positio
            //if (!IsInBorder(transform.position))
            // T9 we have to change this, to use our IsInGrid, instead of using the shape position
            // we will use the child center of square position, to calculate our position to the walls and bottom
            if (!IsInGrid())
            {
                transform.position += new Vector3(0, 1, 0);

                // T11 call our DeleteAllFullRows() function from GameBoard.cs, to get a true/ false
                // if a row was deleted or not
                bool rowDeleted = GameBoard.DeleteAllFullRows();

                // T11 if a row was deleted, verify that no other rows exist
                if (rowDeleted)
                {
                    // T11 call static fnction DeleteAllFullRows in GameBoard.cs
                    // to see if we have the ablility to delete rows
                    GameBoard.DeleteAllFullRows();

                    // T10 monitor this with the console
                    //Debug.Log("SCORE INCREMENTED BY 1");
                    // T11 TODO Change the score, after checking that the bottom row deletes, in Unity
                    // This call to the IncreaseTextUIScore(); function, will change the score, when the row is deleted
                    // the function will be created somewhere at the bottom in this script
                    IncreaseTextUIScore();


                }

                //T10 after the shape has been moved down into position, and can't be moved down further,
                // we want to disable it from moving at all
                enabled = false;

                // add 2 for using and landing the shape
                IncreaseTextUIScore2();

                //T10 spawn a new shape, by calling SpawnShape() from the ShapeSpawner.cs
                // find the object of type ShapeSpawner.cs, then the function we want to call
                FindObjectOfType<ShapeSpawner>().SpawnShape();

                // T12 play a sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShapeStop);
            }
            // T10 otherwise call UpdateGameBoard() to update it while tracking the movements
            else
            {
                UpdateGameBoard();

                // T12 play a sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShapeMove);
            }

            // T9 reset lastMoveDown to the previous version of time, each time it goes through here
            lastMoveDown = Time.time;
        }

        // T9 Monitor if an input key is pressed one time
        // and its the 'w' key
        if (Input.GetKeyDown("w"))
        {
            // T9 Then we want the position, to change it, to rotate 90 degrees, for our Shape
            // it will know we mean the Shape object
            transform.Rotate(0, 0, 90);

            // we know we want to prevent the shape from leaving the play area,
            // but do not know how, so we will instead get a read out onthe screen,
            // of our transport. position, to aid us in figuring it out
            //Debug.Log(transform.position);

            // T9 we have to change this, to use our IsInGrid, instead of using the shape position
            // we will use the child center of square position, to calculate our position to the walls and bottom
            // this one does the opposite angle, reverses itself, from the 90 it did previously
            if (!IsInGrid())
            {
                transform.Rotate(0, 0, -90);
            }
            // T10 otherwise call UpdateGameBoard() to update it while tracking the movements
            else
            {
                UpdateGameBoard();

                // T12 play a sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
            }
        }

        // T10 copied from the 'w' key function, to rotate the opposite way
        if (Input.GetKeyDown("e"))
        {
            // T9 Then we want the position, to change it, to rotate -90 degrees, for our Shape
            // it will know we mean the Shape object
            transform.Rotate(0, 0, -90);

            // we know we want to prevent the shape from leaving the play area,
            // but do not know how, so we will instead get a read out onthe screen,
            // of our transport. position, to aid us in figuring it out
            //Debug.Log(transform.position);

            // T9 we have to change this, to use our IsInGrid, instead of using the shape position
            // we will use the child center of square position, to calculate our position to the walls and bottom
            // this one does the opposite angle, reverses itself, from the -90 it did previously
            if (!IsInGrid())
            {
                transform.Rotate(0, 0, 90);
            }
            // T10 otherwise call UpdateGameBoard() to update it while tracking the movements
            else
            {
                UpdateGameBoard();

                // T12 play a sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
            }
        }
    }

    // T9 cycle through all the child object shapes, 
    // and check the central positions for those child objects with childCount
    public bool IsInGrid()
    {
        // keep in mind how we will debug the child count var
        int childCount = 0;

        // T9 create the child cube for the shapes, transfrom is for the child cubes position
        // of the total shape in a cube child, the center mark of our shape
        foreach (Transform childBlock in transform)
        {
            // T10 implemented RoundVector() function to round our position up
            Vector2 vect = RoundVector(childBlock.position);

            // T9 increment child count for debugging purposes
            childCount++;

            // T9 written to Unity console: the childCount name and its chilc block position
            //Debug.Log(childCount + " " + childBlock.position);

            // T9 if its NOT the, passed in vect (childBlock position)
            if (!IsInBorder(vect))
            {
                return false;
            }

            // T10 see if grid cell space is empty and available, for the shapes cube
            // convert float positions to ints(does NOT work) because they have decimal points that need to be rounded off
            // we will create a function that rounds the floats.
            // gameboard is equal to having something in it, not null, and is equal to having a transport in it.
            // we return a false, yes it has something there
            if (GameBoard.gameBoard[(int)vect.x, (int)vect.y] != null &&
                GameBoard.gameBoard[(int)vect.x, (int)vect.y].parent != transform)
            {
                // T10 return false, but now we have to update our gameBoard, with a function
                return false;
            }
        }
        // T9 if it is the childBlock position
        return true;
    }

    // T10 add function to round up and convert float values into int(s), gets vector2 passed in
    // We will implement this function wherever we create one of these Vector2 vectors.
    public Vector2 RoundVector(Vector2 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    // T9 figure out weather shape is within the borders or not
    // true or false if shape position is within the x left border, x right border and y bottom border
    public static bool IsInBorder(Vector2 pos)
    {
        // T9 We need to subract or add 1 from the data we got for the two x and y position earlier,
        // because the positions reflect the center of the borders, not the inside of the borders
        //return ((int)pos.x >= -4.5 && (int)pos.x <= 4.5 && (int)pos.y >= -9.4);
        //return ((int)pos.x >= -3.5 && (int)pos.x <= 3.5 && (int)pos.y >= -8.4);
        // T9 changed once againt to use our setting for the left right and bottom walls, as they are in the setting
        //return ((int)pos.x >= -4.5 && (int)pos.x <= 4.5 && (int)pos.y >= -9.4);
        // T10 after putting all items into GameObjectGroup, and moving the entire game over x = 4.5 and y = 9.4, 
        // these are the new setting to try, since everything is centered.
        //return ((int)pos.x >= 0 && (int)pos.x <= 8 && (int)pos.y >= 0);
        // T10 after adding our RoundVector() function to round floats up we have to change the RightWall, 
        // x position value, one from 8 to 9
        return ((int)pos.x >= 0 && (int)pos.x <= 9 && (int)pos.y >= 0);

        // T9 IMPORTANT Now if you run the game, You will notice that the central point of each shape is not in the same location,
        // this makes, certain shapes not be at the wall edge, because they are not shaped the same,
        // So we will make each shape, belong in a virtual cube, around the shape, to be able to center properly, in respect to the walls,
        // in FUNCTION IsInGrid()
    }

    // T10 Update the gameBoard
    public void UpdateGameBoard()
    {
        // T10 cycle through all the different blocks, inside of our array, and check if there are null values or transforms of these blocks
        // as the blocks move down the simulated array, then put null, where the blocks used to be, inside of the array
        // 20 is the vertical depth of simulated array grid, 10 is the width of it. itterate though our y columns and x rows
        for (int y = 0; y < 20; ++y)
        {
            for (int x = 0; x < 10; ++x)
            {
                // T10 check if our gameBoaeds x and y cells are empty or not, in our array
                if (GameBoard.gameBoard[x, y] != null && GameBoard.gameBoard[x, y].parent == transform)
                {
                    // T10 if x, y equals transform, that means the shape is moving down, 
                    // so remove it, from where it was previously, in the GameBoard
                    GameBoard.gameBoard[x, y] = null;
                }
            }
        }

        // T10 cycle through all the transforms, and get all the individual child blocks for each transform
        foreach (Transform childBlock in transform)
        {
            // T10 child block positions
            // T10 implemented RoundVector() function to round our position up
            Vector2 vect = RoundVector(childBlock.position);


            // T10 put our cube in our gameBoard array
            GameBoard.gameBoard[(int)vect.x, (int)vect.y] = childBlock;

            // T10 monitor this with the console
            //Debug.Log("Cube At: " + (int)vect.x + " " + (int)vect.y);

            // T10 now we will use this function to itterate in all the places, 
            // where we move the shapes, using UpdateGameBoard() by calling it          
        }
        // T10 update our gameboard with printed X's and N's, by calling PrintArray()
        // T11 no longer needed for debugging so commented out., uncomment out, to see grid array representation of gameplay area
        //GameBoard.PrintArray();
    }

    // T11 add points to the text score, when bottom row is full of transforms and therefor deleted
    // we will have to get the name(Score) of the text UI component, to add to the score
    // Increases the score the text UI 
    void IncreaseTextUIScore()
    {
        // Find the matching text UI component
         var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        // Get the string stored in it and convert to an int
        score = int.Parse(textUIComp.text);

        // pass the score between scenes using a static var that belongs to the class
        textUIComp2 = score;

        // Increment it
        score+=10;

        // Save new score in Text UI
        textUIComp.text = score.ToString();  

        Debug.Log("score" + score.ToString());
        Debug.Log("textUIComp2 " + textUIComp2.ToString());

        
    }
    void IncreaseTextUIScore2()
    {
        // Find the matching text UI component
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        // Get the string stored in it and convert to an int
        score = int.Parse(textUIComp.text);

        // pass the score between scenes using a static var that belongs to the class
        textUIComp2 = score;

        // Increment it
        score += 2;

        // Save new score in Text UI
        textUIComp.text = score.ToString();

        Debug.Log("score two" + score.ToString());


    }
}
