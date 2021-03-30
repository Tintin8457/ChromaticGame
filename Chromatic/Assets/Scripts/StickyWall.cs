﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWall : MonoBehaviour
{
    public Material ogWallColor; //Stores original wall color
    public GameObject climbablePart;
    private Player3D player;
    public string stickyIdentity; //Stores the sticky wall type string: horziontal and vertical

    [Header("Bools")]
    public bool isSticky;
    public bool cooldown;

    [Header("Timer")]
    public float stickable;
    public float resetStickable;

    [Header("Cooldown")]
    public float coolDownTimer;
    public float resetCooldown;

    // Start is called before the first frame update
    void Start()
    {
        isSticky = false;
        cooldown = false;

        //Find and get player
        GameObject inky = GameObject.FindGameObjectWithTag("Player");

        if (inky != null)
        {
            player = inky.GetComponent<Player3D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Start the countdown that the player can be on the wall
        if (isSticky == true)
        {
            stickable -= Time.deltaTime;

            //Reset timer and wall color
            if (stickable <= 0.0f)
            {
                stickable = resetStickable;
                gameObject.GetComponent<Renderer>().material = ogWallColor;
                climbablePart.tag = "Untagged";
                climbablePart.layer = 0;

                // gameObject.tag = "Untagged";
                // gameObject.layer = 0;

                //Check if the player has no gravity and alterative movement is off
                if (player.GetComponent<Rigidbody>().useGravity == false)
                {
                    player.GetComponent<Rigidbody>().useGravity = true;
                }
                
                if (player.alterMovement == true)
                {
                    player.alterMovement = false;
                }

                //Prevent horziontal mode from being on
                if (player.stickyHor == true)
                {
                    player.stickyHor = false;
                }

                cooldown = true;
                isSticky = false;
            }
        }

        //Begin cooldown
        if (cooldown == true)
        {
            coolDownTimer -= Time.deltaTime;

            //Stop cooldown
            if (coolDownTimer <= 0.0f)
            {
                coolDownTimer = resetCooldown;
                cooldown = false;
            }
        }
    }

    //Turn the wall yellow so the player can stick to it
    void OnCollisionEnter(Collision paintball)
    {
        if(paintball.gameObject.tag == "Yellow" && cooldown == false)
        {
            isSticky = true;
            gameObject.GetComponent<Renderer>().material.color = paintball.gameObject.GetComponentInChildren<Renderer>().material.color;
            climbablePart.tag = stickyIdentity;
            climbablePart.layer = 8;
            // gameObject.tag = "Climbable";
            // gameObject.layer = 8;
        }
    }
}