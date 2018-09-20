using UnityEngine;

public static class MatrixHelper
{
    /// <summary>
    /// Extract translation from transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix.</param>
    /// <returns>Translation offset.</returns>
    public static Vector3 ExtractTranslation(this Matrix4x4 matrix)
    {
        Vector3 translate = new Vector3(matrix.m03, matrix.m13, matrix.m23);
        return translate;
    }

    /// <summary>
    /// Extract rotation quaternion from transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix.</param>
    /// to improve performance; no changes will be made to it.</param>
    /// <returns>Quaternion representation of rotation transform.</returns>
    public static Quaternion ExtractRotation(this Matrix4x4 matrix)
    {
        var forward = matrix.ExtractForward();
        var upwards = matrix.ExtractUp();
        return Quaternion.LookRotation(forward, upwards);
    }

    /// <summary>
    /// Extract scale from transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix.</param>
    /// <returns>Scale vector.</returns>
    public static Vector3 ExtractScale(this Matrix4x4 matrix)
    {
        Vector3 scale;
        scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
        scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
        scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
        return scale;
    }

    /// <summary>
    /// Extract the forward direction from the transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix.</param>
    /// <returns>Forward direction.</returns>
    public static Vector3 ExtractForward(this Matrix4x4 matrix)
    {
        return new Vector3(matrix.m02, matrix.m12, matrix.m22);
    }

    /// <summary>
    /// Extract the up direction from the transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix.</param>
    /// <returns>Up direction.</returns>
    public static Vector3 ExtractUp(this Matrix4x4 matrix)
    {
        return new Vector3(matrix.m01, matrix.m11, matrix.m21);
    }
}
