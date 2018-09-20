using UnityEngine;

/// <summary>
/// A Vector2 style class that uses int explicitly.
/// </summary>
[System.Serializable]
public class IntVector2 : System.Object
{
    /// <summary>The x component.</summary>
    public int x = 0;
    /// <summary>The y component.</summary>
    public int y = 0;

    /// <summary>Initializes a new instance of the <see cref="IntVector2"/> class.</summary>
    /// <param name="x">The x component.</param>
    /// <param name="y">The y component.</param>
    public IntVector2(int x = 0, int y = 0)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>Initializes a new instance of the <see cref="IntVector2"/> class.</summary>
    /// <param name="v">The value to instantiate this instance from..</param>
    public IntVector2(IntVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }

    /// <summary>Performs an implicit conversion from <see cref="Vector2"/> to <see cref="IntVector2"/>.</summary>
    /// <param name="v">The value to create the IntVector2 from.</param>
    /// <returns>The result of the conversion.</returns>
    static public implicit operator IntVector2(Vector2 v)
    {
        return new IntVector2((int)v.x, (int)v.y);
    }

    /// <summary>Performs an implicit conversion from <see cref="IntVector2"/> to <see cref="Vector2"/>.</summary>
    /// <param name="v">The value to create the Vector2 from.</param>
    /// <returns>The result of the conversion.</returns>
    static public implicit operator Vector2(IntVector2 v)
    {
        return new Vector2(v.x, v.y);
    }

    /// <summary>Implements the operator +.</summary>
    /// <param name="a">The a component.</param>
    /// <param name="b">The b component.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.x + b.x, a.y + b.y);
    }

    /// <summary>Implements the operator -.</summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.x - b.x, a.y - b.y);
    }

    /// <summary>Implements the operator *.</summary>
    /// <param name="a">The IntVector3 to scale.</param>
    /// <param name="v">The value to scale the vector by.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector2 operator *(IntVector2 a, float v)
    {
        return new IntVector2((int)(a.x * v), (int)(a.y * v));
    }

    /// <summary>Implements the operator /.</summary>
    /// <param name="a">The IntVector3 to scale.</param>
    /// <param name="v">The value to scale the vector by.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector2 operator /(IntVector2 a, float v)
    {
        return new IntVector2((int)(a.x / v), (int)(a.y / v));
    }

    /// <summary>Implements the operator ==. Compares the x and y components of each vector.</summary>
    /// <param name="a">The a component.</param>
    /// <param name="b">The a component.</param>
    /// <returns>True if the vectors are equal.</returns>
    public static bool operator ==(IntVector2 a, IntVector2 b)
    {
        return a.x == b.x && a.y == b.y;
    }

    /// <summary>Implements the operator !=. Compares the x and y components of each vector.</summary>
    /// <param name="a">The a component.</param>
    /// <param name="b">The a component.</param>
    /// <returns>True if the vectors aren't equal.</returns>
    public static bool operator !=(IntVector2 a, IntVector2 b)
    {
        return a.x != b.x || a.y != b.y;
    }

    /// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
        var item = obj as IntVector2;
        return !System.Object.Equals(item, null) && this.x == item.x && this.y == item.y;
    }

    /// <summary>Returns a hash code for this instance.</summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>Read only magnitude for this vector.</summary>
    public float magnitude
    {
        get
        {
            return Mathf.Sqrt(magnitudeSqr);
        }
    }

    /// <summary>Gets the magnitude squared.</summary>
    public float magnitudeSqr
    {
        get
        {
            return x * x + y * y;
        }
    }

    /// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        return "(" + x.ToString() + " ," + y.ToString() + ")";
    }

    /// <summary>Gets the zero initialised IntVector2.</summary>
    public static IntVector2 zero
    {
        get
        {
            return new IntVector2(0, 0);
        }
    }
}