using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset; //Coordinates are set in inspector

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Old way to updating camera movement
        //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        //New way to update camera movement
        Vector3 desiredPosition = player.transform.position + offset; //Get an updated position as the player moves in the level
        transform.position = desiredPosition; //The camera will move from its updated position (up/down when player jumps & left/right when player runs)
    }
}
