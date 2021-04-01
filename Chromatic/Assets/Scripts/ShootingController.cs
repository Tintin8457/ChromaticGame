﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private Rigidbody playerRB;
    public Camera cam;
    private Player3D playerController;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private Vector3 shootDir;
    public float projectileForce = 10.0f;
    public GameObject projectilePrefab;
    public bool stopShooting;
    public string colorMode; //Store the color that the player has collected
    public List<string> colorInventory = new List<string>(); //Store colors that the player gets
    private static bool grayscaleEnabled = true;
    private static bool redEnabled = false;
    private static bool blueEnabled = false;
    private static bool yellowEnabled = false;
    public bool[] activatedColorModes = new bool[4] {grayscaleEnabled, redEnabled, blueEnabled, yellowEnabled};

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerController = GetComponent<Player3D>();
        projectilePrefab.tag = "Grayscale"; //Player should always begin with the default grayscale mode
        stopShooting = false;
    }

    void Update()
    {
        //Capture mouse screen position and convert to usable world position
        mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        worldPos = cam.ScreenToWorldPoint(mousePos);

        //Shoot key
        if (stopShooting == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        //Direction vector formula
        shootDir = worldPos - playerRB.position;
    }

    void Shoot()
    {
        //Spawn projectile and add force to direction vector
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        
        rb.AddForce(shootDir.normalized * projectileForce, ForceMode.Impulse);
    }

    //Function that gets the current Color Mode, looks for the next unlocked Color Mode, and returns the index value corresponding to that Color Mode
    public int CheckNextAvailableColor(int currentColorMode)
    {   
        for(int i = currentColorMode + 1; i < 4; i++)
        {
            if (activatedColorModes[i])
            {
                return i;
            }
        }
        return 0;
    }


    //Unlocks a Color Mode and changes the player's material to the corresponding material
    public void AddColorMode(string newColorMode)
    {
        Debug.Log("Got to AddColorMode.");
        switch(newColorMode)
        {
            case "red":
                activatedColorModes[1] = true;
                playerController.ChangeMaterial(1);
                break;
            case "blue":
                activatedColorModes[2] = true;
                
                playerController.ChangeMaterial(2);
                break;
            case "yellow":
                activatedColorModes[3] = true;
                playerController.ChangeMaterial(3);
                break;
            default:
                activatedColorModes[0] = true;
                playerController.ChangeMaterial(4);
                break;
        }
    }

    //Once the Color Mode has been changed, it will change the projectile type
    public void ChangeProjType(int givenMode)
    {
        switch (givenMode)
        {
            case 0:
                projectilePrefab.tag = "Grayscale";
                break;
            case 1:
                projectilePrefab.tag = "Red";
                break;
            case 2:
                projectilePrefab.tag = "Blue";
                break;
            case 3:
                projectilePrefab.tag = "Yellow";
                break;
            default:
                projectilePrefab.tag = "Grayscale";
                break;
        }
    }
}
