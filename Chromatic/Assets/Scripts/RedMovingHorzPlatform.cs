using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the platforms to move left and right
public class RedMovingHorzPlatform : MonoBehaviour
{
    [Header("Red Platform Properties")]
    public float speed;
    public BoxCollider platCollider;

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
    private ShaderBW toonShader; //Holds toon shader

    // Start is called before the first frame update
    void Start()
    {
        toonShader = GetComponent<ShaderBW>();
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
        //Check for when the blue platform can have collision when its transparency changes
        //Disable collision
        if (toonShader.canBePainted == true)
        {
            platCollider.isTrigger = true;
        }

        //Enable collision
        else if (toonShader.canBePainted == false)
        {
            platCollider.isTrigger = false;
        }

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

        //Stop timer and reset color/transparency
        if (tempColorHolder <= 0.0f)
        {
            canMoveHor = false;
            tempColorHolder = resetColorHolder;
            gameObject.GetComponent<Renderer>().material = ogPlatColor;
            toonShader.canBePainted = true;

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

    void OnTriggerEnter(Collider change)
    {
        //Changes the platform's direction of movement when it triggers any of the two invisible triggers
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

        //The platforms will move once a red projectile hits them and turns them red
        if (change.gameObject.tag == "Red" && cooldown == false)
        {
            canMoveHor = true;
            toonShader.canBePainted = false; //Make visible and colorized
            gameObject.GetComponent<Renderer>().material.color = Color.red;
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
