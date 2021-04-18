using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBreakFloor : MonoBehaviour
{
    [Header("Timers")]
    public float timer = 10f; //Timer will be used to destroy and bring back the platform
    public float resetTimer = 10f;
    public float returnPlat = 3.0f; //Max time to bring back the platform
    public float resetReturn = 3.0f;

    public GameObject floor;
    public bool reappear;
    public bool disappear;
    private Player3D player;

    // Start is called before the first frame update
    void Start()
    {
        reappear = false;
        disappear = false;

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
        if (disappear == true)
        {
            timer -= Time.deltaTime; //Start timer

            //Make the platform disappear once time is up
            if (timer <= 0f)
            {
                timer = resetTimer; //Reset timer
                floor.SetActive(false);
                reappear = true;
                disappear = false;
            }
        }

        //Make the platform reappear
        if (reappear == true)
        {
            returnPlat -= Time.deltaTime; //Start returning

            //The platform reappears
            if (returnPlat <= 0f)
            {
                returnPlat = resetReturn; //Reset timer
                floor.SetActive(true);
                reappear = false;
            }
        }
    }

    //Start to make the floor disappear
    void OnTriggerEnter(Collider slow)
    {
        //When the player is on the specific floor, the platform will shortly disappear 
        if (slow.gameObject.tag == "Player")
        {
            disappear = true;
        }
    }
}
