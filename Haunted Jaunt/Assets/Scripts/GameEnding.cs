using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    //declarations
    public float fadeDuration = 1f; //amount of time for the fade
    public float displayImageDuration = 1f; //displays images for set # of frames
    public GameObject player; //call player game object

    public CanvasGroup exitBackgroundImageCanvasGroup; //call winning exit image
    public AudioSource exitAudio; //call winning exit audio

    public CanvasGroup caughtBackgroundImageCanvasGroup; //call losing exit image
    public AudioSource caughtAudio; //call losing exit audio

    float m_Timer; //timer to ensure game doesn't quit before screen has faded
    bool m_HasAudioPlayed; //boolean to determine if audio has played

    bool m_IsPlayerAtExit; //boolean to determine if player is at exit
    bool m_IsPlayerCaught; //boolean to determine if player is caught by enemy

    //method to determine if player is at the exit
    void OnTriggerEnter(Collider other)
    {
        //ensures that ending is only triggered by player colliding with exit
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true; //sets player at exit
        }
    }

    //method to set if player is caught
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    //update method updates regularly as game is running
    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }

        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    //method to fade the screen out & quits game
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        //play won/caught audio if not
        if (!m_HasAudioPlayed)
        {
            audioSource.Play(); //play audio
            m_HasAudioPlayed = true; //set boolean so audio only plays once
        }

        m_Timer += Time.deltaTime; //starts timer
        imageCanvasGroup.alpha = m_Timer / fadeDuration; //fades images

        //sets the game exit after the fade has finished
        if(m_Timer > fadeDuration + displayImageDuration)
        {
            //if caught, restart the game
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }

            //if won, exit game
            else
            {
                Application.Quit();
            }
        }
    }
}
