using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superbrush : MonoBehaviour
{
    public bool superBrushActive = false;
    public int requiredBristleAmount = 11;
    public Camera mainCamera;
    public Camera sweepCamera;
    private Animator camSweepAnim;
    public GameObject checkpointUI;
    public GameObject curColorUI;
    public GameObject nextColorUI;
    public GameObject prevColorUI;
    public GameObject bristleUI;
    public GameObject timerUI;
    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject restartButton;
    public GameObject quitButton;
    //Animation sb; //Show repair animation
    //public Camera cameraSweep; //Change camera that shows the entire level in full color horizontally back

    void Start()
    {
        camSweepAnim = sweepCamera.GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        Player3D player = other.GetComponent<Player3D>();
        ShootingController shoot = other.GetComponent<ShootingController>();
        if (player != null)
        {
            if (player.bristles == requiredBristleAmount)
            {
                {
                    superBrushActive = true;
                    mainCamera.enabled = false;
                    sweepCamera.enabled = true;
                    camSweepAnim.enabled = true;
                    checkpointUI.SetActive(false);
                    curColorUI.SetActive(false);
                    prevColorUI.SetActive(false);
                    nextColorUI.SetActive(false);
                    bristleUI.SetActive(false);
                    timerUI.SetActive(false);
                    pauseUI.SetActive(false);
                    winUI.SetActive(true);
                    restartButton.SetActive(true);
                    quitButton.SetActive(true);
                    player.playerSpeed = 0f;
                    player.stopJumping = true;
                    shoot.stopShooting = true;
                    player.inkySound.Stop();
                }
            }
        }
    }
}
