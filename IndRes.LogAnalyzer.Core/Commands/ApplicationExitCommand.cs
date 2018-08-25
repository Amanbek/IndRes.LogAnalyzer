using System;
using System.Threading.Tasks;

namespace IndRes.LogAnalyzer.Commands.Commands
{
  public class ApplicationExitCommand : ICommand
  {
    public void Process()
    {
      Environment.Exit(0);
    }
  }
}
