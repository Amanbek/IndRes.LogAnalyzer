using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using IndRes.LogAnalyzer.Core.Models;

namespace IndRes.LogAnalyzer.Core.LogReader
{
  public class LogReader : ILogReader
  {
    private readonly ILogReaderConfiguration configuration;

    public LogReader(ILogReaderConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public List<Event> ReadLogEvents()
    {
      var reader = new StreamReader(this.configuration.LogLocation);
      return this.Deserialize(reader).Events;
    }

    public Log Deserialize(TextReader reader)
    {
      var xRoot = new XmlRootAttribute
      {
        ElementName = "log",
        IsNullable = true
      };
      var xml = new XmlSerializer(typeof(Log), xRoot);

      return (Log)xml.Deserialize(reader);
    }
  }
}
