using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// An extension of "Stack" that allows you to see
/// the most recently added (and second most recently added)
/// values.
/// </summary>
/// <typeparam name="T">The type of the values stored in the stack.</typeparam>
public class ViewableStack<T> : Stack<T>
{
    /// <summary>
    /// Most recent element to be added.
    /// </summary>
    /// <returns></returns>
    public T Top()
    {
        T val = Pop();
        Push(val);
        return val;
    }

    /// <summary>
    /// Second most recent element to be added.
    /// </summary>
    /// <returns></returns>
    public T Previous()
    {
        T val1 = Pop();
        T val2 = Pop();
        Push(val2);
        Push(val1);
        return val2;
    }
}