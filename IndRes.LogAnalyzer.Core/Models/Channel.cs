using System.Xml.Serialization;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "channel")]
  public class Channel
  {
    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }
    [XmlAttribute(AttributeName = "originAddress")]
    public string OriginAddress { get; set; }
    [XmlAttribute(AttributeName = "requestAddress")]
    public string RequestAddress { get; set; }
    [XmlAttribute(AttributeName = "targetAddress")]
    public string TargetAddress { get; set; }
  }
}