using System.Collections;
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
    //Animator ammoMovement;
    
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        //ammoMovement = GetComponent<Animator>();
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
        
        //ammoMovement.SetBool("CanLaunch", true); //Play ammo animation
        rb.AddForce(shootDir.normalized * projectileForce, ForceMode.Impulse);
    }
}
