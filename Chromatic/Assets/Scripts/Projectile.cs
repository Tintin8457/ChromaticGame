using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Renderer[] paintball; //All renderers from each component of the children
    private Player3D player;
    public float projectileMaxDist = 50.0f;
    //private int components;
    Animator ammoMovement;

    void Start()
    {
        //paintball = GetComponentInChildren<Renderer>();
        ammoMovement = GetComponent<Animator>();
        //components = paintball.Length; //Get length of ammo's materials

        //Find and get player
        GameObject colorizeAmmo = GameObject.FindGameObjectWithTag("Player");

        if (colorizeAmmo != null)
        {
            player = colorizeAmmo.GetComponent<Player3D>();
        }
    }

    void Update()
    {
        if ( ((transform.position.magnitude - player.transform.position.magnitude) > projectileMaxDist) || (transform.position.magnitude - player.transform.position.magnitude < (-1.0f * projectileMaxDist)) )
        {
            Destroy(gameObject);
        }

        //Match the projectile color to player's color
        //paintball.material.color = player.currentColor.color;
        ComponentIndex();
        ammoMovement.SetBool("CanLaunch", true); //Play ammo animation
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); //Projectiles are destroyed once they hit other objects

        //Destroy breakable walls
        // if (collision.gameObject.tag == "Breakable")
        // {
        //     Destroy(collision.gameObject);
        // }
    }

    //Contains each part of the projectile
    public void ComponentIndex()
    {
        paintball[0].material.color = player.currentColor.color;
        paintball[1].material.color = player.currentColor.color;
        paintball[2].material.color = player.currentColor.color;
        paintball[3].material.color = player.currentColor.color;
        paintball[4].material.color = player.currentColor.color;
        paintball[5].material.color = player.currentColor.color;
    }
}
