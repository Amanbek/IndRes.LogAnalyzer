using System;
using System.Linq;

using IndRes.LogAnalyzer.Plugins;

namespace IndRes.Samples.SamplePlugin
{
  public class SampleNotoficationPlugin : INotificationPlugin
  {
    public void Notify(NotificationMessage[] validationresults)
    {
      if (validationresults.Any() == false)
      {
        return;
      }

      Console.ForegroundColor = ConsoleColor.Red;
      foreach (var notificationMessage in validationresults)
      {

        Console.WriteLine($"{notificationMessage.ViolationType.ToString()}: {notificationMessage.Message}");
      }

      Console.ForegroundColor = ConsoleColor.Green;
    }
  }
}
