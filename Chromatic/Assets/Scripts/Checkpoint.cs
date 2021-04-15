using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Set checkpoint and show checkpoint text
    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();

        if(player != null)
        {
            player.SetCheckpoint(transform.position);
            player.ShowCPText(true);
        }
    }

    //Exit and remove last checkpoint
    void OnTriggerExit(Collider exitCP)
    {
        Player3D player = exitCP.GetComponent<Player3D>();

        if(player != null)
        {
            player.ShowCPText(false);
            Destroy(gameObject);
        }
    }
}
