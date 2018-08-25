using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IndRes.LogAnalyzer.Plugins
{
  public class PluginProvider
  {
    private readonly IPluginConfiguration pluginConfiguration;

    public PluginProvider(IPluginConfiguration pluginConfiguration)
    {
      this.pluginConfiguration = pluginConfiguration;
    }
    public IEnumerable<INotificationPlugin> GetNotificationPlugins()
    {
      var pluginCollection = new List<INotificationPlugin>();

      if (string.IsNullOrWhiteSpace(this.pluginConfiguration.PluginLocation))
      {
        return pluginCollection;
      }

      foreach (var pluginType in this.GetPluginTypes())
      {
        try
        {
          var notificationPlugin = Activator.CreateInstance(pluginType) as INotificationPlugin;
          pluginCollection.Add(notificationPlugin);
        }
        catch
        {
        }
      }

      return pluginCollection;
    }

    private IEnumerable<Type> GetPluginTypes()
    {
      Assembly.LoadFrom(this.pluginConfiguration.PluginLocation);
      var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
      var allTypes = allAssemblies.SelectMany(a => a.GetTypes());
      return allTypes.Where(t => t.GetInterface(nameof(INotificationPlugin)) != null);
    }
  }
}
