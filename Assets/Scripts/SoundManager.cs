using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // T12 We only want to ever have 1 SoundManagers so we will make it a singleton;
    // to set up that only one sound manager can be created
    public static SoundManager Instance = null;

    // T12 provide a way for the user, to set up thier audioclip sounds, in the SoundManager settings
    public AudioClip rotateSound;
    public AudioClip rowDelete;
    public AudioClip ShapeMove;
    public AudioClip ShapeStop;
    public AudioClip gameOver;

    // T12 refer to the audio source thats going to be added to the sound manager to play the sound effects
    private AudioSource soundEffectAudio;

    // Start is called before the first frame update
    void Start()
    {
        // T12 this is the singleton area, to make sure there is only 1 soundManager, if another one shows up, destroy it
        // if, we are trying to create an instance of the sound manager, check if it is null(instance = this)
        // else if it isn't equal to null(this), destroy the version of the gameOject(soundmanager) being created
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        // T12 get our audio source, and set it to theSource
        AudioSource theSource = GetComponent<AudioSource>();
        soundEffectAudio = theSource;
    }

    // T12 called every time we play an audio clip
    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
