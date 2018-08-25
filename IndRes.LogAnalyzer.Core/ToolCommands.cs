using System.ComponentModel;

namespace IndRes.LogAnalyzer.Commands
{
  public enum ToolCommands
  {
    [Description("Read logs.")]
    ReadLog = 1,

    [Description("Analyze log")]
    AnalyzeLog,

    [Description("Exit.")]
    Exit
  }
}
