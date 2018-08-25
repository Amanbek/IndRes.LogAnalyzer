using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using IndRes.LogAnalyzer.Core.Models;

namespace IndRes.LogAnalyzer.Core
{
  public class LogReader : ILogReader
  {
    private readonly ILogReaderConfiguration configuration;

    public LogReader(ILogReaderConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public List<Event> ReadLog()
    {
      XmlRootAttribute xRoot = new XmlRootAttribute();
      xRoot.ElementName = "log";
      xRoot.IsNullable = true;
      XmlSerializer xml = new XmlSerializer(typeof(Log), xRoot);

      return ((Log)xml.Deserialize(new StreamReader(configuration.LogLocation))).Events;
    }
  }
}
