using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Complete Work In Progress I just quickly copy and pasted from the original player script, don't think that's gonna work the same for 3D

public class Player3D : MonoBehaviour
{
    private float horzMovement;
    private float vertMovement;
    private Rigidbody playerRB;
    public float playerSpeed = 3.0f;

    private Renderer originalColor;
    public Material currentColor;

    //2D jump functionality
    /*[Header("Jump Functionality")]
    public LayerMask allGround;
    public Transform groundCheck;
    public float checkRadius;
    private bool isOnGround = false;*/

    public bool canJump = true;

    //Change jump amount in inspector
    public float jumpAmount = 30.0f;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        originalColor = GetComponent<Renderer>();
        currentColor.color = this.originalColor.material.color;
    }

    void Update()
    {
        //Player directional input
        horzMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical"); //Could need this later? 

        //Initial player color set
        this.originalColor.material.color = currentColor.color;

        //Press the spacebar to jump if the player can jump
        if (canJump == true)
        {
            if((Input.GetKey(KeyCode.Space)))
            {
                playerRB.AddForce(Vector3.up * jumpAmount);
            }
        }
    }

    void FixedUpdate()
    {
        //Old Player movement
        //playerRB.AddForce(new Vector3(horzMovement * playerSpeed, vertMovement * playerSpeed));
        //Vector3 movePlayer = new Vector3(horzMovement, 0, vertMovement);
        //transform.position = new Vector3(horzMovement, 0 , vertMovement) * Time.deltaTime * playerSpeed;

        //New player movement
        transform.Translate(new Vector3(horzMovement, 0 , 0) * Time.deltaTime * playerSpeed);

        //2D Ground Check
        //isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);
        //isOnGround = Physics.OverlapSphere(groundCheck.position, checkRadius, allGround);
    }

    //////Jump//////
    void OnCollisionStay(Collision collision)
    {
        //Old Jump code
        // if (collision.gameObject.tag == "Ground" && isOnGround)
        // {
        //     if((Input.GetKey(KeyCode.W)) || (Input.GetKey("up")))
        //     {
        //         //playerRB.AddForce(new Vector2(0, 3), ForceMode.Impulse);
        //         playerRB.AddForce(Vector3.up * 3.0f, ForceMode.Impulse);
        //         //this.spriteRend.color = currentColor;
        //     }
        // }

        //Player can only jump if they are ON the ground
        if (collision.collider.tag == "Ground")
        {
            canJump = true;
        }
    }

    //Player can NOT jump if they are NOT the ground
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            canJump = false;
        }
    }

    //2D version of changing color
    // public void ChangeColor(Color paintColor)
    // {
    //     currentColor = paintColor;
    // }

    //Updated way to changing the player's color
    public void ChangeColor(Material paintColor)
    {
        currentColor.color = paintColor.color;
    }
}
