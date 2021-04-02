using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only works with the pipeline asset applied in the graphics portion in project settings
public class ShaderBW : MonoBehaviour
{
    public Material[] bwShader; //Attch all necessary materials for the shader and change the size in inspector
    //public bool canDestroy; //This is just for demo

    //public MeshRenderer shaderBw;
    //public Shader bw;
    //public Vector4 satInput;
    //public float saturation; //Change the saturation in the game

    // Start is called before the first frame update
    void Start()
    {
        //bwShader.SetVector("RangeSaturation", new Vector4(1f, 0f, 0f, 0f));
        //Debug.Log(bw.GetPropertyName(1));
        //bwShader.SetFloat("_Saturation", 1f);
        //shaderBw.material.SetFloat("_Saturation", 1f);
        
        //This object will start off as a black & white object
        foreach (var mat in bwShader)
        {
            mat.SetFloat("_Saturation", 0f);
        }

        //canDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        //bwShader.GetVector("_Saturation");

        //Destroy for demo only
        // if (canDestroy == true)
        // {
        //     StartCoroutine("WaitToDestroy");
        // }
    }

    //The object will turn the object back into its colored form
    void OnCollisionEnter(Collision colorize)
    {
        if (colorize.gameObject.tag == "Red" || colorize.gameObject.tag == "Yellow" || colorize.gameObject.tag == "Blue" || colorize.gameObject.tag == "Grayscale")
        {
            foreach (var mat in bwShader)
            {
                mat.SetFloat("_Saturation", 1f);
            }

            //canDestroy = true;
        }
    }

    // IEnumerator WaitToDestroy()
    // {
    //     yield return new WaitForSeconds(7f);
    //     Destroy(gameObject);
    // }
}
