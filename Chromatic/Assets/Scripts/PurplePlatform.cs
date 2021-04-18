using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePlatform : MonoBehaviour
{
    [Header("Purple Platform Properties")]
    public Vector3 ogLocation; //Stores platform's original location
    public Material purPlat;
    public BoxCollider platCollider;
    public float speed = 2.0f;

    [Header("Bools")]
    public bool canChange; //Only allows the player to shoot at this platform once
    public bool cooldown;
    public bool changedColor;
    public bool red; //Change to red once
    public bool blue; //Change to blue once

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

    [Header("Audio")]
    AudioSource platSource;
    public AudioClip activatedClip; //Play sfx when activated

    [Header("Colors")]
    public Texture[] purplePlatColors; //Set purple, red, and blue colors for purple platform events

    private ShaderBW toonShader; //Holds toon shader
    
    // Start is called before the first frame update
    void Start()
    {
        toonShader = GetComponent<ShaderBW>();

        canChange = true;
        cooldown = false;
        changedColor = false;

        horizontal = false;
        vertical = false;

        red = true;
        blue = true;

        platSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for when the purple platform can have collision when its transparency changes
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
            //gameObject.GetComponent<Renderer>().material = purPlat; //Reset original platform color
            gameObject.GetComponent<Renderer>().material.SetTexture("_Texture", purplePlatColors[0]);
            toonShader.canBePainted = true; //Reset color and transparency

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

    void OnTriggerEnter(Collider direction)
    {
        //Changes the platform's direction when it triggers one of the two transforms
        if (direction.gameObject.tag == "Moving")
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

        //Change the purple platform's color once before resetting
        //Changes to red to allow for horizontal direction one time
        if (direction.gameObject.tag == "Red" && canChange == true && red == true)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.red;
            toonShader.red = true;
            canChange = false;
            changedColor = true; //Start timer
            horizontal = true;
            gameObject.GetComponent<Renderer>().material.SetTexture("_Texture", purplePlatColors[1]);
            FlipTransforms(); //Change orientation of the two invisible transforms
            toonShader.canBePainted = false; //Make visible and colorized
            platSource.clip = activatedClip;
            platSource.Play();
            red = false; //Prevent player from turning platform into red again
        }

        //Changes to blue to allow for vertical direction one time
        else if (direction.gameObject.tag == "Blue" && canChange == true && blue == true)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.blue;
            toonShader.blue = true;
            canChange = false;
            changedColor = true; //Start timer
            vertical = true;
            gameObject.GetComponent<Renderer>().material.SetTexture("_Texture", purplePlatColors[2]);
            FlipTransforms(); //Change orientation of the two invisible transforms
            toonShader.canBePainted = false; //Make visible and colorized
            platSource.clip = activatedClip;
            platSource.Play();
            blue = false; //Prevent player from turning platform into blue again
        }
    }

    //Move player with moving platform
    void OnCollisionEnter(Collision changeColor)
    {
        if (changeColor.gameObject.tag == "Player")
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
