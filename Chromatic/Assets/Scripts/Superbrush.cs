using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superbrush : MonoBehaviour
{
    //Animation sb; //Show repair animation
    //public Camera cameraSweep; //Change camera that shows the entire level in full color horizontally back

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
