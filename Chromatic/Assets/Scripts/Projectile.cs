using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Renderer[] paintball; //All renderers from each component of the children
    public Texture[] projColors; //Holds different projectile color textures
    private Player3D player;
    Animator ammoMovement;
    public float projectileMaxDist = 50.0f;
    //private int components;

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
    }

    //The projectile's texture becomes colorized to match the player's color
    public void ComponentIndex()
    {
        foreach (var paint in paintball)
        {
            //Changes to gray
            if (gameObject.tag == "Grayscale")
            {
                //paint.material.color = new Color(1f, 1f, 1f, 1f);
                //paint.material.color = new Color(0.4622642f, 0.4208348f, 0.4208348f, 1f);
                paint.material.mainTexture = projColors[0];
            }

            //Changes to red
            else if (gameObject.tag == "Red")
            {
                //paint.material.color = new Color(0.9215686f, 0.1607843f, 0.1921569f, 1f);
                paint.material.mainTexture = projColors[1];
            }

            //Changes to blue
            else if (gameObject.tag == "Blue")
            {
                //paint.material.color = new Color(0.2980392f, 0.3490196f, 0.6588235f, 1f);
                //paint.material.color = Color.blue;
                paint.material.mainTexture = projColors[2];
            }

            //Changes to yellow
            else if (gameObject.tag == "Yellow")
            {
                //paint.material.color = new Color(0.9294118f, 0.9058824f, 0.08235294f, 1f);
                paint.material.mainTexture = projColors[3];
            }
        }
    }
}
