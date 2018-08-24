using System.Xml.Serialization;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "authentication")]
  public class Authentication
  {
    [XmlAttribute(AttributeName = "attemptNr")]
    public string AttemptNr { get; set; }

    [XmlAttribute(AttributeName = "userName")]
    public string UserName { get; set; }

    [XmlAttribute(AttributeName = "method")]
    public string Method { get; set; }

    [XmlAttribute(AttributeName = "windowsAccount")]
    public string WindowsAccount { get; set; }
  }
}