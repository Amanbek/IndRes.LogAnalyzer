using System.Xml.Serialization;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "error")]
  public class Error
  {
    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "message")]
    public string Message { get; set; }
  }
}