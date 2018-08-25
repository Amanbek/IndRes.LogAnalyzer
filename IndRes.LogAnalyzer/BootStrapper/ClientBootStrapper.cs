using System.Configuration;

using IndRes.LogAnalyzer.Commands;
using IndRes.LogAnalyzer.Core;
using IndRes.LogAnalyzer.Validation;

using StructureMap;

namespace IndRes.LogAnalyzer.BootStrapper
{
  public static class ClientBootStrapper
  {
    public static IContainer Configure()
    {
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
            x.For<ILogReaderConfiguration>().Use<LogReaderConfiguration>().SetProperty(p => p.LogLocation = ConfigurationManager.AppSettings["LogLocation"]);
            x.For<ILogReader>().Use<LogReader>();
            x.For<CommandFactory>().Singleton();
            x.For<CommandProcessor>().Singleton();
            x.For<ValidationProvider>().Use(v => new ValidationProvider(new ValidationConfiguration()));
          });

      return container;
    }
  }
}
