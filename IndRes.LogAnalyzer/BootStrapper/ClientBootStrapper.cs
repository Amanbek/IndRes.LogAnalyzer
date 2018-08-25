using System.Configuration;

using IndRes.LogAnalyzer.Commands;
using IndRes.LogAnalyzer.Commands.Commands;
using IndRes.LogAnalyzer.Core;
using IndRes.LogAnalyzer.Core.LogReader;
using IndRes.LogAnalyzer.Plugins;
using IndRes.LogAnalyzer.Validation;

using Serilog;

using StructureMap;

namespace IndRes.LogAnalyzer.BootStrapper
{
  public static class ClientBootStrapper
  {
    public static IContainer Configure()
    {
      var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

      var container = new Container(
        _ =>
          {
            _.Scan(x =>
              {
                x.AssembliesAndExecutablesFromApplicationBaseDirectory();
                x.WithDefaultConventions();
                x.AddAllTypesOf<ICommand>();
              });
          });

      container.Configure(
        x =>
          {
            x.For<ILogger>().Use(l => logger);
            x.For<ILogReaderConfiguration>().Use<LogReaderConfiguration>()
              .SetProperty(p => p.LogLocation = ConfigurationManager.AppSettings["LogLocation"]);
            x.For<ILogReader>().Use<LogReader>();
            x.For<CommandFactory>().Singleton();
            x.For<CommandProcessor>().Singleton();
            x.For<ValidationProvider>().Use(v => new ValidationProvider(new ValidationConfiguration()
            {
              UserNameAttemptsLimit = ClientBootStrapper.TryGetSetting("allowed-username-attempts"),
              SameUsernameAttemptsPeriodLimit = ClientBootStrapper.TryGetSetting("same-username-attempts-period"),
              MultipleUsernameAttemptsPeriodLimit = ClientBootStrapper.TryGetSetting("multiple-username-attempts-period")
            }));
            x.For<IPluginConfiguration>().Use<PluginConfiguration>()
              .SetProperty(p => p.PluginLocation = ConfigurationManager.AppSettings["notification-plugin-location"]);
            x.For<ReadLogCommand>().Use<ReadLogCommand>().SetProperty(p =>
              p.EventLogAction = e => logger.Information($"{e.Time}, {e.Name}, {e.Authentication?.UserName}"));
          });

      return container;
    }

    private static int TryGetSetting(string settingName)
    {
      return int.TryParse(ConfigurationManager.AppSettings[settingName], out var setting)
               ? setting
               : int.MaxValue;
    }
  }
}
