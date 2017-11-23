using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, float t)
    {
        Vector3 B = Vector3.zero;
        float t1 = (1 - t);
        B = t1 * P0 + P1 * t;
        return B;
    }

    public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, float t)
    {
        Vector3 B = Vector3.zero;
        float t1 = (1 - t) * (1 - t);
        float t2 = t * (1 - t);
        float t3 = t * t;
        B = P0 * t1 + 2 * t2 * P1 + t3 * P2;
        return B;
    }

    public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
    {
        Vector3 B = Vector3.zero;
        float t1 = (1 - t) * (1 - t) * (1 - t);
        float t2 = (1 - t) * (1 - t) * t;
        float t3 = t * t * (1 - t);
        float t4 = t * t * t;
        B = P0 * t1 + 3 * t2 * P1 + 3 * t3 * P2 + P3 * t4;
        return B;
    }
}

