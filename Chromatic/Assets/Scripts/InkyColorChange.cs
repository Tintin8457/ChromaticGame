using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tried to use this a demo to show off switching colors
public class InkyColorChange : MonoBehaviour
{
    private Player3D player;
    public Image colSwapIcon;
    public Material[] colorModes; //Store colors here
    public List<Material> mode = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
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
        colSwapIcon.color = player.currentColor.color;
    }

    //Store colors into a color collection
    public void AddToColorCollection()
    {
        // for (int i = colorModes.Length; i < colorModes.Length; i++)
        // {
        //     colorModes[i + 1] = player.currentColor;
        // }
        
        //colorModes[colorModes.Length + 1] = player.currentColor;

        foreach (Material c in mode)
        {
            //c = player.currentColor;
            mode.Add(player.currentColor);
        }
    }
}
