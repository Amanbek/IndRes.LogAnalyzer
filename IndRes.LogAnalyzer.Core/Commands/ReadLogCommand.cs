using System;
using System.Threading.Tasks;

using IndRes.LogAnalyzer.Core;

namespace IndRes.LogAnalyzer.Commands
{
  public class ReadLogCommand : ICommand
  {
    private readonly ILogReader reader;

    public ReadLogCommand(ILogReader reader)
    {
      this.reader = reader;
    }
    public void Process()
    {
      reader.ReadLog();
    }
  }
}