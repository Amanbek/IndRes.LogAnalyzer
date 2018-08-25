using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using IndRes.LogAnalyzer.Core;
using IndRes.LogAnalyzer.Plugins;
using IndRes.LogAnalyzer.Validation;

using Serilog;
using Serilog.Core;

namespace IndRes.LogAnalyzer.Commands.Commands
{
  public class AnalyzeLogEventsCommand : ICommand
  {
    private readonly ILogReader logReader;

    private readonly ValidationProvider validationProvider;

    private readonly PluginProvider pluginProvider;

    public AnalyzeLogEventsCommand(ILogReader logReader, ValidationProvider validationProvider, PluginProvider pluginProvider)
    {
      this.logReader = logReader;
      this.validationProvider = validationProvider;
      this.pluginProvider = pluginProvider;
    }

    public void Process()
    {
      var events = this.logReader.ReadLog();
      var results = this.validationProvider.Validate(events);

      if (results.Any())
      {
        this.NotifyOnViolation(results);
      }
    }

    private void NotifyOnViolation(IReadOnlyCollection<ValidationResult> results)
    {
      var messages = results
        .Select(r => new NotificationMessage()
        {
          ViolationType = r.Type,
          Message = r.Message
        }).ToArray();

      foreach (var plugin in this.pluginProvider.GetNotificationPlugins())
      {
        plugin.Notify(messages);
      }
    }
  }
}
