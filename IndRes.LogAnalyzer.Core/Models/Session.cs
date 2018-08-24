using System.Xml.Serialization;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "session")]
  public class Session
  {
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
    [XmlAttribute(AttributeName = "service")]
    public string Service { get; set; }

    [XmlAttribute(AttributeName = "remoteAddress")]
    public string RemoteAddress { get; set; }

    [XmlAttribute(AttributeName = "windowsAccount")]
    public string WindowsAccount { get; set; }
  }
}