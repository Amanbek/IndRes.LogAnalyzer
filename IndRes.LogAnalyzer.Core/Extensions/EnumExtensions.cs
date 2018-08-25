using System;
using System.ComponentModel;

namespace IndRes.LogAnalyzer.Commands.Extensions
{
  public static class EnumExtensions
  {
    public static string GetDescription(this Enum value)
    {
      var info = value.GetType().GetField(value.ToString());
      var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

      return attributes.Length <= 0
               ? value.ToString()
               : attributes[0].Description;
    }
  }
}
