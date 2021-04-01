using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    void OnCollisionStay(Collision collision)
    {
        Player3D player = collision.gameObject.GetComponent<Player3D>();
        if (player != null)
        {
            player.ResetToCheckpoint();
        }
    }
}
