using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only works with the pipeline asset applied in the graphics portion in project settings
public class ShaderBW : MonoBehaviour
{
    //Attch all necessary materials for the shader and change the size in inspector
    [Header("Object Mesh Renderers")]
    public MeshRenderer[] bwShader; //This will be needed for all objects, included ones that use more than one mesh renderers

    [Header("Shader Behavior")]
    public bool paintable; //Change its value in inspector to indicate what can be semi-transparent
    public bool canBePainted; //Change shader look through platforms

    [Header("Sticky Platforms")]
    public bool stickyWall; //Prevents the sticky wall from being semi-transparent
    //public bool paintSticky; //Can only paint sticky platforms

    [Header("Purple Platforms")]
    public bool purplePlat; //Changes color when told to
    // public bool red; //Change to red
    // public bool blue; //Change to blue

    private MaterialPropertyBlock shader; //Use to reference the shader graph properties
    MeshRenderer components; //Contains all colored elements of the object
    //private Timer timer; //Access timer to change specific objects into toon shader mid-game
    private Superbrush superBrush;

    //Use shaders to change from bw to toon shader during the game;
    //Shader bAndW;
    Shader toon;

    //Use colors for sticky platforms
    private Color ogStick;
    private Color sticky;

    //Uses colors for purple platforms
    private Color ogPurple;
    private Color hor;
    private Color ver;

    //public bool nonPaintable; //Change its value in inspector to indicate what can't be semi-transparent and desaturated

    // Start is called before the first frame update
    void Start()
    {
        shader = new MaterialPropertyBlock(); //Set up a new shader property that allows the bw shader to work properly
        
        components = GetComponent<MeshRenderer>(); //Get the object's renderer

        //Find the toon shader to use for nonpaintables
        //bAndW = Shader.Find("Shader Graphs/BlackAndWhite");

        if (paintable == false && stickyWall == false && purplePlat == false)
        {
            toon = Shader.Find("Shader Graphs/ToonCellShader");
        }

        //Find and get timer
        /*GameObject time = GameObject.FindGameObjectWithTag("Timer");

        if (time != null)
        {
            timer = time.GetComponent<Timer>();
        }*/
        
        //Find and get Superbrush script
        GameObject sBrush = GameObject.FindGameObjectWithTag("Superbrush");

        if (sBrush != null)
        {
            superBrush = sBrush.GetComponent<Superbrush>();
        }

        //For objects that use one mesh renderer
        if (components != null)
        {
            //The non-paintable object will be black & white and fully visible
            if (paintable == false)
            {
                shader.SetFloat("_Saturation", 0f);
                components.SetPropertyBlock(shader);
            }

            //Only make paintable objects semi-transparent, colored, and paintable
            if (paintable == true)
            {
                shader.SetFloat("_Opacity", 0.5f);
                components.SetPropertyBlock(shader);
                canBePainted = true;
            }

            //Make the sticky wall paintable and semi-transparent
            if (stickyWall == true)
            {
                shader.SetFloat("_Opacity", 0.5f);
                components.SetPropertyBlock(shader);
                canBePainted = true;

                //Assign colors for original wall color and yellow painted color
                ogStick = new Color(0f, 0.509434f, 0.0125797f, 0f);
                sticky = new Color(0.7372549f, 0.6901961f, 0.0627451f, 0f);
            }

            //Make purple platforms semi-transparent, colored, and paintable
            if (purplePlat == true)
            {
                shader.SetFloat("_Opacity", 0.5f);
                components.SetPropertyBlock(shader);
                canBePainted = true;

                //Does not change to these colors by default
                // red = false;
                // blue = false;

                //Assign colors for purple platform color and direction colors
                // ogPurple = new Color(0.5547814f, 0f, 0.7075472f, 0f);
                // hor = new Color(1f, 0f, 0.07843138f, 0f);
                // ver = new Color(0f, 0.1127396f, 0.7264151f, 0f);
            }
        }
        
        //For objects that use more than one mesh renderer
        else if (components == null)
        {
            //Get the mesh renderer for any object has more than one material
            foreach (var mat in bwShader)
            {
                //The non-paintable object will be black & white and fully visible
                if (paintable == false)
                {
                    shader.SetFloat("_Saturation", 0f);
                    mat.SetPropertyBlock(shader);
                }

                //Only make paintable objects semi-transparent, colored, and paintable
                if (paintable == true)
                {
                    shader.SetFloat("_Opacity", 0.5f);
                    mat.SetPropertyBlock(shader);
                    canBePainted = true;
                }

                //Make the sticky wall paintable and semi-transparent
                if (stickyWall == true)
                {
                    shader.SetFloat("_Opacity", 0.5f);
                    mat.SetPropertyBlock(shader);
                    canBePainted = true;

                    //Assign colors for original wall color and yellow painted color
                    ogStick = new Color(0f, 0.509434f, 0.0125797f, 0f);
                    sticky = new Color(0.7372549f, 0.6901961f, 0.0627451f, 0f);
                }  

                //Make purple platforms semi-transparent, colored, and paintable
                if (purplePlat == true)
                {
                    shader.SetFloat("_Opacity", 0.5f);
                    mat.SetPropertyBlock(shader);
                    canBePainted = true;

                    //Does not change to these colors by default
                    // red = false;
                    // blue = false;

                    //Assign colors for purple platform color and direction colors
                    // ogPurple = new Color(0.5547814f, 0f, 0.7075472f, 0f);
                    // hor = new Color(1f, 0f, 0.07843138f, 0f);
                    // ver = new Color(0f, 0.1127396f, 0.7264151f, 0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //For objects that use one mesh renderer
        if (components != null)
        {
            //Make paintables semi-transparent/transparent
            if (paintable == true)
            {
                //Make semi-transparent to indicate that it needs to be painted
                if (canBePainted == true)
                {
                    shader.SetFloat("_Opacity", 0.5f);
                    components.SetPropertyBlock(shader);
                }

                //Make visible to indicate that it has been painted
                else if (canBePainted == false)
                {
                    shader.SetFloat("_Opacity", 1f);
                    components.SetPropertyBlock(shader);
                }
            }

            //Change sticky platform's color
            if (stickyWall == true)
            {
                //Reset to default color and semi-transparency
                if (canBePainted == true)
                {
                    shader.SetColor("_ShaColor", ogStick);
                    shader.SetFloat("_Opacity", 0.5f);
                    components.SetPropertyBlock(shader);
                }
                
                //Change to yellow paint and make visible
                else if (canBePainted == false)
                {
                    shader.SetColor("_ShaColor", sticky);
                    shader.SetFloat("_Opacity", 1f);
                    components.SetPropertyBlock(shader);
                }
            }

            //Change purple platform's color and transparency
            if (purplePlat == true)
            {
                //Reset to default color and semi-transparency
                if (canBePainted == true)
                {
                    // red = false;
                    // blue = false;

                    //shader.SetColor("_ShaColor", ogPurple);
                    shader.SetFloat("_Opacity", 0.5f);
                    components.SetPropertyBlock(shader);
                }

                //Change to indicated color and make visible
                else if (canBePainted == false)
                {
                    // if (red == true)
                    // {
                    //     //shader.SetColor("_ShaColor", hor);
                    //     shader.SetFloat("_Opacity", 1f);
                    //     components.SetPropertyBlock(shader);
                    // }

                    // else if (blue == true)
                    // {
                    //     //shader.SetColor("_ShaColor", ver);
                    //     shader.SetFloat("_Opacity", 1f);
                    //     components.SetPropertyBlock(shader);
                    // }

                    shader.SetFloat("_Opacity", 1f);
                    components.SetPropertyBlock(shader);
                }
            }
        }

        //For objects that use more than one mesh renderer
        else if (components == null)
        {
            foreach (var mat in bwShader)
            {
                //Make paintables semi-transparent/transparent
                if (paintable == true)
                {
                    //Make semi-transparent to indicate that it needs to be painted
                    if (canBePainted == true)
                    {
                        shader.SetFloat("_Opacity", 0.5f);
                        mat.SetPropertyBlock(shader);
                    }

                    //Make visible to indicate that it has been painted
                    else if (canBePainted == false)
                    {
                        shader.SetFloat("_Opacity", 1f);
                        mat.SetPropertyBlock(shader);
                    }
                }

                //Change sticky platform's color
                if (stickyWall == true)
                {
                    //Reset to default color and semi-transparency
                    if (canBePainted == true)
                    {
                        shader.SetColor("_ShaColor", ogStick);
                        shader.SetFloat("_Opacity", 0.5f);
                        mat.SetPropertyBlock(shader);
                    }
                    
                    //Change to indicated color and make visible
                    else if (canBePainted == false)
                    {
                        shader.SetColor("_ShaColor", sticky);
                        shader.SetFloat("_Opacity", 1f);
                        mat.SetPropertyBlock(shader);
                    }
                }

                //Change purple platform's color
                if (purplePlat == true)
                {
                    //Reset to default color
                    if (canBePainted == true)
                    {
                        // red = false;
                        // blue = false;

                        //shader.SetColor("_ShaColor", ogPurple);
                        shader.SetFloat("_Opacity", 1f);
                        mat.SetPropertyBlock(shader);
                    }

                    //Change to indicated color
                    else if (canBePainted == false)
                    {
                        // if (red == true)
                        // {
                        //     //shader.SetColor("_ShaColor", hor);
                        //     shader.SetFloat("_Opacity", 1f);
                        //     mat.SetPropertyBlock(shader);
                        // }

                        // else if (blue == true)
                        // {
                        //     //shader.SetColor("_ShaColor", ver);
                        //     shader.SetFloat("_Opacity", 1f);
                        //     mat.SetPropertyBlock(shader);
                        // }

                        shader.SetFloat("_Opacity", 1f);
                        mat.SetPropertyBlock(shader);
                    }
                }
            }
        }
        
        //Check for when paintables can have collision when their transparency changes
        //Disable collision
        // if (canBePainted == true)
        // {

        // }

        //Enable collision
        // else if (canBePainted == false)
        // {

        // }

        //Change the entire environment from black and white shader to toon shader at the end of the game after reaching the super brush with all bristles
        if (superBrush.superBrushActive)
        {
            //Affects non-paintables
            if (paintable == false && stickyWall == false && purplePlat == false)
            {
                //For objects that use one mesh renderer
                if (components != null)
                {
                    components.material.shader = toon;
                }
                
                //For objects that use more than one mesh renderer
                else if (components == null)
                {
                    foreach (var mat in bwShader)
                    {
                        mat.material.shader = toon;
                    }
                }
            }

            //components.SetPropertyBlock(bw);
            //Debug.Log("Shader name: " + components.material.shader.name);
        }
    }

    //This is used for testing- The nonpaintable object will turn the object back into its colored form
    //Eventually, everything will be full color at the end of the level
    // void OnCollisionEnter(Collision colorize)
    // {
    //     if (colorize.gameObject.tag == "Red" || colorize.gameObject.tag == "Yellow" || colorize.gameObject.tag == "Blue" || colorize.gameObject.tag == "Grayscale")
    //     {
    //         //For objects that use  one mesh renderer
    //         if (components != null)
    //         {
    //             if (paintable == false)
    //             {
    //                 shader.SetFloat("_Saturation", 1f);
    //                 components.SetPropertyBlock(shader);
    //             }
    //         }

    //         //For objects that use more than one mesh renderer
    //         else if (components == null)
    //         {
    //             foreach (var mat in bwShader)
    //             {
    //                 if (paintable == false)
    //                 {
    //                     shader.SetFloat("_Saturation", 1f);
    //                     mat.SetPropertyBlock(shader);
    //                 }
    //             }
    //         }
    //     }
    // }
}

// foreach (var mat in bwShader)
        // {
        //     mat.SetFloat("_Saturation", 0f); //The non-paintable or paintable object will start off as black & white

        //     //Only make paintable objects
        //     if (paintable == true)
        //     {
        //         mat.SetFloat("_Opacity", 0.5f);
        //         canBePainted = true;
        //     }
        // }

// foreach (var mat in bwShader)
        // {
        //     //Make the specific object semi-transparent to indicate that it needs to be painted
        //     if (paintable == true)
        //     {
        //         if (canBePainted == true)
        //         {
        //             mat.SetFloat("_Opacity", 0.5f);
        //             mat.SetFloat("_Saturation", 0f);
        //         }

        //         //Make the specific object visible and colorized to indicate that has been painted
        //         else if (canBePainted == false)
        //         {
        //             mat.SetFloat("_Opacity", 1f);
        //             mat.SetFloat("_Saturation", 1f);
        //         }
        //     }

        //     //Make the specific object semi-transparent and decolorized to indicate that it needs to be painted
        //     // else if (paintable == true)
        //     // {
        //     //     mat.SetFloat("_Opacity", 1f);
        //     //     mat.SetFloat("_Saturation", 1f);
        //     // }
        // }

//Make the paintable object colorized and visible/black and white and semi-transparent
    // public void Paintable()
    // {
    //     foreach (var mat in bwShader)
    //     {
    //         //Make semi-transparent to indicate that it needs to be painted
    //         if (paintable == true)
    //         {
    //             mat.SetFloat("_Opacity", 0.5f);
    //             mat.SetFloat("_Saturation", 0f);
    //         }

    //         //Make visible and colorized to indicate that has been painted
    //         // else if (paintable == false)
    //         // {
    //         //     mat.SetFloat("_Opacity", 1f);
    //         //     mat.SetFloat("_Saturation", 1f);
    //         // }
    //     }
    // }

    //Make the non-paintable object black and white and not semi-transparent
    // public void NonPaintable()
    // {
    //     foreach (var mat in bwShader)
    //     {
    //         //Make semi-transparent to indicate that it needs to be painted
    //         if (paintable == true)
    //         {
    //             mat.SetFloat("_Opacity", 0.5f);
    //             mat.SetFloat("_Saturation", 0f);
    //         }
    //         //Make semi-transparent to indicate that it needs to be painted
    //     //     if (nonPaintable == true)
    //     //     {
    //     //         //mat.SetFloat("_Opacity", 0.5f);
    //     //         mat.SetFloat("_Saturation", 0f);
    //     //     }

    //     //     //Make visible and colorized to indicate that has been painted
    //     //     else if (nonPaintable == false)
    //     //     {
    //     //         //mat.SetFloat("_Opacity", 1f);
    //     //         mat.SetFloat("_Saturation", 1f);
    //     //     }
    //     }
    // }

    //Change the sticky platform's saturation
        // else if (paintSticky == true)
        // {
        //     //For objects that use one mesh renderer
        //     if (components != null)
        //     {
        //         //Make desaturated to indicate that it needs to be painted
        //         if (canBePainted == true)
        //         {
        //             bw.SetFloat("_Saturation", 1f);
        //             bw.SetFloat("_Opacity", 1f);
        //             components.SetPropertyBlock(bw);
        //         }

        //         //Make olorized to indicate that it has been painted
        //         else if (canBePainted == false)
        //         {
        //             bw.SetFloat("_Saturation", 1f);
        //             bw.SetFloat("_Opacity", 1f);
        //             components.SetPropertyBlock(bw);
        //         }
        //     }

        //     //For objects that use more than one mesh renderer
        //     else if (components == null)
        //     {
        //         foreach (var mat in bwShader)
        //         {
        //             //Make desaturated to indicate that it needs to be painted
        //             if (canBePainted == true)
        //             {
        //                 bw.SetFloat("_Saturation", 1f);
        //                 bw.SetFloat("_Opacity", 1f);
        //                 mat.SetPropertyBlock(bw);
        //             }

        //             //Make visible and colorized to indicate that it has been painted
        //             else if (canBePainted == false)
        //             {
        //                 bw.SetFloat("_Saturation", 1f);
        //                 bw.SetFloat("_Opacity", 1f);
        //                 mat.SetPropertyBlock(bw);
        //             }
        //         }
        //     }
        // }
            // Paintable();
            // NonPaintable();