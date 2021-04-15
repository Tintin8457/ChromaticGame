using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //Animator button;
    public bool canPress; //Only press button once

    // Start is called before the first frame update
    void Start()
    {
        canPress = true;
        //button = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     button.SetBool("push", true);
        // }
    }

    //The button can only be activated from any of Inky's projectiles
    private void OnTriggerEnter(Collider proj)
    {
        if(canPress == true && (proj.gameObject.tag == "Grayscale" || proj.gameObject.tag == "Red" || proj.gameObject.tag == "Blue" || proj.gameObject.tag == "Yellow"))
        {
            canPress = false;
            Destroy(proj.gameObject);
        }
    }

    //Causes the button to activate the door/gate
    private void OnTriggerExit(Collider butEvent)
    {

    }
}
