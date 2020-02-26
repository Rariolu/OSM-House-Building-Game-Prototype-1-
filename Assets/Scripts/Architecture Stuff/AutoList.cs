using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A list which can add blank elements when necessary.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AutoList<T> : List<T> where T : class
{
    public void AddOutboundElement(int index, T element)
    {
        if(index < Count && index >= 0)
        {
            this[index] = element;
        }
        else if (index >= 0)
        {
            while(Count < index)
            {
                Add(null);
            }
            Add(element);
        }
    }
}