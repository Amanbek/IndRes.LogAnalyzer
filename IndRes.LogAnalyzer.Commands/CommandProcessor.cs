using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IndRes.LogAnalyzer.Commands.Extensions;

namespace IndRes.LogAnalyzer.Commands
{
  public class CommandProcessor
  {
    public CommandProcessor(CommandFactory commandFactory)
    {
      commands.Add(ToolCommands.ReadLog, commandFactory.CreateLogReaderCommand());
      commands.Add(ToolCommands.AnalyzeLog, commandFactory.CreateAnalyzeLogCommand());
      commands.Add(ToolCommands.Exit, commandFactory.CreateApplicationExitCommand());
    }

    private Dictionary<ToolCommands, ICommand> commands = new Dictionary<ToolCommands, ICommand>();

    public async Task ProcessCommand(string input)
    {
      if (int.TryParse(input, out var command))
      {
        this.commands.First(c => c.Key == (ToolCommands)command).Value.Process();
      }
      else
      {
        Console.WriteLine("Command not found.");
      }
    }

    public List<string> GetOptions()
    {
      return commands
        .Select(c =>
          {
            var toolCommand = (ToolCommands)c.Key;
            return $"{(int)toolCommand}. {EnumExtensions.GetDescription(c.Key)}";
          })
        .OrderBy(c => c)
        .ToList();
    }
  }
}
