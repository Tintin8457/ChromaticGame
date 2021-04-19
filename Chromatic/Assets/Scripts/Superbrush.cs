using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superbrush : MonoBehaviour
{
    public bool superBrushActive = false;
    public int requiredBristleAmount = 11;
    //Animation sb; //Show repair animation
    //public Camera cameraSweep; //Change camera that shows the entire level in full color horizontally back

    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();
        if (player != null)
        {
            if (player.bristles == requiredBristleAmount)
            {
                {
                    superBrushActive = true;
                }
            }
        }
    }
}
