using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int cpNum; //Will be used to identify each checkpoint for the UI

    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();

        if(player != null)
        {
            player.setCheckpoint(transform.position);
            player.UpdateCPUI(cpNum);
        }
    }
}
