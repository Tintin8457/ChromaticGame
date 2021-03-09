using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player3D : MonoBehaviour
{
    
    private float horzMovement;
    private float vertMovement;
    private Rigidbody playerRB;
    public float playerSpeed = 3.0f;

    private Renderer originalColor;
    public Material currentColor;
    
    public bool canJump = true;


    //Change jump amount in inspector
    public float jumpAmount = 1.0f;

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
                playerRB.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
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

    //Changing the player's color
    public void ChangeColor(Material paintColor)
    {
        currentColor.color = paintColor.color;
    }
}
