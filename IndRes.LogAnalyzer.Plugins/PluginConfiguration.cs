using System.Configuration;

namespace IndRes.LogAnalyzer.Plugins
{
  public class PluginConfiguration
  {
    public string PluginLocation => ConfigurationManager.AppSettings["notification-plugin-location"];
  }
}
