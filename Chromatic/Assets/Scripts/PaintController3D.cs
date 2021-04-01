using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController3D : MonoBehaviour
{
    //private InkyColorChange colorSwap; Look at og script
    private ShootingController playerShooting;
    public string colorType; //Stores color type for each paint pickup
    
    void Start()
    {

        // GameObject coloring = GameObject.FindGameObjectWithTag("Swap");

        // if (coloring != null)
        // {
        //     colorSwap = coloring.GetComponent<InkyColorChange>();
        // }

        //Get and find projectile color manager
        GameObject proj = GameObject.FindGameObjectWithTag("Player");

        if (proj != null)
        {
            playerShooting = proj.GetComponent<ShootingController>();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered Pick Up.");
        if (playerShooting != null)
        {
            playerShooting.AddColorMode(colorType);
            Destroy(gameObject);
        }
    }
}
