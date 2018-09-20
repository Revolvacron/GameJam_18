using UnityEngine;

/// <summary>
/// A Vector3 style class that uses int explicitly.
/// </summary>
[System.Serializable]
public class IntVector3 : System.Object
{
    /// <summary>The x component.</summary>
    public int x = 0;
    /// <summary>The y component.</summary>
    public int y = 0;
    /// <summary>The z component.</summary>
    public int z = 0;

    /// <summary>Initializes a new instance of the <see cref="IntVector3"/> class.</summary>
    /// <param name="x">The x component.</param>
    /// <param name="y">The y component.</param>
    /// <param name="z">The z component.</param>
    public IntVector3(int x = 0, int y = 0 , int z = 0)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>Initializes a new instance of the <see cref="IntVector3"/> class.</summary>
    /// <param name="v">The value to instantiate this instance from..</param>
    public IntVector3(IntVector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    /// <summary>Performs an implicit conversion from <see cref="Vector3"/> to <see cref="IntVector3"/>.</summary>
    /// <param name="v">The value to create the IntVector3 from.</param>
    /// <returns>The result of the conversion.</returns>
    static public implicit operator IntVector3(Vector3 v)
    {
        return new IntVector3((int)v.x, (int)v.y, (int)v.z);
    }

    /// <summary>Performs an implicit conversion from <see cref="IntVector3"/> to <see cref="Vector3"/>.</summary>
    /// <param name="v">The value to create the Vector3 from.</param>
    /// <returns>The result of the conversion.</returns>
    static public implicit operator Vector3(IntVector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    /// <summary>Implements the operator +.</summary>
    /// <param name="a">The a component.</param>
    /// <param name="b">The b component.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector3 operator +(IntVector3 a, IntVector3 b)
    {
        return new IntVector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    /// <summary>Implements the operator -.</summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector3 operator -(IntVector3 a, IntVector3 b)
    {
        return new IntVector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    
    /// <summary>Implements the operator *.</summary>
    /// <param name="a">The IntVector3 to scale.</param>
    /// <param name="v">The value to scale the vector by.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector3 operator *(IntVector3 a, float v)
    {
        return new IntVector3((int)(a.x * v), (int)(a.y * v), (int)(a.z * v));
    }
    
    /// <summary>Implements the operator /.</summary>
    /// <param name="a">The IntVector3 to scale.</param>
    /// <param name="v">The value to scale the vector by.</param>
    /// <returns>The result of the operation.</returns>
    public static IntVector3 operator /(IntVector3 a, float v)
    {
        return new IntVector3((int)(a.x / v), (int)(a.y / v), (int)(a.z / v));
    }

    /// <summary>Implements the operator ==. Compares the x, y and z components of each vector.</summary>
    /// <param name="a">The a component.</param>
    /// <param name="b">The a component.</param>
    /// <returns>True if the vectors are equal.</returns>
    public static bool operator ==(IntVector3 a, IntVector3 b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    /// <summary>Implements the operator !=. Compares the x, y and z components of each vector.</summary>
    /// <param name="a">The a component.</param>
    /// <param name="b">The a component.</param>
    /// <returns>True if the vectors aren't equal.</returns>
    public static bool operator !=(IntVector3 a, IntVector3 b)
    {
        return a.x != b.x || a.y != b.y || a.z != b.z;
    }

    /// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
        var item = obj as IntVector3;
        return item != null && this.x == item.x && this.y == item.y && this.z == item.z;
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
            return x * x + y * y + z * z;
        }
    }
    
    /// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        return "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ")";
    }

    /// <summary>Gets the zero initialised IntVector2.</summary>
    public static IntVector3 zero
    {
        get
        {
            return new IntVector3(0, 0, 0);
        }
    }
}