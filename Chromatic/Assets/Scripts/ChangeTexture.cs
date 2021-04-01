using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script temporarily changes the player's textures until we make later changes to this mechanic
public class ChangeTexture : MonoBehaviour
{
    //Change Inky's texture when the player collects this pickup
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            //player.GetComponent<Player3D>().ChangeTexture();
            Destroy(gameObject);
        }
    }
}
