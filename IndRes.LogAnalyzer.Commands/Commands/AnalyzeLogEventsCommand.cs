using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using IndRes.LogAnalyzer.Core;
using IndRes.LogAnalyzer.Validation;

namespace IndRes.LogAnalyzer.Commands.Commands
{
  public class AnalyzeLogEventsCommand : ICommand
  {
    private readonly ILogReader logReader;

    private readonly ValidationProvider validationProvider;

    public AnalyzeLogEventsCommand(ILogReader logReader, ValidationProvider validationProvider)
    {
      this.logReader = logReader;
      this.validationProvider = validationProvider;
    }

    public void Process()
    {
      var events = logReader.ReadLog();
      var results = validationProvider.Validate(events);

      if (results.Any(r => r.IsSuspicious))
      {
        NotifyOnViolation(results);
      }
    }

    private void NotifyOnViolation(IEnumerable<ValidationResult> results)
    {
      var pluginName = ConfigurationManager.AppSettings["notofication-plugin"];
      INotificationPlugin plugin = (INotificationPlugin) Activator.CreateInstance(Type.GetType(pluginName));
      
      plugin.Notify(results.ToArray());
    }
  }

  internal interface INotificationPlugin
  {
    void Notify(ValidationResult[] validationresults);
  }
}
