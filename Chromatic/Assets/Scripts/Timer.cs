using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float maxTime = 120.0f; //Max time for the game
    public bool canTime;

    private Player3D player;

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
            canTime = false; //Stop timer
            restartButton.SetActive(true); //Summon restart button
            quitButton.SetActive(true); //Summon quit button
        }
    }
}
