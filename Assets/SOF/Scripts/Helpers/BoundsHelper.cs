using UnityEngine;

public static class BoundsHelper
{
    public static Vector3[] Corners(this Bounds bb)
    {
        Vector3[] corners = new Vector3[8];
        corners[0] = bb.min;
        corners[1] = bb.max;
        corners[2] = new Vector3(corners[0].x, corners[0].y, corners[1].z);
        corners[3] = new Vector3(corners[0].x, corners[1].y, corners[0].z);
        corners[4] = new Vector3(corners[1].x, corners[0].y, corners[0].z);
        corners[5] = new Vector3(corners[0].x, corners[1].y, corners[1].z);
        corners[6] = new Vector3(corners[1].x, corners[0].y, corners[1].z);
        corners[7] = new Vector3(corners[1].x, corners[1].y, corners[0].z);
        return corners;
    }
}