using System;
using System.Threading.Tasks;

namespace IndRes.LogAnalyzer.Commands.Commands
{
  public class ApplicationExitCommand : ICommand
  {
    public CommandExecuteResult Process()
    {
      Environment.Exit(0);
      return null;
    }
  }
}
