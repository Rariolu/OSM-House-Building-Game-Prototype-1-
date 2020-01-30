using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A class to use instead of the regular "System.Text.StringBuilder"
/// because this allows someone to use string formatting and append
/// a new line simultaneously (which is far more intuitive).
/// </summary>
public class StringBuilderPro
{
    StringBuilder sb = new StringBuilder();
    public void AppendLine(string text)
    {
        sb.AppendLine(text);
    }
    /// <summary>
    /// Append string format with values followed by newline.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public void AppendLineFormat(string format, params object[] args)
    {
        sb.AppendFormat(format + "\n", args);
    }
    public override string ToString()
    {
        return sb.ToString();
    }
}