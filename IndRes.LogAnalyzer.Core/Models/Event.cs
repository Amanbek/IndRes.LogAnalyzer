using System.Xml.Serialization;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "event")]
  public class Event
  {
    [XmlElement(ElementName = "session")]
    public Session Session { get; set; }

    [XmlElement(ElementName = "channel")]
    public Channel Channel { get; set; }

    [XmlAttribute(AttributeName = "seq")]
    public string Sequence { get; set; }

    [XmlAttribute(AttributeName = "time")]
    public string Time { get; set; }

    [XmlAttribute(AttributeName = "app")]
    public string App { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "desc")]
    public string Description { get; set; }

    [XmlElement(ElementName = "parameters")]
    public Parameters Parameters { get; set; }

    [XmlElement(ElementName = "authentication")]
    public Authentication Authentication { get; set; }

    [XmlElement(ElementName = "help")]
    public Help Help { get; set; }

    [XmlElement(ElementName = "error")]
    public Error Error { get; set; }
  }
}