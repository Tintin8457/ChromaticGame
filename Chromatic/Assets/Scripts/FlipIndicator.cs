using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipIndicator : MonoBehaviour
{
    public Vector3 xPos;
    public Vector3 yPos;

    //Flip indicator to the x-axis when horizontal platforms are enabled
    public void FlipToX(Vector3 newXpos)
    {
        //gameObject.transform.localPosition = xPos;
        gameObject.transform.localPosition = newXpos;
    }

    //Flip indicator to the y-axis when horizontal platforms are enabled
    public void FlipToY(Vector3 newYpos)
    {
        //gameObject.transform.localPosition = yPos;
        gameObject.transform.localPosition = newYpos;
    }
}
