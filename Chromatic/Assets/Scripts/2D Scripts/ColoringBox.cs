using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringBox : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        //Get player
        GameObject testPlayer = GameObject.FindWithTag("Player");

        //Find player
        if (testPlayer != null)
        {
            player = testPlayer.GetComponent<Player>();
        }
        
        spriteRend = GetComponent<SpriteRenderer>();
    }

    /////////Test color change from player collision/////////
    void OnCollisionEnter2D(Collision2D change)
    {

        if (change.gameObject.tag == "Player")
        {
            //Color change code
            Player player = change.gameObject.GetComponent<Player>();
            if(player != null)
            {
            spriteRend.color = player.currentColor; 
            }
            
            //StartCoroutine("RemoveObstacle");
        }
    }

    //Remove obstacle after a few seconds
    /*IEnumerator RemoveObstacle()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        player.firstObjDone = true; //Change the objective
    }*/
}
