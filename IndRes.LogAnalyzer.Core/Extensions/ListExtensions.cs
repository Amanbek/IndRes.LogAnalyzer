using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IndRes.LogAnalyzer.Core.Models;

namespace IndRes.LogAnalyzer.Core.Extensions
{
  public static class ListExtensions
  {
    public static List<IGrouping<string, Event>> ToSessionList(this List<Event> events)
    {
      return events.GroupBy(e => e.Session.RemoteAddress).ToList();
    }
  }
}
