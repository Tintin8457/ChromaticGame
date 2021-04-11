using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
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
