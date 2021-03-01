using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringBox3D : MonoBehaviour
{
    private Renderer mesh;
    private Player3D player;

    // Start is called before the first frame update
    void Start()
    {
        //Get player
        GameObject testPlayer = GameObject.FindWithTag("Player");

        //Find player
        if (testPlayer != null)
        {
            player = testPlayer.GetComponent<Player3D>();
        }
        
        mesh = GetComponent<Renderer>();
    }

    /////////Test color change from player collision/////////
    void OnCollisionEnter(Collision change)
    {

        if (change.gameObject.tag == "Player")
        {
            //Color change code
            Player3D player = change.gameObject.GetComponent<Player3D>();

            if(player != null)
            {
                mesh.material.color = player.currentColor.color; 
            }
        }
    }
}
