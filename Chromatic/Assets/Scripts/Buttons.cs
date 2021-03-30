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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Prototyping"))
        {
            SceneManager.LoadScene("Prototyping");
        }

        //For level designers and eventually the final look of the game?
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ChromaticVertSlice"))
        {
            SceneManager.LoadScene("ChromaticVertSlice");
        }
    }

    //Exit game
    public void Quit()
    {
        Application.Quit();
    }
}
