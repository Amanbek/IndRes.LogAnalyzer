using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndRes.LogAnalyzer.Commands.Extensions;

namespace IndRes.LogAnalyzer.Commands
{
  public class CommandProcessor
  {
    private Dictionary<ToolCommands, ICommand> _commands = new Dictionary<ToolCommands, ICommand>();

    public CommandProcessor(CommandFactory commandFactory)
    {
      _commands.Add(ToolCommands.ReadLog, commandFactory.CreateLogReaderCommand());
      _commands.Add(ToolCommands.AnalyzeLog, commandFactory.CreateAnalyzeLogCommand());
      _commands.Add(ToolCommands.Exit, commandFactory.CreateApplicationExitCommand());
    }

    public async Task ProcessCommand(string input)
    {
      if (int.TryParse(input, out var command))
      {
        _commands.First(c => c.Key == (ToolCommands)command).Value.Process();
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
