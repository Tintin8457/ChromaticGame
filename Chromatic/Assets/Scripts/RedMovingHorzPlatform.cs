using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the platforms to move left and right
public class RedMovingHorzPlatform : MonoBehaviour
{
    public float speed;

    [Header("Bools")]
    public bool changeDir; //Changes the direction that the platform is moving
    public bool canMoveHor; //Move platforms horizontally when a red projectile hits it
    public bool cooldown;

    [Header("Colored platform timer")]
    public float tempColorHolder;
    public float resetColorHolder;

    [Header("Cooldown timer")]
    public float coolDownTimer;
    public float resetTimer;

    public Material ogPlatColor; //Holds original color

    // Start is called before the first frame update
    void Start()
    {
        canMoveHor = false;

        if (canMoveHor == true)
        {
            changeDir = true;
        }

        cooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Moving platforms only happen when a red projectile hits them for a limited time
        if (canMoveHor == true)
        {
            tempColorHolder -= Time.deltaTime; //Start timer

            //The platform can move left
            if (changeDir == true)
            {
                transform.Translate(-Time.deltaTime * speed, 0, 0, Space.World);
            }

            //The platform can move right
            else if (changeDir == false)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
            }
        }

        //Stop timer and reset color
        if (tempColorHolder <= 0.0f)
        {
            canMoveHor = false;
            tempColorHolder = resetColorHolder;
            gameObject.GetComponent<Renderer>().material = ogPlatColor;

            //Cool down before the player can paint it again
            cooldown = true;
        }

        //Cool down before the player can paint it again
        if (cooldown == true)
        {
            coolDownTimer -= Time.deltaTime;

            if (coolDownTimer <= 0.0f)
            {
                coolDownTimer = resetTimer;
                cooldown = false;
            }
        }
    }

    //Changes the platform's direction of movement when it triggers any of the two invisible triggers
    void OnTriggerEnter(Collider change)
    {
        if (change.gameObject.tag == "Moving")
        {
            //Changes platform's direction to the right
            if (changeDir == true)
            {
                changeDir = false;
            }
            
            //Changes platform's direction to the left
            else if (changeDir == false)
            {
                changeDir = true;
            }
        }
    }

    void OnCollisionEnter(Collision player)
    {
        //Prevent the player from falling off the moving platform
        if (player.gameObject.tag == "Player")
        {
            player.gameObject.transform.parent = gameObject.transform;
        }

        //The platforms will move once a red projectile hits them and turns them red
        if (player.gameObject.tag == "Red" && cooldown == false)
        {
            canMoveHor = true;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
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
