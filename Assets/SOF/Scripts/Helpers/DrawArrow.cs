using UnityEngine;

/// <summary>
/// Based on  https://forum.unity3d.com/threads/debug-drawarrow.85980/
/// </summary>
public static class DrawArrow
{
    public static void Gizmo(Vector3 position, Vector3 direction, float arrowLength = 1.0f, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawRay(position, direction * arrowLength);
        foreach (var barb in GetArrowheadBarbPositions(direction, arrowHeadLength, arrowHeadAngle))
        {
            Gizmos.DrawRay(position + direction * arrowLength, barb);
        }
    }

    public static void Debug(Vector3 position, Vector3 direction, float arrowLength = 1.0f, Color color=default(Color), float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        UnityEngine.Debug.DrawRay(position, direction * arrowLength, color);
        foreach (var barb in GetArrowheadBarbPositions(direction.normalized, arrowHeadLength, arrowHeadAngle))
        {
            UnityEngine.Debug.DrawRay(position + direction * arrowLength, barb, color);
        }
    }

    private static Vector3[] GetArrowheadBarbPositions(Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        return new Vector3[4] {
            (Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength,
            (Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength,
            (Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength,
            (Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength
        };
    }
}