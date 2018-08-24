using System.Xml.Serialization;
using System.Collections.Generic;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "log", DataType = "string")]
  public class Log
  {
    [XmlElement(ElementName = "event")]
    public List<Event> Events { get; set; }
  }
}
