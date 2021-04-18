using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject resumePopUp;

    void Start()
    {
        resumePopUp.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        resumePopUp.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        resumePopUp.SetActive(false);
    }

    //Restart level
    public void Restart()
    {
        //For programmers
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Prototype"))
        {
            SceneManager.LoadScene("Prototype");
        }

        //For level designers and eventually the final look of the game?
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ChromaticVertSlice"))
        {
            SceneManager.LoadScene("ChromaticVertSlice");
        }

        //For the white box
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ChromaticWhiteBox"))
        {
            SceneManager.LoadScene("ChromaticWhiteBox");
        }
    }

    //Exit game
    public void Quit()
    {
        Application.Quit();
    }
}
