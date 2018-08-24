using System.Xml.Serialization;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "help")]
  public class Help
  {
    [XmlAttribute(AttributeName = "message")]
    public string Message { get; set; }
  }
}