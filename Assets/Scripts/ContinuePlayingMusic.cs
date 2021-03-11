using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuePlayingMusic : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        // Don't destroy the background music when a new
        // scene loads, so the music will continue playing after splashscreen closes and MainScene runs.
        DontDestroyOnLoad(gameObject);
    }
}
