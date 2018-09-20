using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A 2D array designed to be serializable by unity by indexing with 2D coordinates but 
/// actually storing the information in a one dimensional array.
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class SerializableArray2D<T> : System.Object, IEnumerable<T> where T : class
{
    [SerializeField]
    [HideInInspector]
    private T[] _data;
    [SerializeField]
    [HideInInspector]
    private int _width;
    /// <summary>Read only width of the array.</summary>
    public int width { get { return _width; } }
    [SerializeField]
    [HideInInspector]
    private int _height;
    /// <summary>Read only height of the array.</summary>
    public int height { get { return _height; } }

    /// <summary>Initializes a new instance of the <see cref="SerializableArray2D{T}"/> class.</summary>
    /// <param name="width">The width of the array.</param>
    /// <param name="height">The height of the array.</param>
    public SerializableArray2D(int width, int height, T initialValue = default(T))
    {
        _width = width;
        _height = height;
        _data = new T[_width * _height];
        Reset(ref initialValue);
    }

    /// <summary>
    /// Reset all the items in the array to a copy of the supplied value.
    /// </summary>
    /// <param name="value">The value to use for each element in the array.</param>
    public void Reset(ref T value)
    {
        for (int i = 0; i < _data.Length; ++i)
        {
            if (value != null)
                _data[i] = CopyHelper.DeepCopy(value);
            else
                _data[i] = null;
        }
    }

    /// <summary>Determines whether [is in bounds] [the specified i].</summary>
    /// <param name="i">The i component.</param>
    /// <param name="j">The j component.</param>
    /// <returns><c>true</c> if [is in bounds] [the specified i]; otherwise, <c>false</c>.</returns>
    public bool IsInBounds(int i, int j)
    {
        return IndexTo1DIndex(i, j) != -1;
    }

    /// <summary>Converts a 2D index to a one dimensional index into the arary.</summary>
    /// <param name="i">The i component.</param>
    /// <param name="j">The j component.</param>
    /// <returns>The index into the 1D array or -1 if the index is invalid.</returns>
    private int IndexTo1DIndex(int i, int j)
    {
        if (i < 0 || j < 0 || i >= width || j >= height)
            return -1;
        else
            return i + j * width;
    }

    /// <summary>
    /// Gets or sets the <see cref="T"/> value in the given coordinate of the array.
    /// Can throw an out of bounds exception.
    /// </summary>
    /// <param name="i">The i component.</param>
    /// <param name="j">The j component.</param>
    public T this[int i, int j]
    {
        get
        {
            return _data[IndexTo1DIndex(i, j)];
        }
        set
        {
            _data[IndexTo1DIndex(i, j)] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var v in _data)
        {
            yield return v;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    { 
        // call the generic version of the method
        return GetEnumerator();
    }
}