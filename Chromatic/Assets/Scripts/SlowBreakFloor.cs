using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBreakFloor : MonoBehaviour
{
    public float timer = 10f; //Max time
    public GameObject floor;
    public bool reappear;
    private Player3D player;

    // Start is called before the first frame update
    void Start()
    {
        reappear = false;

        //Find and get player
        GameObject feelPlayer = GameObject.FindGameObjectWithTag("Player");

        if (feelPlayer != null)
        {
            player = feelPlayer.GetComponent<Player3D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //The timer is activated from the player touching the floor
        if (player.canDestroyFloor == true)
        {
            timer -= Time.deltaTime; //Start timer

            //Make the platform disappear once time is up
            if (timer <= 0f)
            {
                floor.SetActive(false);
                //Destroy(gameObject);
                reappear = true;
                player.canDestroyFloor = false;
            }
        }

        //Make the platform reappear
        if (reappear == true)
        {
            timer += Time.deltaTime; //Resume timer

            //The platform reappears
            if (timer >= 2.1f)
            {
                floor.SetActive(true);
                reappear = false;
                timer = 10f; //Reset
            }
        }
    }
}
