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
    //public Color currentColor;
    public Material currentColor;

    [Header("Jump Functionality")]
    public LayerMask allGround;
    public Transform groundCheck;
    public float checkRadius;
    private bool isOnGround = false;

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
    }

    void FixedUpdate()
    {
        //Old Player movement
        //playerRB.AddForce(new Vector3(horzMovement * playerSpeed, vertMovement * playerSpeed));
        //Vector3 movePlayer = new Vector3(horzMovement, 0, vertMovement);
        //transform.position = new Vector3(horzMovement, 0 , vertMovement) * Time.deltaTime * playerSpeed;

        //New player movement
        transform.Translate(new Vector3(horzMovement, 0 , 0) * Time.deltaTime * playerSpeed);

        //Ground Check
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);
    }

    //////Jump in progress//////
    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     //Jump code
    //     if (collision.collider.tag == "Ground" && isOnGround)
    //     {
    //         if((Input.GetKey(KeyCode.W)) || (Input.GetKey("up")))
    //         {
    //             playerRB.AddForce(new Vector3(0, vertMovement, 0), ForceMode.Impulse);
    //             //this.spriteRend.color = currentColor;
    //         }
    //     }
    // }

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
