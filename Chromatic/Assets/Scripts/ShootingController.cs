﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private Rigidbody playerRB;
    public Camera cam;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private Vector3 shootDir;
    public float projectileForce = 10.0f;
    public GameObject projectilePrefab;
    public string colorMode; //Store the color that the player has collected
    public List<string> colorInventory = new List<string>(); //Store colors that the player gets

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        projectilePrefab.tag = "Untagged"; //The projectile not have a specific color tag until the player gets a color
    }

    void Update()
    {
        //Capture mouse screen position and convert to usable world position
        mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        worldPos = cam.ScreenToWorldPoint(mousePos);

        //Shoot key
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        //Update the tag of the projectile
        ChangeProjType();
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

    //Change the projectile color type and add the color to the inventory
    public void ColorizeProjectile(string newColor)
    {
        colorMode = newColor;
        colorInventory.Add(colorMode);
    }

    //Once the color mode has been changed, it will change the projectile type
    public void ChangeProjType()
    {
        //Change to red
        if (colorMode == "red")
        {
            projectilePrefab.tag = "Red";
        }

        //Change to yellow
        else if (colorMode == "yellow")
        {
            projectilePrefab.tag = "Yellow";
        }

        //Change to blue
        else if (colorMode == "blue")
        {
            projectilePrefab.tag = "Blue";
        }

        //Changes to grayscale
        else if (colorMode == "grayscale")
        {
            projectilePrefab.tag = "Grayscale";
        }
    }
}
