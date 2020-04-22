
using System;
using System.Collections;

public class SuperRand : Random
{
    public double NextDouble(double min, double max)
    {
        return min + (NextDouble() * (max - min));
    }
}