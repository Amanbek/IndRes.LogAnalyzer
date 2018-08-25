using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndRes.LogAnalyzer.Core.Extensions
{
  public static class StringExtensions
  {
    public static DateTime ToDateTime(this string datetime)
    {
      return DateTime.TryParse(datetime, out var parsedDateTime)
               ? parsedDateTime
               : DateTime.MinValue;
    }
  }
}
