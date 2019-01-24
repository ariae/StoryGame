using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]

public class LaunchArcRenderer : MonoBehaviour
{
    LineRenderer lr;

    public float velocity;  //Arc velocity
    public float angle;     //Arc angle
    public int resolution;  //Resolution of arc line

    float g;                //Force of gravity on the Y axis
    float radianAngle;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y); //Get absolute gravity
    }


    void Start()
    {
        RenderArc();

        //MaxDistance
        //Velocity
        //Angle
        //Gravity
    }

    void RenderArc() //Assigning settings to RenderArc (LineRender)
    {
        lr.SetVertexCount (resolution + 1);
        lr.SetPositions(CalculateArcArray());

    }


    //Create array of vector3 positions for RenderArc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
    }
}
