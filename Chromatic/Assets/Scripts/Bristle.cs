using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bristle : MonoBehaviour
{
    //Add 1 bristle to the current bristle amount UI text
    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();

        if(player != null)
        {
            player.AddBristles(1);
            Destroy(gameObject);
        }
    }
}
