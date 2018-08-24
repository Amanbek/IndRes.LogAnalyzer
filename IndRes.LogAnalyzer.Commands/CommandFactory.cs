using IndRes.LogAnalyzer.Commands.Commands;

using StructureMap;

namespace IndRes.LogAnalyzer.Commands
{
  public class CommandFactory
  {
    private readonly IContainer container;

    public CommandFactory(IContainer container)
    {
      this.container = container;
    }

    public ICommand CreateLogReaderCommand()
    {
      return container.GetInstance<ReadLogCommand>();
    }

    public ICommand CreateApplicationExitCommand()
    {
      return container.GetInstance<ApplicationExitCommand>();
    }

    public ICommand CreateAnalyzeLogCommand()
    {
      return container.GetInstance<AnalyzeLogEventsCommand>();
    }
  }
}