using System.Collections.Generic;

using IndRes.LogAnalyzer.Core.Models;

namespace IndRes.LogAnalyzer.Core
{
  public interface ILogReader
  {
    List<Event> ReadLog();
  }
}
