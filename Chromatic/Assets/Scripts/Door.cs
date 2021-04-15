using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Button activatedButton; 
    private Player3D inkyBristles;
    public float openSpeed;

    [Header("Open Bools")]
    public bool open;
    public bool canOpen;

    [Header("Enter Bristle Amount")]
    public int bristleNum;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        canOpen = false;

        //Find and get button
        GameObject but = GameObject.FindGameObjectWithTag("Button");

        if (but != null)
        {
            activatedButton = but.GetComponent<Button>();
        }

        //Find and get player
        GameObject inky = GameObject.FindGameObjectWithTag("Player");

        if (inky != null)
        {
            inkyBristles = inky.GetComponent<Player3D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //The door will check if the player has a specific amount of bristles before opening, which is dependent on how many are in the current stage
        if (activatedButton.canPress == false && open == false)
        {
            //The door can open after a required amount
            if (inkyBristles.bristles >= bristleNum)
            {
                open = true;
                canOpen = true;
                activatedButton.UpdateText(bristleNum);
                activatedButton.canPress = false;
            }

            //The door cannot open after getting less than the required amount
            else if (inkyBristles.bristles < bristleNum)
            {
                activatedButton.UpdateText(bristleNum);
                activatedButton.canPress = true;
            }
        }

        //Open door
        else if (open == true && canOpen == true)
        {
            transform.Rotate(new Vector3(0f, -Time.deltaTime * openSpeed), Space.World);
            //Debug.Log("cur: " + transform.localRotation.eulerAngles.y);

            //Stop from opening
            if (transform.rotation.y <= 0f)
            {
                canOpen = false;
            }
        }
    }
}

//Open door
// if (open == true)
// {
//     //transform.localRotation = new Quaternion(0f, -Time.deltaTime * openSpeed, 0f, 0f);
//     transform.Rotate(new Vector3(0f, -Time.deltaTime * openSpeed), Space.World);

//     //Stop from opening
//     if (transform.localRotation.eulerAngles.y <= 0f)
//     {
//         open = false;
//     }
// }

// transform.Rotate(new Vector3(0f, -Time.deltaTime * openSpeed), Space.World);
// //Debug.Log("cur: " + transform.localRotation.eulerAngles.y);

// //Stop from opening
// if (transform.rotation.y <= 0f)
// {
//     open = false;
// }