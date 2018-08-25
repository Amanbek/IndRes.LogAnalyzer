using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using IndRes.LogAnalyzer.Core;
using IndRes.LogAnalyzer.Core.Models;

using Serilog;

namespace IndRes.LogAnalyzer.Commands
{
  public class ReadLogCommand : ICommand
  {
    private readonly ILogReader reader;

    public Action<Event> EventLogAction { get; set; }

    public ReadLogCommand(ILogReader reader)
    {
      this.reader = reader;
    }
    public CommandExecuteResult Process()
    {
      try
      {
        var events = this.reader.ReadLogEvents();
        events.ForEach(this.EventLogAction);
        return new CommandExecuteResult() { Succeeded = true };
      }
      catch (Exception e)
      {
        return new CommandExecuteResult()
        {
          Succeeded = false,
          Message = e.ToString()
        };
      }
    }
  }
}