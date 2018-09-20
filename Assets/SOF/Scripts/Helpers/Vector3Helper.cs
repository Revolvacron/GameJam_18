using UnityEngine;
using System.Collections.Generic;

public static class Vector3Helper
{
    /// <summary>Totals the specified vectors.</summary>
    /// <param name="vectors">The vectors.</param>
    /// <returns>A vector representing the total of the supplied vectors.</returns>
    public static Vector3 Total(this Vector3[] vectors)
    {
        Vector3 total = Vector3.zero;
        foreach (Vector3 v in vectors)
        {
            total += v;
        }
        return total;
    }

    /// <summary>Totals the specified vectors.</summary>
    /// <param name="vectors">The vectors.</param>
    /// <returns>A vector representing the total of the supplied vectors.</returns>
    public static Vector3 Total(this List<Vector3> vectors)
    {
        return Vector3Helper.Total(vectors.ToArray());
    }

    /// <summary>Returns the average (center) of a list of vectors.</summary>
    /// <param name="vectors">The vectors.</param>
    /// <returns>The average.</returns>
    public static Vector3 Average(this Vector3[] vectors)
    {
        return vectors.Total() / vectors.Length;
    }

    /// <summary>Returns the average (center) of a list of vectors.</summary>
    /// <param name="vectors">The vectors.</param>
    /// <returns>The average.</returns>
    public static Vector3 Average(this List<Vector3> vectors)
    {
        return Vector3Helper.Average(vectors.ToArray());
    }

    /// <summary>Determines whether any component of the vector is NaN.</summary>
    /// <param name="vector">The vector.</param>
    /// <returns><c>true</c> if [is NaN] [the specified vector]; otherwise, <c>false</c>.</returns>
    public static bool IsNaN(this Vector3 vector)
    {
        return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
    }

    /// <summary>Returns a vector which is positive in each component.</summary>
    /// <param name="vector">The v.</param>
    /// <returns></returns>
    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

    /// <summary>
    /// Given a vector and an extension, this method will add the extension onto the vector. However,
    /// unlike a normal addition this will add/subtract each component based whether the given vector
    /// is positive or negative in that axis.
    /// </summary>
    /// <param name="vector">The vector to extend.</param>
    /// <param name="extension">The amount to extend the vector by.</param>
    /// <returns>An extented version of vector.</returns>
    public static Vector3 Extend(this Vector3 vector, Vector3 extension)
    {
        Vector3 result = new Vector3();
        result.x = vector.x + (vector.x < 0.0f ? -extension.x : extension.x);
        result.y = vector.y + (vector.y < 0.0f ? -extension.y : extension.y);
        result.z = vector.z + (vector.z < 0.0f ? -extension.z : extension.z);
        return result;
    }

    /// <summary>Returns a copy of the given vector with no y component.</summary>
    /// <param name="v">The vector.</param>
    /// <returns>A new vector with no Y component.</returns>
    public static Vector3 NoYComponent(this Vector3 v)
    {
        return new Vector3(v.x, 0.0f, v.z);
    }

    /// <summary>
    /// Calculates the angle (in degrees) between this and an other vector about a given axis.
    /// </summary>
    /// <param name="vector">This vector.</param>
    /// <param name="other">The other vector.</param>
    /// <param name="axis">The axis of rotation.</param>
    /// <returns>Signed float between -180 and 180 (degrees).</returns>
    public static float AngleAboutAxis(this Vector3 vector, Vector3 other, Vector3 axis)
    {
        var a = Vector3.ProjectOnPlane(vector, axis);
        var b = Vector3.ProjectOnPlane(other, axis);
        return Vector3.SignedAngle(a, b, axis);
    }
}
