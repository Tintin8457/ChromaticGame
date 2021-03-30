using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipIndicator : MonoBehaviour
{
    public Vector3 xPos;
    public Vector3 yPos;

    //Flip indicator to the x-axis when horizontal platforms are enabled
    public void FlipToX()
    {
        gameObject.transform.localPosition = xPos;
    }

    //Flip indicator to the y-axis when horizontal platforms are enabled
    public void FlipToY()
    {
        gameObject.transform.localPosition = yPos;
    }
}
