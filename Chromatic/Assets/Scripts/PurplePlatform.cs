using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePlatform : MonoBehaviour
{
    public Vector3 ogLocation; //Stores platform's original location
    public Material purPlat;
    public float speed = 2.0f;

    [Header("Bools")]
    public bool canChange; //Only allows the player to shoot at this platform once
    public bool cooldown;
    public bool changedColor;

    [Header("Directional Indicators")]
    public bool horizontal;
    public bool vertical;
    public bool dirChange;
    public GameObject transform1;
    public GameObject transform2;
    //private FlipIndicator switchDir;

    [Header("Timer")]
    public float timer = 10f;
    public float resetTimer = 10f;

    [Header("Cooldown Timer")]
    public float coolDownTimer = 5f;
    public float resetCooldown = 5f;

    private ShaderBW bwShader; //Holds bw shader
    
    // Start is called before the first frame update
    void Start()
    {
        bwShader = GetComponent<ShaderBW>();

        canChange = true;
        cooldown = false;
        changedColor = false;

        // if (horizontal == true || vertical == true)
        // {
        //     dirChange = true;
        // }

        horizontal = false;
        vertical = false;

        //Find and get the directional indicator script
        // GameObject orientation = GameObject.FindGameObjectWithTag("Moving");

        // if (orientation != null)
        // {
        //     switchDir = orientation.GetComponent<FlipIndicator>();
        // }
    }

    // Update is called once per frame
    void Update()
    {
        //Start platform's alternative color behavior
        if (changedColor == true)
        {
            timer -= Time.deltaTime; //Start timer

            //Move horizontally
            if (horizontal == true)
            {
                //Right
                if (dirChange == true)
                {
                    transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
                }

                //Left
                else if (dirChange == false)
                {
                    transform.Translate(-Time.deltaTime * speed, 0, 0, Space.World);
                }
            }

            //Move vertically
            else if (vertical == true)
            {
                //Up
                if (dirChange == true)
                {
                    transform.Translate(0, -Time.deltaTime * speed, 0, Space.World);
                }

                //Down
                if (dirChange == false)
                {
                    transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
                }
            }
        }

        //Reset timer and purple platform
        if (timer <= 0.0f)
        {
            timer = resetTimer; //Reset timer
            cooldown = true;
            gameObject.GetComponent<Renderer>().material = purPlat; //Reset original platform color
            bwShader.canBePainted = true; //Reset color and transparency

            //Stop platform from moving
            if (horizontal == true)
            {
                horizontal = false;
            }

            else if (vertical == true)
            {
                vertical = false;
            }

            changedColor = false;
            //gameObject.transform.position = ogLocation;
            gameObject.transform.localPosition = ogLocation; //Reset platform position
        }

        //Start cooldown
        if (cooldown == true)
        {
            coolDownTimer -= Time.deltaTime;

            //End cooldown
            if (coolDownTimer <= 0.0f)
            {
                coolDownTimer = resetCooldown;
                cooldown = false;
                canChange = true;
            }
        }
    }

    //Changes the platform's direction when it triggers one of the two transforms
    void OnTriggerEnter(Collider direction)
    {
        if(direction.gameObject.tag == "Moving")
        {
            if (dirChange == true)
            {
                dirChange = false;
            }

            else if (dirChange == false)
            {
                dirChange = true;
            }
        }
    }

    void OnCollisionEnter(Collision changeColor)
    {
        //Change the purple platform's color once before resetting
        //Changes to red to allow for horizontal direction
        if (changeColor.gameObject.tag == "Red" && canChange == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            canChange = false;
            changedColor = true; //Start timer
            horizontal = true;
            FlipTransforms(); //Change orientation of the two invisible transforms
            bwShader.canBePainted = false; //Make visible and colorized
        }

        //Changes to blue to allow for vertical direction
        else if (changeColor.gameObject.tag == "Blue" && canChange == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            canChange = false;
            changedColor = true; //Start timer
            vertical = true;
            FlipTransforms(); //Change orientation of the two invisible transforms
            bwShader.canBePainted = false; //Make visible and colorized
        }

        //Move player with moving platform
        else if (changeColor.gameObject.tag == "Player")
        {
            changeColor.gameObject.transform.parent = gameObject.transform;
        }
    }

    //Player is no longer part of moving platform
    void OnCollisionExit(Collision exit)
    {
        if (exit.gameObject.tag == "Player")
        {
            exit.gameObject.transform.parent = null;
        }
    }

    //Access the two transforms and flip them appropriately
    public void FlipTransforms()
    {
        //Flip to horizontal orientation
        if (horizontal == true)
        {
            transform1.GetComponent<FlipIndicator>().FlipToX();
            transform2.GetComponent<FlipIndicator>().FlipToX();
        }

        //Flip to vertical orientation
        else if (vertical == true)
        {
            transform1.GetComponent<FlipIndicator>().FlipToY();
            transform2.GetComponent<FlipIndicator>().FlipToY();
        }
    }
}
