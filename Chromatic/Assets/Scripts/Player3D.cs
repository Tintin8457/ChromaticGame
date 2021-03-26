using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player3D : MonoBehaviour
{
    //Fixed the jumping and cleaned up by removing old code.
    
    private float horzMovement;
    private float vertMovement;
    private Rigidbody playerRB;
    private SphereCollider playerCol;
    public float playerSpeed = 3.0f; //Change speed in inspector
    public float jumpForce = 10.0f; //Change jump force in inspector
    public LayerMask groundLayer;

    private Vector3 currentCheckpoint;

    private Renderer originalColor;
    public Material currentColor;

    //This texture will be used during the mid-game, which we may need an array to change into different textures
    public Texture[] inkyTexture;
    private int randomTexture = 0; //Use for randomly selecting textures

    public bool canDestroyFloor; //Destroy a specific floor

    public TextMeshProUGUI cpText; //Will use to update checkpoint UI

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCol = GetComponent<SphereCollider>();
        originalColor = GetComponent<Renderer>();
        currentColor.color = this.originalColor.material.color;
        currentCheckpoint = transform.position; //Sets players first checkpoint to player location on start up
        canDestroyFloor = false;
        cpText.text = "Checkpoint: Not Met!";
    }

    void Update()
    {
        //Player directional input
        horzMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical"); //Could need this later? 

        //Initial player color set
        this.originalColor.material.color = currentColor.color;

        //Press the spacebar to jump if the player can jump
        if(IsGrounded() && (Input.GetKeyDown(KeyCode.Space)))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //Debug.Log(Vector3.up * jumpForce);
        }
    }

    void FixedUpdate()
    {
        //Player movement
        transform.Translate(new Vector3(horzMovement, 0 , 0) * Time.deltaTime * playerSpeed);
    }

    //Changing the player's color
    public void ChangeColor(Material paintColor)
    {
        currentColor.color = paintColor.color;
    }

    //Ground check function
    public bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCol.bounds.center, new Vector3(playerCol.bounds.center.x, playerCol.bounds.min.y, playerCol.bounds.center.z), playerCol.radius * 0.9f, groundLayer);
    }

    //Set checkpoint
    public void setCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
        Debug.Log("Checkpoint set to: " + currentCheckpoint);
    }

    //Reset player position to last checkpoint
    public void resetToCheckpoint()
    {
        Debug.Log("Died.");
        transform.position = currentCheckpoint;
    }

    //Update the checkpoint UI when the player reaches a specific checkpoint
    public void UpdateCPUI(int currentCP)
    {
        cpText.text = "Checkpoint: " + currentCP.ToString();
    }

    //Temporarily change Inky's texture only when they collect specific pick-ups
    //Its texture will be changed during the mid-game once we figured out the mechanic
    public void ChangeTexture()
    {
        //originalColor.material.mainTexture = inkyTexture[0];

        //Randomly select between the textures for Inky's texture to become
        randomTexture = Random.Range(0,2);

        //Change Inky's texture from its albedo map of its main material
        //"_MainTex" refers to Albedo (main map) in the material
        originalColor.material.SetTexture("_MainTex", inkyTexture[randomTexture]);
    }

    //When the player is on the specific floor, the platform will shortly disappear 
    void OnCollisionEnter(Collision slow)
    {
        if (slow.gameObject.tag == "SlowBreak")
        {
            canDestroyFloor = true;
        }
    }

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
}
