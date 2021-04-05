using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player3D : MonoBehaviour
{
    private float horzMovement;
    private float vertMovement;
    private Rigidbody playerRB;
    private BoxCollider playerCol;
    private Renderer playerRend;
    private ShootingController playerShoot;
    
    [Header("Movement")]
    public float playerSpeed = 3.0f; //Change speed in inspector
    public float jumpForce = 10.0f; //Change jump force in inspector
    public LayerMask groundLayer;
    
    private Vector3 currentCheckpoint;
    private Vector3 jumpBounds;
    private float jumpSpeed;

    public bool stopJumping;

    [Header("Bools")]
    public bool alterMovement;
    public bool stickyHor; //Use when the player is on the horizontal sticky floor

    [Header("Colors")]
    public Material currentColor;
    private int currentColorMode = 0; //Defaults player Color Mode to Greyscale

    //This texture will be used during the mid-game, which we may need an array to change into different textures
    public Material[] inkyMaterials = new Material[4];
    //private int randomTexture = 0; //Use for randomly selecting textures

    public bool canDestroyFloor; //Destroy a specific floor

    [Header("Checkpoint UI")]
    public TextMeshProUGUI cpText; //Will use to update checkpoint UI
    public TextMeshProUGUI curBristles; //Holds current amount of bristles
    public int bristles = 0; //Amount of bristles

    [Header("Current/New Color UI")]
    public Image curColor; //Will be used to update the player's current color
    public Color[] uiColors = new Color[4]; //Changes the current color image from the player's current color 
    private int colorCurUI = 0; //Use for changing the current color UI
    private int colorNextUI = 0; //Use to change the next color UI
    public Image colorSwap; //Will be used to show which color will be next
    //public List <Color> colorInventory = new List<Color>(); //Store colors in here to see the next color to switch to

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCol = GetComponent<BoxCollider>();
        playerRend = GetComponent<Renderer>();
        playerShoot = GetComponent<ShootingController>();
        
        playerRend.material = inkyMaterials[0]; //Sets player's starting material to Greyscale
        currentCheckpoint = transform.position; //Sets player's first checkpoint to player location on start up
        canDestroyFloor = false;

        cpText.text = "Checkpoint: Not Met!";

        alterMovement = false;
        stickyHor = false;
        stopJumping = false;
    }

    void Update()
    {
        //Player directional input
        horzMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");

        //Press spacebar to jump if the player can jump
        if (stopJumping == false)
        {
            if(IsGrounded() && (Input.GetKeyDown(KeyCode.Space)))
            {
                //Player has normal jumping when the player is not on a sticky horizontal floor
                if (stickyHor == false)
                {
                    playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }

                //playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                else if (stickyHor == true)
                {
                    playerRB.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
                }
            
                //Debug.Log(Vector3.up * jumpForce);        
            }
        }

        //Press shift to change colors and the projectile type
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentColorMode = playerShoot.CheckNextAvailableColor(currentColorMode);
            ChangeMaterial(currentColorMode);
            playerShoot.ChangeProjType(currentColorMode);
            //colorCurUI = pUpdateCurColorUI(colorCurUI);
            UpdateCurColorUI(currentColorMode);
            //CheckForNextColorUI(currentColorMode);
            //ColorSwaping(currentColorMode);
            //colorSwap.color = colorInventory[currentColorMode];
            colorNextUI = playerShoot.CheckNextAvailableColor(colorNextUI);
            ChangeNewColorUI(colorNextUI);
        }

        //Make sure there is the amount of collected bristles
        curBristles.text = "Bristles Collected: " + bristles.ToString();

        //Update the color UI icon when the player's color changes AND eventually when the player chooses which color to shoot with
        //curColor.color = currentColor.color;
        //UpdateCurColorUI(currentColorMode);
    }

    void FixedUpdate()
    {
        //Player movement
        
        Vector2 position = playerRB.position;
        
        //The player will move normally in the game
        if (alterMovement == false)
        {
            position.x = position.x + playerSpeed * horzMovement * Time.deltaTime;
            playerRB.MovePosition(position);
            //Old movement code: transform.Translate(new Vector3(horzMovement, 0 , 0) * Time.deltaTime * playerSpeed);
        }

        //The movement changes when the player is on the wall
        else if (alterMovement == true || stickyHor == true)
        {
            transform.Translate(new Vector3(horzMovement, vertMovement, 0) * Time.deltaTime * playerSpeed);
        }
    }

    //Ground check function
    public bool IsGrounded()
    {
        //Normal jumping
        if (alterMovement == false)
        {
            Vector3 ogCol = new Vector3(playerCol.bounds.center.x, playerCol.bounds.min.y, playerCol.bounds.center.z);
            jumpBounds = ogCol;
            jumpSpeed = 0.9f;
        }

        //Alternative jumping if the player is on a sticky wall
        else if (alterMovement == true)
        {
            Vector3 wallPlayerCol = new Vector3(playerCol.bounds.center.y, playerCol.bounds.min.x, playerCol.bounds.center.z);
            jumpBounds = wallPlayerCol;
            jumpSpeed = 0.5f;
        }

        //Alternative jumping if the player is on the sticky horizontal floor
        else if (stickyHor == true)
        {
            Vector3 horCol = new Vector3(playerCol.bounds.center.x, -playerCol.bounds.min.y, playerCol.bounds.center.z);
            jumpBounds = horCol;
            jumpSpeed = 0.9f;
        }

        //Return jumping result using a box centered at the bottom of the player's collider and whose dimensions are slightly smaller than the collider's x axis size, and is 0.1 units in the y and z
        return Physics.CheckBox(new Vector3(playerCol.bounds.center.x, playerCol.bounds.min.y, playerCol.bounds.center.z), new Vector3 (playerCol.bounds.size.x * 0.4f, 0.1f, 0.1f), Quaternion.identity, groundLayer);
    }

    //Set checkpoint
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
        Debug.Log("Checkpoint set to: " + currentCheckpoint);
    }

    //Reset player position to last checkpoint
    public void ResetToCheckpoint()
    {
        Debug.Log("Died.");
        transform.position = currentCheckpoint;
    }

    //Update the checkpoint UI when the player reaches a specific checkpoint
    public void UpdateCPUI(int currentCP)
    {
        cpText.text = "Checkpoint: " + currentCP.ToString();
    }

    //Use UI indicator to show the next color to change to switch between 4 colors in a specific predetermined order and loop around
    public void ChangeNewColorUI(int nColor)
    {
        colorNextUI = nColor;

        switch(nColor)
        {
            case 0:
                colorSwap.color = uiColors[colorNextUI];
                break;
            case 1:
                colorSwap.color = uiColors[colorNextUI];
                break;
            case 2:
                colorSwap.color = uiColors[colorNextUI];
                break;
            case 3:
                colorSwap.color = uiColors[colorNextUI];
                break;
            default:
                colorSwap.color = uiColors[colorNextUI];
                break;
        }
    }

    //Increase the amount of bristles
    public void AddBristles(int br)
    {
        bristles += br;
    }

    //Change between materials
    public void ChangeMaterial(int switchingToColorMode)
    {
        currentColorMode = switchingToColorMode;
        playerRend.material = inkyMaterials[switchingToColorMode];
    }

    //Update the current color UI based on the current color of the player
    public void UpdateCurColorUI(int newColor)
    {
        colorCurUI = newColor;
        curColor.color = uiColors[colorCurUI];
    }

    private void OnCollisionEnter(Collision slow)
    {
        //When the player is on the specific floor, the platform will shortly disappear 
        if (slow.gameObject.tag == "SlowBreak")
        {
            canDestroyFloor = true;
        }

        //The player's movement changes when they are on the vertical sticky wall
        if (slow.gameObject.tag == "VerClimbable")
        {
            alterMovement = true;
            playerRB.velocity = Vector3.zero; //Prevents player from moving by itself
            playerRB.useGravity = false;
        }

        //The player's movement changes when they are on the horizontal sticky wall
        if (slow.gameObject.tag == "HorClimbable")
        {
            //alterMovement = true;
            stickyHor = true;
            playerRB.velocity = Vector3.zero; //Prevents player from moving by itself
            playerRB.useGravity = false;
        }
    }

    //The player's movement changes back to normal when they are off the sticky wall
    private void OnCollisionExit(Collision back)
    {
        if (back.gameObject.tag == "VerClimbable")
        {
            alterMovement = false;
            playerRB.useGravity = true;
        }

        if (back.gameObject.tag == "HorClimbable")
        {
            //alterMovement = false;
            stickyHor = false;
            playerRB.useGravity = true;
        }
    }

    //Use it for separate function desc- Update the UI that displays the player's current color AND eventually when the player chooses which color to shoot with
    //Use UI indicator to show the next color to change to
    //Switch between 4 colors from shift in a specific predetermined order and loop around
    // public void ColorSwaping(int nextColor)
    // {
    //     //colorNextUI = nextColor;
    //     //colorSwap.color = uiColors[colorNextUI];

    //     // colorInventory[colorNextUI] = uiColors[nextColor];

    //     // colorSwap.color = colorInventory[colorNextUI];

    //     // colorInventory.Add(uiColors[nextColor]);
    //     //colorNextUI = nextColor;
    //     //colorInventory[colorNextUI] = uiColors[nextColor];
    //     // colorSwap.color = colorInventory[nextColor];

    //     colorNextUI = nextColor;
    //     colorSwap.color = uiColors[colorNextUI];
    // }

    //Check when the next color UI can change
    // public int CheckForNextColorUI(int nColor)
    // {
    //     for (int c = 0; c < uiColors.Length; c++)
    //     {
    //         //playerController.colorSwap.color = playerController.colorInventory[nColor + 1];
    //         //nColor += c;
    //         // playerController.uiColors[c];
    //         //nColor = c;

    //         if (c == 0) 
    //         {
    //             colorSwap.color = uiColors[1];
    //             Debug.Log("color: " + c);
    //         } 

    //         else if (c == 1)
    //         {
    //             colorSwap.color = uiColors[2];
    //             Debug.Log("color: " + c);
    //         }

    //         else if (c == 2)
    //         {
    //             colorSwap.color = uiColors[3];
    //             Debug.Log("color: " + c);
    //         }

    //         else if (c == 3)
    //         {
    //             colorSwap.color = uiColors[0];
    //             Debug.Log("color: " + c);    
    //         }
    //     }

    //     // foreach (var c in playerController.colorInventory)
    //     // {   
    //     //     playerController.colorInventory[c] += 1;
    //     // }
    //     return 0;
    // }

    //Test player's texture change by going through a specific cube
    // public void OnTriggerEnter(Collider form)
    // {
    //     if (form.gameObject.tag == "Appear")
    //     {
    //         //ChangeTexture();
    //         originalColor.material.SetTexture("_MainTex", inkyTexture[0]);
    //     }
    // }

    // public void OnTriggerExit(Collider form)
    // {
    //     if (form.gameObject.tag == "Appear")
    //     {
    //         //ChangeTexture();
    //         originalColor.material.SetTexture("_MainTex", inkyTexture[1]);
    //     }
    // }

    /*//Changing the player's color
    public void ChangeColor(Material paintColor)
    {
        currentColor.color = paintColor.color;
    }*/
    
    //Temporarily change Inky's texture only when they collect specific pick-ups
    //Its texture will be changed during the mid-game once we figured out the mechanic
    /*public void ChangeTexture()
    {
        //originalColor.material.mainTexture = inkyTexture[0];

        //Randomly select between the textures for Inky's texture to become
        randomTexture = Random.Range(0,2);

        //Change Inky's texture from its albedo map of its main material
        //"_MainTex" refers to Albedo (main map) in the material
        originalColor.material.SetTexture("_MainTex", inkyTexture[randomTexture]);
    }*/
}
