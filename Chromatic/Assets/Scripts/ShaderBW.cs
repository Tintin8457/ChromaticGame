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
    public bool paintable; //Change its value in inspector to indicate what can be semi-transparent and desaturated
    public bool canBePainted; //Change shader look through platforms

    [Header("Sticky Platforms")]
    public bool stickyWall; //Prevents the sticky wall from being semi-transparent
    public bool paintSticky; //Can only paint sticky platforms
    
    private MaterialPropertyBlock bw; //Use to reference the bw shader graph properties
    MeshRenderer components; //Contains all colored elements of the object

    //public bool nonPaintable; //Change its value in inspector to indicate what can't be semi-transparent and desaturated

    // Start is called before the first frame update
    void Start()
    {
        bw = new MaterialPropertyBlock(); //Set up a new shader property that allows the bw shader to work properly
        
        components = GetComponent<MeshRenderer>(); //Get the object's renderer

        //For objects that use one mesh renderer
        if (components != null)
        {
            //The non-paintable or paintable object will start off as black & white
            bw.SetFloat("_Saturation", 0f);
            components.SetPropertyBlock(bw);

            //Only make paintable objects semi-transparent and paintable
            if (paintable == true)
            {
                bw.SetFloat("_Opacity", 0.5f);
                components.SetPropertyBlock(bw);
                canBePainted = true;
            }

            //Make the sticky wall paintable and visible
            else if (stickyWall == true)
            {
                bw.SetFloat("_Opacity", 1f);
                components.SetPropertyBlock(bw);
                canBePainted = true;
            }
        }
        
        //For objects that use more than one mesh renderer
        else if (components == null)
        {
            //Get the mesh renderer for any object has more than one material
            foreach (var mat in bwShader)
            {
                //The non-paintable or paintable object will start off as black & white
                bw.SetFloat("_Saturation", 0f);
                mat.SetPropertyBlock(bw);

                //Only make paintable objects
                if (paintable == true)
                {
                    bw.SetFloat("_Opacity", 0.5f);
                    mat.SetPropertyBlock(bw);
                    canBePainted = true;
                }

                //Make the sticky wall paintable and visible
                else if (stickyWall == true)
                {
                    bw.SetFloat("_Opacity", 1f);
                    mat.SetPropertyBlock(bw);
                    canBePainted = true;
                }  
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Change the paintable's transparency and saturation
        if (paintable == true)
        {
            //For objects that use one mesh renderer
            if (components != null)
            {
                //Make semi-transparent and desaturated to indicate that it needs to be painted
                if (canBePainted == true)
                {
                    bw.SetFloat("_Opacity", 0.5f);
                    bw.SetFloat("_Saturation", 0f);
                    components.SetPropertyBlock(bw);
                }

                //Make visible and colorized to indicate that it has been painted
                else if (canBePainted == false)
                {
                    bw.SetFloat("_Opacity", 1f);
                    bw.SetFloat("_Saturation", 1f);
                    components.SetPropertyBlock(bw);
                }
            }

            //For objects that use more than one mesh renderer
            else if (components == null)
            {
                foreach (var mat in bwShader)
                {
                    //Make semi-transparent and desaturated to indicate that it needs to be painted
                    if (canBePainted == true)
                    {
                        bw.SetFloat("_Opacity", 0.5f);
                        bw.SetFloat("_Saturation", 0f);
                        mat.SetPropertyBlock(bw);
                    }

                    //Make visible and colorized to indicate that it has been painted
                    else if (canBePainted == false)
                    {
                        bw.SetFloat("_Opacity", 1f);
                        bw.SetFloat("_Saturation", 1f);
                        mat.SetPropertyBlock(bw);
                    }
                }
            }
        }

        //Change the sticky platform's saturation
        else if (paintSticky == true)
        {
            //For objects that use one mesh renderer
            if (components != null)
            {
                //Make desaturated to indicate that it needs to be painted
                if (canBePainted == true)
                {
                    bw.SetFloat("_Saturation", 0f);
                    components.SetPropertyBlock(bw);
                }

                //Make olorized to indicate that it has been painted
                else if (canBePainted == false)
                {
                    bw.SetFloat("_Saturation", 1f);
                    components.SetPropertyBlock(bw);
                }
            }

            //For objects that use more than one mesh renderer
            else if (components == null)
            {
                foreach (var mat in bwShader)
                {
                    //Make desaturated to indicate that it needs to be painted
                    if (canBePainted == true)
                    {
                        bw.SetFloat("_Saturation", 0f);
                        mat.SetPropertyBlock(bw);
                    }

                    //Make visible and colorized to indicate that it has been painted
                    else if (canBePainted == false)
                    {
                        bw.SetFloat("_Saturation", 1f);
                        mat.SetPropertyBlock(bw);
                    }
                }
            }
        }
            // Paintable();
            // NonPaintable();
    }

    void OnCollisionEnter(Collision colorize)
    {
        //This is used for testing- The nonpaintable object will turn the object back into its colored form
        //Eventually, everything will be full color at the end of the level
        if (colorize.gameObject.tag == "Red" || colorize.gameObject.tag == "Yellow" || colorize.gameObject.tag == "Blue" || colorize.gameObject.tag == "Grayscale")
        {
            //For objects that use  one mesh renderer
            if (components != null)
            {
                if (paintable == false)
                {
                    bw.SetFloat("_Saturation", 1f);
                    components.SetPropertyBlock(bw);
                }
            }

            //For objects that use more than one mesh renderer
            else if (components == null)
            {
                foreach (var mat in bwShader)
                {
                    if (paintable == false)
                    {
                        bw.SetFloat("_Saturation", 1f);
                         mat.SetPropertyBlock(bw);
                    }
                }
            }
        }
    }
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