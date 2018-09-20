using UnityEngine;
using System.Collections.Generic;

public static class CameraHelper
{
    public static float HorizontalFieldOfView(this Camera camera)
    {
        var radAngle = camera.fieldOfView * Mathf.Deg2Rad;
        var radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * camera.aspect);
        return Mathf.Rad2Deg * radHFOV;
    }

    public static float CalculateZoomToFit(this Camera camera, Vector3 position, Vector3 border = default(Vector3))
    {
        position = camera.transform.InverseTransformPoint(position).Extend(border);
        
        var x = Mathf.Abs(position.x) / Mathf.Tan(camera.HorizontalFieldOfView() * 0.5f * Mathf.Deg2Rad);
        var y = Mathf.Abs(position.y) / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        return Mathf.Max(x, y) - position.z;
    }

    public static float CalculateZoomToFit(this Camera camera, Vector3[] positions, Vector3 border = default(Vector3))
    {
        float z = float.MinValue;
        foreach (Vector3 p in positions)
        {
            z = Mathf.Max(camera.CalculateZoomToFit(p, border), z);
        }
        return z;
    }

    public static float CalculateZoomToFit(this Camera camera, Bounds bb, Vector3 border = default(Vector3))
    {
        return camera.CalculateZoomToFit(bb.Corners(), border);
    }
}