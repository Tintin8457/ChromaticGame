using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    [Header("Button")]
    public Animator button;
    public bool canPress; //Only press button once

    [Header("Interactable Object")]
    public GameObject interactable;

    private float display = 3.0f;

    [Header("Enter Bristle Amount")]
    public int bristleNum;

    [Header("Open Bools")]
    public bool open;
    public bool canOpen;

    [Header("Bristle Amount Pop-Up")]
    public GameObject amountPP; //Disable/Enable text when necessary
    public TextMeshProUGUI amountText;

    private Player3D inkyBr;

    // Start is called before the first frame update
    void Start()
    {
        canPress = true;
        amountPP.SetActive(false);

        //Get and find Inky
        GameObject inky = GameObject.FindGameObjectWithTag("Player");

        if (inky != null)
        {
            inkyBr = inky.GetComponent<Player3D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //The door will check if the player has a specific amount of bristles before opening, which is dependent on how many are in the current stage
        if (canPress == false && open == false)
        {
            //The door can open after a required amount
            if (inkyBr.bristles >= bristleNum)
            {
                open = true;
                canOpen = true;
                UpdateText(bristleNum);
                canPress = false;
            }

            //The door cannot open after getting less than the required amount
            else if (inkyBr.bristles < bristleNum)
            {
                UpdateText(bristleNum);
                canPress = true;
            }
        }

        //Open door
        else if (open == true && canOpen == true)
        {
            interactable.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    //The button can only be activated from any of Inky's projectiles
    private void OnTriggerEnter(Collider proj)
    {
        if(canPress == true && (proj.gameObject.tag == "Grayscale" || proj.gameObject.tag == "Red" || proj.gameObject.tag == "Blue" || proj.gameObject.tag == "Yellow"))
        {
            canPress = false; //Activates door/gate
            StartCoroutine("DisplayMessage");
            Destroy(proj.gameObject);
        }
    }

    //Display amount popup
    IEnumerator DisplayMessage()
    {
        amountPP.SetActive(true);
        yield return new WaitForSeconds(display);
        amountPP.SetActive(false);
    }

    //Update pop-up text based on door/gate bristle amount
    public void UpdateText(int br)
    {
        //Display text at 0
        if (inkyBr.bristles == 0)
        {
            br -= inkyBr.bristles;
            amountText.text = "You need to collect " + br.ToString() + " bristles.";
        }

        //Display text higher than 0
        else if (inkyBr.bristles < br)
        {
            br -= inkyBr.bristles;
            amountText.text = "You need to collect " + br.ToString() + " more bristles.";
        }

        //Display text from after getting all bristles in the stage
        else if (inkyBr.bristles >= br)
        {
            amountText.text = "You have collected all bristles.";
            button.SetBool("press", true);
        }
    }
}
