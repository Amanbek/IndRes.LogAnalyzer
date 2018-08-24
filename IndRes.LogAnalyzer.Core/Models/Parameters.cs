using System.Xml.Serialization;

namespace IndRes.LogAnalyzer.Core.Models
{
  [XmlRoot(ElementName = "parameters")]
  public class Parameters
  {
    [XmlAttribute(AttributeName = "addressRule")]
    public string AddressRule { get; set; }

    [XmlAttribute(AttributeName = "listenAddress")]
    public string ListenAddress { get; set; }

    [XmlAttribute(AttributeName = "clientVersion")]
    public string ClientVersion { get; set; }

    [XmlAttribute(AttributeName = "kexAlg")]
    public string KexAlg { get; set; }

    [XmlAttribute(AttributeName = "cipherAlgIn")]
    public string CipherAlgIn { get; set; }

    [XmlAttribute(AttributeName = "cipherAlgOut")]
    public string CipherAlgOut { get; set; }

    [XmlAttribute(AttributeName = "macAlgIn")]
    public string MacAlgIn { get; set; }

    [XmlAttribute(AttributeName = "macAlgOut")]
    public string MacAlgOut { get; set; }

    [XmlAttribute(AttributeName = "comprAlgIn")]
    public string ComprAlgIn { get; set; }

    [XmlAttribute(AttributeName = "comprAlgOut")]
    public string ComprAlgOut { get; set; }

    [XmlAttribute(AttributeName = "failureReason")]
    public string FailureReason { get; set; }

    [XmlAttribute(AttributeName = "groupSettings")]
    public string GroupSettings { get; set; }
    [XmlAttribute(AttributeName = "tokenType")]
    public string TokenType { get; set; }

    [XmlAttribute(AttributeName = "tokenLogonType")]
    public string TokenLogonType { get; set; }
  }
}