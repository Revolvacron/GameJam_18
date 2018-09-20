using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A container for two values of varying types.
/// </summary>
/// <typeparam name="First">The type of thef first item.</typeparam>
/// <typeparam name="Second">The type of the second item.</typeparam>
public class Pair<First, Second> : ICloneable, IEquatable<Pair<First, Second>>
{
    /// <summary>The first item.</summary>
    public First first;
    /// <summary>The second item.</summary>
    public Second second;

    /// <summary>Initializes a new instance of the <see cref="Pair{First, Second}"/> class.</summary>
    /// <param name="first">The first item.</param>
    /// <param name="second">The second item.</param>
    public Pair(First first, Second second)
    {
        this.first = first;
        this.second = second;
    }

    #region IEquatable
    public bool Equals(Pair<First, Second> other)
    {
        return EqualityComparer<First>.Default.Equals(first, other.first) &&
            EqualityComparer<Second>.Default.Equals(second, other.second);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Pair<First, Second>))
            return false;

        return Equals(obj as Pair<First, Second>);
    }

    public static bool operator ==(Pair<First, Second> x, Pair<First, Second> y)
    {
        return x.Equals(y);
    }

    public static bool operator !=(Pair<First, Second> x, Pair<First, Second> y)
    {
        return !(x == y);
    }

    public static bool operator <(Pair<First, Second> x, Pair<First, Second> y)
    {
        var result = Comparer<First>.Default.Compare(x.first, y.first);
        if (result < 0)
        {
            return true;
        }
        else if (result == 0)
        {
            return Comparer<Second>.Default.Compare(x.second, y.second) < 0;
        }
        else
        {
            return false;
        }
    }

    public static bool operator >(Pair<First, Second> x, Pair<First, Second> y)
    {
        var result = Comparer<First>.Default.Compare(x.first, y.first);
        if (result > 0)
        {
            return true;
        }
        else if (result == 0)
        {
            return Comparer<Second>.Default.Compare(x.second, y.second) > 0;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion

    #region ICloneable
    Pair<First, Second> Clone()
    {
        return (Pair<First, Second>)MemberwiseClone();
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
    #endregion
}
