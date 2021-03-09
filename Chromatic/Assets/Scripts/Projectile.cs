using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileMaxDist = 50.0f;
    void Update()
    {
        if (transform.position.magnitude > projectileMaxDist)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
