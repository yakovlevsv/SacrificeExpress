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

    public static void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
