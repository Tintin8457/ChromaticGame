using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public float playerSpeed = 3.0f;
    public TextMeshProUGUI objectiveText;
    private float horzMovement;
    private float vertMovement;
    public LayerMask allGround;
    public Transform groundCheck;
    public float checkRadius;
    private bool isOnGround = false;
    public Color currentColor;
    private SpriteRenderer spriteRend;

    public bool firstObjDone;

    // Start is called before the first frame update
    void Start()
    {
        
        playerRB = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        currentColor = this.spriteRend.color;
        firstObjDone = false; 
        objectiveText.text = "Collide into the Box!"; //Display first color change objective
    }

    // Update is called once per frame
    void Update()
        {
            //Player directional input
            horzMovement = Input.GetAxis("Horizontal");
            vertMovement = Input.GetAxis("Vertical");

            //Change to the second objective
            if (firstObjDone == true)
            {
                objectiveText.text = "Press C to change the color of a torch!";
            }
            
            //Initial player color set
            this.spriteRend.color = currentColor;
        }

    void FixedUpdate()
    {
        //Player movement
        playerRB.AddForce(new Vector2(horzMovement * playerSpeed, vertMovement * playerSpeed));

        //Ground Check
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);

        //Original movment code
        /*if (Input.GetKey(KeyCode.A))
        {
            playerRB.MovePosition(transform.position + -transform.right * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerRB.MovePosition(transform.position + transform.right * playerSpeed * Time.deltaTime);
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Jump code
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if((Input.GetKey(KeyCode.W)) || (Input.GetKey("up")))
            {
                playerRB.AddForce(new Vector2(0,3), ForceMode2D.Impulse);
                this.spriteRend.color = currentColor;
            }
        }
    }
    
    public void ChangeColor(Color paintColor)
    {
        currentColor = paintColor;
    }

    /////////Test color change by colliding into it/////////
    // void OnCollisionEnter2D(Collision2D change)
    // {
    //     if (change.gameObject.tag == "Color")
    //     {
    //         change.gameObject.GetComponent<Renderer>().material.color = new Color(23, 32, 42, 1);
    //     }
    // }
}
