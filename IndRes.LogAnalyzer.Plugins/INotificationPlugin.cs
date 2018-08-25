namespace IndRes.LogAnalyzer.Plugins
{
  public interface INotificationPlugin
  {
    void Notify(NotificationMessage[] validationresults);
  }
}