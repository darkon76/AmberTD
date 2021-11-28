using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BezierCurveWrapper
{
    public BezierCurve Curve;
    public float[] Lengths;

    public BezierCurveWrapper(BezierCurve curve)
    {
        Curve = curve;
        var lengths = Curve.pointCount - 1;
        Lengths = new float[lengths];
        for (int i = 0; i < lengths; i++)
        {
            Lengths[i] = BezierCurve.ApproximateLength(curve[i], curve[i + 1]);
        }
        

    }


}
