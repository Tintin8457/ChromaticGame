using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private Renderer originalColor;
    public Material currentColor;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCol = GetComponent<SphereCollider>();
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
        if(IsGrounded() && (Input.GetKeyDown(KeyCode.Space)))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log(Vector3.up * jumpForce);
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

    //Ground Check Function
    public bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCol.bounds.center, new Vector3(playerCol.bounds.center.x, playerCol.bounds.min.y, playerCol.bounds.center.z), playerCol.radius * 0.9f, groundLayer);
    }
}
