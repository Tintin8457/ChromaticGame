using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    //Restart level
    public void Restart()
    {
        SceneManager.LoadScene("3D Prototype");
    }

    //Exit game
    public void Quit()
    {
        Application.Quit();
    }
}
