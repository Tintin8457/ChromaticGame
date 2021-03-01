using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController3D : MonoBehaviour
{
    private Renderer mesh;
    public Material currentColor;
    
    void Start()
    {
        mesh = GetComponent<Renderer>();
        currentColor.color = this.mesh.material.color;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();
        
        if (player != null)
        {
            player.ChangeColor(currentColor);
            Destroy(gameObject);
        }
    }
}
