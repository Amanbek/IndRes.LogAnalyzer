using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndRes.LogAnalyzer.Commands.Extensions;

using Serilog;

namespace IndRes.LogAnalyzer.Commands
{
  public class CommandProcessor
  {
    private readonly ILogger logger;

    private readonly Dictionary<ToolCommands, ICommand> _commands = new Dictionary<ToolCommands, ICommand>();

    public CommandProcessor(CommandFactory commandFactory, ILogger logger)
    {
      this.logger = logger;
      _commands.Add(ToolCommands.ReadLog, commandFactory.CreateLogReaderCommand());
      _commands.Add(ToolCommands.AnalyzeLog, commandFactory.CreateAnalyzeLogCommand());
      _commands.Add(ToolCommands.Exit, commandFactory.CreateApplicationExitCommand());
    }

    public async Task ProcessCommand(string input)
    {
      if (int.TryParse(input, out var command))
      {
        var result = _commands.First(c => c.Key == (ToolCommands)command).Value.Process();

        if (result.Succeeded == false)
        {
          this.logger.Error(result.Message);
        }
      }
      else
      {
        Console.WriteLine("Command not found.");
      }
    }

    public List<string> GetOptions()
    {
      return _commands.Select(c => $"{(int)c.Key}. {c.Key.GetDescription()}").ToList();
    }
  }
}
