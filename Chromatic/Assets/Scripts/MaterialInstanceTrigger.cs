using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Change object's colors from a specific object
public class MaterialInstanceTrigger : MonoBehaviour
{
    //Drag in any of the test subject cubes that has a mesh renderer with a material in the inspector
    [SerializeField] private Renderer thatObject;

    //Explanation is in MaterialTrigger script

    //Changes the material's color to a "new" color when the player triggers the object
    void OnTriggerEnter(Collider chosenOne)
    {
        if(chosenOne.gameObject.tag == "Change")
        {
            //testSubject.material.color = Color.green; 
            thatObject.material.color = new Color(0.413592f, 0.8113208f, 0.3176397f, 1f);
        }
    }

    //Changes the material's color to a "original" color when the player leaves the object
    void OnTriggerExit(Collider chosenOne)
    {
        if(chosenOne.gameObject.tag == "Change")
        {
            //testSubject.material.color = Color.red;
            thatObject.material.color = new Color(0.8584906f, 0.4875486f, 0.4413938f, 1f);
        }
    }
}
