using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Change object's colors from a specific material
public class MaterialTrigger : MonoBehaviour
{
    //The field allows the user to access a private variable in the inspector
    [SerializeField] private Material testSubject; //Drag in material that is used for more than one material

    //For changing color, we could go for:
    //A simple color: see lines 25 and 35
    //OR A specific color based on the RGBA color values. (You could only use values: 0f to 1f for each value): see lines 26 and 36

    //To test different RGBA values yourself:
    //Go to the material and click on the color box.
    //Then, click on the drop-down menu button and select "RGB 0-1.0"
    //Finally, you can play around of what specific colors you want and add the values onto this script

    //Changes the material's color to a "new" color when the player triggers the object
    void OnTriggerEnter(Collider subject)
    {
        if(subject.gameObject.tag == "Change")
        {
            //testSubject.color = Color.blue; 
            testSubject.color = new Color(0.241f, 0.468f, 0.844f, 1f);
        }
    }

    //Changes the material's color to a "original" color when the player leaves the object
    void OnTriggerExit(Collider subject)
    {
        if(subject.gameObject.tag == "Change")
        {
            //testSubject.color = Color.yellow;
            testSubject.color = new Color(1f, 0.9215686f, 0.01568628f, 1f);
        }
    }
}
