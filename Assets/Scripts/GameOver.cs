using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public int passScore;
    int score;
    int highScore;
    
    // Starts when program is first started
    void Awake()
    {
        // in the event that there hasn't been anything stored yet in storedHighScore, averts the potential error, for value doesn't exist
        if (PlayerPrefs.HasKey("storedHighScore"))
        {
            // set our int highScore to our high score playerprefs stored value
            highScore = PlayerPrefs.GetInt("storedHighScore");

            // gets the GOHighScore component and creates an instance, so we can diplay our high score in the GOHighScore text label
            var textHSComp = GameObject.Find("GOHighScore").GetComponent<Text>();

            // write the stored High Score to the GOHighScore label, using instance highScore to write to insace object thextHSComp as text a string
            textHSComp.text = highScore.ToString();
        }       
    }

    // Update is called once per frame
    void Update()
    {
        // if escape key is pressed the game end abruptly
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // calls the score and high score, at end of game
        ShowFinalScore();
    }
    
    int i =  0;
    void ShowFinalScore()
    {
        // only allow if statement to run one time per game
        if(i == 0)
        {
            // Find the matching text UI score displaying component objects, from GameOver script
            var textFSComp = GameObject.Find("GOScore").GetComponent<Text>();
            var textHSComp = GameObject.Find("GOHighScore").GetComponent<Text>();

            // gets the stored high score
            //highScore = PlayerPrefs.GetInt("StoredHScore");
            //Debug.Log("HScore " + HScore.ToString());


            //Debug.Log("GO " + textFSComp.text.ToString());
            score = PlayerPrefs.GetInt("passScore");

            // Save new score in Text UI
            textFSComp.text = score.ToString();

            // up i to close if statement
            i++;          

            //PlayerPrefs.SetInt("StoredHScore", score);
            // call to update high score
            UpdatHighScore();
            textHSComp.text = highScore.ToString();
        }

    }

    // keep track of the games high score
    void UpdatHighScore()
    {
        // if the current score is higher than the high score, the score will become the new high score
        if (score > highScore)
        {
            highScore = score;

            // write the new high score to memory, for non volitile storage, so we can use it after the gme has been terminated, then started again
            PlayerPrefs.SetInt("storedHighScore", highScore);
        }
    }

}
