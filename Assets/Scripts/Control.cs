using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control : MonoBehaviour
{

    // Drag this script into the MainCamera add component settings are
    
    // if end game button clicked it will close the game
    public void Exit()
    {
        Application.Quit();
    }

    // if play Again button is clicked, a new game will begin without the splash screen
    public void PlayAgain()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("MainScene");
    }

    void ShowFinalScore()
    {

        // Find the matching text UI component from GameOver script
        //var textFSComp = GameObject.Find("GOScore").GetComponent<Text>();

        // Get our score and store in PlayerPrefs
        int textUIComp = PlayerPrefs.GetInt("textUIComp2");
        Debug.Log("textUIComp  " + textUIComp.ToString());
    }


    // Clear HighScore Button
    public void ClearHighScore()
    {
        // call awake to create an instance of our ClearHighScore button
        Awake();

        // finds our GOHighScore and gets the text stored in it
        var textHSComp = GameObject.Find("GOHighScore").GetComponent<Text>();

        // deletes the high score, that is stored in the  Playerprefs memory
        PlayerPrefs.DeleteKey("storedHighScore");

        // sets our highScore var to 0
        int highScore = 0;

        // display 0 as text, in the HigScore component label
        textHSComp.text = highScore.ToString();       
    }

    private void Awake()
    {
        // find our ClearHighScore button when it is pressed
        var Button = GameObject.Find("ClearHighScore").GetComponent<Button>();
    }
   
   
}
