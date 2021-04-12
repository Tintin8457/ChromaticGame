using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public float maxTime = 120.0f; //Max time for the game
    //public float halfWay; //Use to check for mid-game
    public bool canTime;

    private Player3D player;
    private ShootingController shoot;

    [Header("Buttons")]
    public GameObject restartButton;
    public GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        canTime = true;

        //Find and get player
        GameObject inky = GameObject.FindGameObjectWithTag("Player");

        if (inky != null)
        {
            player = inky.GetComponent<Player3D>();
        }

        //Find and get player's shooting ability
        GameObject shooting = GameObject.FindGameObjectWithTag("Player");

        if (shooting != null)
        {
            shoot = shooting.GetComponent<ShootingController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = Mathf.RoundToInt(maxTime).ToString();

        //Start timer
        if (canTime == true)
        {
            maxTime -= Time.deltaTime;
        }
        
        //End the game at the end of the timer
        //Lose the game
        if (maxTime <= 0f)
        {
            player.playerSpeed = 0f; //Stop player from moving
            player.stopJumping = true; //Stop player from jumping
            shoot.stopShooting = true; //Stop player from shooting
            canTime = false; //Stop timer
            restartButton.SetActive(true); //Summon restart button
            quitButton.SetActive(true); //Summon quit button
        }
    }
}
