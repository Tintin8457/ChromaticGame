using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    public Color currentColor;
    
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        currentColor = this.spriteRend.color;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        if (player != null)
        {
            player.ChangeColor(currentColor);
            Destroy(gameObject);
        }
    }
}
