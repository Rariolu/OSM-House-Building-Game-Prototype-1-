using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public struct Standard
{
    public double rating;
    public STANDARDTYPE type;
    public override string ToString()
    {
        return "{0}: {1};".Format(type, rating);
    }
}