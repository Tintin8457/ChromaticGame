using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Renderer paintball;
    private Player3D player;
    public float projectileMaxDist = 50.0f;

    void Start()
    {
        paintball = GetComponent<Renderer>();

        //Find and get player
        GameObject colorizeAmmo = GameObject.FindGameObjectWithTag("Player");

        if (colorizeAmmo != null)
        {
            player = colorizeAmmo.GetComponent<Player3D>();
        }
    }

    void Update()
    {
        if (transform.position.magnitude > projectileMaxDist)
        {
            Destroy(gameObject);
        }

        //Match projectile color to player's color
        paintball.material.color = player.currentColor.color;
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
