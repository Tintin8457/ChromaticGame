using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController3D : MonoBehaviour
{
    private Renderer mesh;
    public Material currentColor;
    //private InkyColorChange colorSwap; Look at og script
    private ShootingController projColor;
    public string colorType; //Stores color type for each paint pickup
    
    void Start()
    {
        mesh = GetComponent<Renderer>();
        currentColor.color = this.mesh.material.color;

        // GameObject coloring = GameObject.FindGameObjectWithTag("Swap");

        // if (coloring != null)
        // {
        //     colorSwap = coloring.GetComponent<InkyColorChange>();
        // }

        //Get and find projectile color manager
        GameObject proj = GameObject.FindGameObjectWithTag("Player");

        if (proj != null)
        {
            projColor = proj.GetComponent<ShootingController>();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();
        
        if (player != null)
        {
            player.ChangeColor(currentColor);
            //colorSwap.AddToColorCollection(); 
            projColor.ColorizeProjectile(colorType);
            Destroy(gameObject);
        }
    }
}
