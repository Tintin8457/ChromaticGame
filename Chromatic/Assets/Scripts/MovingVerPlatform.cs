using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the platforms to move up and down
public class MovingVerPlatform : MonoBehaviour
{
    public float speed;
    public bool changeDir; //Changes the direction that the platform is moving

    // Start is called before the first frame update
    void Start()
    {
        changeDir = true;
    }

    // Update is called once per frame
    void Update()
    {
        //The platform can move down
        if (changeDir == true)
        {
            transform.Translate(0, -Time.deltaTime * speed, 0, Space.World);
        }

        //The platform can move up
        else if (changeDir == false)
        {
            transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
        }
    }

    //Changes the platform's direction of movement when it triggers any of the two invisible triggers
    void OnTriggerEnter(Collider change)
    {
        if (change.gameObject.tag == "Moving")
        {
            //Changes platform's direction to upwards
            if (changeDir == true)
            {
                changeDir = false;
            }
            
            //Changes platform's direction downwards
            else if (changeDir == false)
            {
                changeDir = true;
            }
        }
    }

    //Prevent the player from falling off the moving platform
    void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == "Player")
        {
            player.gameObject.transform.parent = gameObject.transform;
        }
    }

    //The platform should no longer control the player's movement
    void OnCollisionExit(Collision player)
    {
        if (player.gameObject.tag == "Player")
        {
            player.gameObject.transform.parent = null;
        }
    }
}
