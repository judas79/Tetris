using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DropdownControl : MonoBehaviour
{
    // string list the alphabet, a space, and special characters to populate our dropdowns with
    List<string> letters = new List<string> { "~~", "A.", "B.", "C.", "D.", "E.", "F.", "G.", "H.", "I.", "J.", "K.", "L.", "M.", "N.", "O.", "P.", "Q.", "R.", "S.", "T.", "U.", "V.", "W.", "X.", "Y.", "Z.", "_", "-", "Sr.", "Jr.", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };

    // reference our Dropdown object
    public Dropdown dropdown;

    // 
    public Text SelectedNames;

    // reference our initial UI text labels
    GameObject initialOne;
    GameObject initialTwo;
    GameObject initialThree;

    public Text textInitials1;
    public Text textInitials2;
    public Text textInitials3;

    // reference to our goup of objects, so we can disable them if they are not to be used,
    // then enable the objects if the players score is greater than the current high score
    public GameObject EnterInitialsGroup;

    // since the list is based on the index number defining the letter
    // we will create a way of assigning the index number to a letter at the event of chosing a character from the list
    public void DropDown_IndexChanged(int index)
    {
        // get the chosen letter from the indext number that represents it
        SelectedNames.text = letters[index];
    }

    // run when it first starts
    void Start()
    {
        // calls DisableEnterInitialsGroup, so it can disable our EnterInitialsGroup, 
        // after starting and getting the stored PlayerPrefs and loading the  letters into our 3 initials pickers
        Invoke("DisableEnterInitialsGroup", .5f);

        // call populateList to populate our dropdown boxes with letters A - Z and an empty space _.
        PopulateList();
        
    }
    
    // loads the 3 dropdowns, with the letters and characters, the user will use to enter initials with
    void PopulateList()
    {
        // // gets rid of error - UnassignedReferenceException: 
        if (dropdown)
        {
            // refers to the AddOptions, where the letters and symbols are loaded into
            dropdown.AddOptions(letters);
        }
        
    }

    // enables or disables a group of objects used to enter a user initials, if the high score is surpassed
    private void DisableEnterInitialsGroup()
    {
        // gets rid of error - UnassignedReferenceException: The variable EnterInitialsGroup of DropdownControl has not been assigned.
        if (EnterInitialsGroup)
        {
            // disables the group of object used to enter user initials if a new high score is achieved
            EnterInitialsGroup.SetActive(false);

            // gets the high score stored in storedHighScore on computer
            int highScore = PlayerPrefs.GetInt("storedHighScore");
            Debug.Log("High Score: " + highScore.ToString());

            // gets the users current score, being passed from the gameplay to the GameOver scene
            int score = PlayerPrefs.GetInt("passScore");
            Debug.Log("High Score: " + score.ToString());

            // if the users score is higher than the current high score, 
            // enable the objects in our EnterInitialsGroup, so the user can enter, initials
            if (score >= highScore)
            {
                EnterInitialsGroup.SetActive(true);
/*
                FindInitialsComp();
                if (textInitials1)
                {
                    textInitials1.gameObject.SetActive(true);
                }
                if (textInitials2)
                {
                    textInitials2.gameObject.SetActive(true);
                }
                if (textInitials3)
                {
                    textInitials3.gameObject.SetActive(true);
                }  
                */
            }
        }       
    }
 
    // add all three initials to PlayerPrefs when SubmitInitials is pressed
    public void StoreUserInitials()
    {
        Debug.Log("StoreUserInitials event called");
        
        // deletes the current initials, that is stored in the  Playerprefs memory
        PlayerPrefs.DeleteKey("storedInitials1");
        PlayerPrefs.DeleteKey("storedInitials2");
        PlayerPrefs.DeleteKey("storedInitials3");

        Debug.Log("StoreUserInitials event called, before the if");
        

        // called to find all three initial text labels,
        // store on computer, whatever the user has chosen using dropdowns,
        // and is being stored in the three initials text labels
        FindInitialsComp();

        if (textInitials1)
        {
            textInitials1.gameObject.SetActive(true);
            PlayerPrefs.SetString("storedInitials1", textInitials1.text.ToString());
        }
        if (textInitials2)
        {
            textInitials2.gameObject.SetActive(true);
            PlayerPrefs.SetString("storedInitials2", textInitials2.text.ToString());
        }
        if (textInitials3)
        {
            textInitials3.gameObject.SetActive(true);
            PlayerPrefs.SetString("storedInitials3", textInitials3.text.ToString());
        }       

        string test = PlayerPrefs.GetString("storedInitials1".ToString());

        // for debugging code
        Debug.Log("Initials: " + PlayerPrefs.GetString("storedInitials1".ToString()) + PlayerPrefs.GetString("storedInitials2".ToString()) + PlayerPrefs.GetString("storedInitials3".ToString()));

      

        // Get the button, SubmitInitials and create instance, button
        var button = GameObject.Find("SubmitInitials").GetComponent<Button>();

        // disable button, so user cannot click it twice
        button.enabled = false;

        // make button invisible by setting it to not active
        //button.gameObject.SetActive(false);

        
    }
 

    private void Awake()
    {
        // find our SubmitInitials button when it is pressed
        var button = GameObject.Find("SubmitInitials").GetComponent<Button>();
        button.enabled = true;

        // call to get stored initials and display them
        GetDisplayInitials();
        
    }

    // get stored initials and display them in initial text labels
    void GetDisplayInitials()
    {
        // in the event that there hasn't been anything stored yet in storedHighScore, averts the potential error, for value doesn't exist
        if (PlayerPrefs.HasKey("storedInitials1"))
        {
            if (GameObject.Find("Initial1").GetComponent<Text>())
            {
                var initial1 = GameObject.Find("Initial1").GetComponent<Text>();

                // set our int highScore to our high score playerprefs stored value
                string firstInitial1 = PlayerPrefs.GetString("storedInitials1".ToString());
                initial1.text = firstInitial1.ToString();
                if (initial1)
                {
                    initial1.GetComponent<Text>().enabled = true;
                }
            }
        }
        if (PlayerPrefs.HasKey("storedInitials2"))
        {
            if (GameObject.Find("Initial2").GetComponent<Text>())
            {
                var initial2 = GameObject.Find("Initial2").GetComponent<Text>();

                // set our int highScore to our high score playerprefs stored value
                string firstInitial2 = PlayerPrefs.GetString("storedInitials2".ToString());
                initial2.text = firstInitial2.ToString();
                if (initial2)
                {
                    initial2.GetComponent<Text>().enabled = true;
                }
            }
        }
        if (PlayerPrefs.HasKey("storedInitials3"))
        {
            if (GameObject.Find("Initial3").GetComponent<Text>())
            {
                var initial3 = GameObject.Find("Initial3").GetComponent<Text>();

                // set our int highScore to our high score playerprefs stored value
                string firstInitial3 = PlayerPrefs.GetString("storedInitials3".ToString());
                initial3.text = firstInitial3.ToString();
                if (initial3)
                {
                    initial3.GetComponent<Text>().enabled = true;
                }
            }
        }
    }

    // find all three initial text labels
    void FindInitialsComp()
    {
        
        // finds our 3 initials in each dropdown and gets the text stored in them
        if (GameObject.Find("Initial1"))
        {
            textInitials1 = GameObject.Find("Initial1").GetComponent<Text>();
            
        }
        if (GameObject.Find("Initial2"))
        {
            textInitials2 = GameObject.Find("Initial2").GetComponent<Text>();
            
        }
        if (GameObject.Find("Initial3"))
        {
            textInitials3 = GameObject.Find("Initial3").GetComponent<Text>();
            
        }
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
