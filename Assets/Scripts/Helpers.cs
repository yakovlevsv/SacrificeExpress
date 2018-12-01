using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers {

    public static float GetDistance(Vector3 p1, Vector3 p2)
    {
        Vector3 distance = p1 - p2;
        distance.y = 0;
        return distance.magnitude;
    }
}
