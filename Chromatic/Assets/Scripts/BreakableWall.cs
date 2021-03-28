using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    //Destroy breakable walls from a grayscale projectile
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grayscale")
        {
            Destroy(gameObject);
        }
    }
}
