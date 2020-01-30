using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public struct Task
{
    public int number;
    public TASKTYPE type;
    public override string ToString()
    {
        return "{0}: {1};".Format(type, number);
    }
}