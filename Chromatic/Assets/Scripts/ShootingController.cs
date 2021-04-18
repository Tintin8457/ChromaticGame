using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Shooting Functionality")]
    public Camera cam;
    private Rigidbody playerRB;
    private Player3D playerController;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private Vector3 shootDir;
    public float projectileForce = 10.0f;
    public GameObject projectilePrefab;
    public bool stopShooting;
    //public string colorMode; //Store the color that the player has collected
    //public List<string> colorInventory = new List<string>(); //Store colors that the player gets
    private static bool grayscaleEnabled = true;
    private static bool redEnabled = false;
    private static bool blueEnabled = false;
    private static bool yellowEnabled = false;

    [Header("Color Modes")]
    public bool[] activatedColorModes = new bool[4] {grayscaleEnabled, redEnabled, blueEnabled, yellowEnabled};
    private int colorModes = 0; //Will be used to change the next color UI

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerController = GetComponent<Player3D>();
        projectilePrefab.tag = "Grayscale"; //Player should always begin with the default grayscale mode
        stopShooting = false;
    }

    void Update()
    {
        //Capture mouse screen position and convert to usable world position
        mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        worldPos = cam.ScreenToWorldPoint(mousePos);

        //Shoot key
        if (stopShooting == false)
        {
            if (Time.timeScale == 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shoot();
                }
            }
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
        
        rb.AddForce(shootDir.normalized * projectileForce, ForceMode.Impulse);
    }

    //Function that gets the current Color Mode, looks for the next unlocked Color Mode, and returns the index value corresponding to that Color Mode
    public int CheckNextAvailableColor(int currentColorMode)
    {   
        for(int i = currentColorMode + 1; i < 4; i++)
        {
            if (activatedColorModes[i])
            {
                return i;
            }
        }
        return 0;
    }

    //Unlocks a Color Mode, changes the player's material to the corresponding material, colorizes the projectile, and updates the current/new color UI
    public void AddColorMode(string newColorMode)
    {
        switch(newColorMode)
        {
            case "grayscale":
                activatedColorModes[0] = true;
                projectilePrefab.tag = "Grayscale";
                ColorizeProjectile();
                playerController.ChangeMaterial(0);
                //playerController.UpdateCurColorUI(0);
                //playerController.ColorSwaping(0);
                //playerController.ChangeNewColorUI(1);
                break;
            case "red":
                activatedColorModes[1] = true;
                projectilePrefab.tag = "Red";
                ColorizeProjectile();
                playerController.ChangeMaterial(1);
                //playerController.UpdateCurColorUI(1);
                //playerController.ColorSwaping(1);
                //playerController.ChangeNewColorUI(2);
                break;
            case "blue":
                activatedColorModes[2] = true;
                projectilePrefab.tag = "Blue";
                ColorizeProjectile();
                playerController.ChangeMaterial(2);
                //playerController.UpdateCurColorUI(2);
                //playerController.ColorSwaping(2);
                //playerController.ChangeNewColorUI(3);
                break;
            case "yellow":
                activatedColorModes[3] = true;
                projectilePrefab.tag = "Yellow";
                ColorizeProjectile();
                playerController.ChangeMaterial(3);
                //playerController.UpdateCurColorUI(3);
                //playerController.ColorSwaping(3);
                //playerController.ChangeNewColorUI(0);
                break;
            default:
                activatedColorModes[0] = true;
                projectilePrefab.tag = "Grayscale";
                ColorizeProjectile();
                playerController.ChangeMaterial(0);
                //playerController.UpdateCurColorUI(0);
                //playerController.ColorSwaping(0);
                //playerController.ChangeNewColorUI(1);
                break;
        }
    }

    //Once the Color Mode has been changed, it will change the projectile type
    public void ChangeProjType(int givenMode)
    {
        switch (givenMode)
        {
            case 0:
                projectilePrefab.tag = "Grayscale";
                ColorizeProjectile();
                // playerController.colorSwap.color = playerController.uiColors[0];
                break;
            case 1:
                projectilePrefab.tag = "Red";
                ColorizeProjectile();
                // playerController.colorSwap.color = playerController.uiColors[1];
                break;
            case 2:
                projectilePrefab.tag = "Blue";
                ColorizeProjectile();
                // playerController.colorSwap.color = playerController.uiColors[2];
                break;
            case 3:
                projectilePrefab.tag = "Yellow";
                ColorizeProjectile();
                // playerController.colorSwap.color = playerController.uiColors[3];
                break;
            default:
                projectilePrefab.tag = "Grayscale";
                ColorizeProjectile();
                // playerController.colorSwap.color = playerController.uiColors[0];
                break;
        }
    }

    //Changes the projectile color to match Inky's color
    public void ColorizeProjectile()
    {
        if (projectilePrefab == null)
        {
            projectilePrefab.GetComponent<Projectile>().ComponentIndex();
        }
    }
}

// //Check when the next color UI can change
    // public int CheckForNextColorUI(int nColor)
    // {
    //     for (int c = 0; c < nColor; c++)
    //     {
    //         //playerController.colorSwap.color = playerController.colorInventory[nColor + 1];
    //         //nColor += c;
    //         // playerController.uiColors[c];
    //         //nColor = c;

    //         if (c == 0)
    //         {
    //             playerController.colorSwap.color = playerController.colorInventory[c + 1];
    //             Debug.Log("color: " + c);
    //         }

    //         else if (c == 1)
    //         {
    //             playerController.colorSwap.color = playerController.colorInventory[c + 1];
    //             Debug.Log("color: " + c);
    //         }

    //         else if (c == 2)
    //         {
    //             playerController.colorSwap.color = playerController.colorInventory[c + 1];
    //             Debug.Log("color: " + c);
    //         }

    //         else if (c == 3)
    //         {
    //             playerController.colorSwap.color = playerController.colorInventory[c - 3];
    //             Debug.Log("color: " + c);    
    //         }
    //     }

    //     // foreach (var c in playerController.colorInventory)
    //     // {   
    //     //     playerController.colorInventory[c] += 1;
    //     // }
    //     return 0;
    // }

// public void ChangeNewColorUI(int nColor)
    // {
    //     if (activatedColorModes[0] == true && activatedColorModes[1] == true && activatedColorModes[2] == true && activatedColorModes[3] == true)
    //     {
    //         switch(nColor)
    //         {
    //             case 0:
    //                 playerController.colorSwap.color = playerController.uiColors[1];
    //                 break;
    //             case 1:
    //                 playerController.colorSwap.color = playerController.uiColors[2];
    //                 break;
    //             case 2:
    //                 playerController.colorSwap.color = playerController.uiColors[3];
    //                 break;
    //             case 3:
    //                 playerController.colorSwap.color = playerController.uiColors[0];
    //                 break;
    //             default:
    //                 playerController.colorSwap.color = playerController.uiColors[1];
    //                 break;
    //         }
    //     }

    //     // if (activatedColorModes.Length > 1)
    //     // {   
    //     //     if (nColor == 1)
    //     //     {
    //     //         playerController.colorSwap.color = playerController.uiColors[1];
    //     //     }

    //     //     else if (nColor == 2)
    //     //     {
    //     //         playerController.colorSwap.color = playerController.uiColors[2];
    //     //     }
        
    //     //     else if (nColor == 3)
    //     //     {
    //     //         playerController.colorSwap.color = playerController.uiColors[3];
    //     //     }

    //     //     else if (nColor == 0)
    //     //     {
    //     //         playerController.colorSwap.color = playerController.uiColors[0];
    //     //     }
    //     // }
    // }