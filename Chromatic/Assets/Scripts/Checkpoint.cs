using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();

        if(player != null)
        {
            player.setCheckpoint(transform.position);
        }
    }
}
