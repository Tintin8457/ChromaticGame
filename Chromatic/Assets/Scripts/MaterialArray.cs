using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changes the trigger's color from a color palette (array)
public class MaterialArray : MonoBehaviour
{
    public Material[] colorPalette; //Set number of materials in inspector
    Renderer curColor; //Color of the object

    // Start is called before the first frame update
    void Start()
    {
        //Set default color for the object
        curColor = GetComponent<Renderer>();
        curColor.sharedMaterial = colorPalette[0]; 
    }

    //Changes the trigger's color to a different color from the palette when the player triggers into the trigger
    void OnTriggerEnter(Collider experimenting)
    {
        if(experimenting.gameObject.tag == "Player")
        {
            curColor.sharedMaterial = colorPalette[1]; 
        }
    }

    //Changes the trigger's color to a new color from the palette when the player leaves the trigger
    void OnTriggerExit(Collider experimenting)
    {
        if(experimenting.gameObject.tag == "Player")
        {
            curColor.sharedMaterial = colorPalette[2]; 
        }
    }
}
