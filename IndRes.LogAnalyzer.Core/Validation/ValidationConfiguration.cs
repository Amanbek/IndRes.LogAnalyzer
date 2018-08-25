using System.Configuration;

namespace IndRes.LogAnalyzer.Validation
{
  public class ValidationConfiguration
  {
    public int UserNameAttemptsLimit => ValidationConfiguration.TryGetSetting("allowed-username-attempts");

    public int SameUsernameAttemptsPeriodLimit => ValidationConfiguration.TryGetSetting("same-username-attempts-period");

    public int MultipleUsernameAttemptsPeriodLimit => ValidationConfiguration.TryGetSetting("multiple-username-attempts-period");

    private static int TryGetSetting(string settingName)
    {
      return int.TryParse(ConfigurationManager.AppSettings[settingName], out var setting) 
               ? setting 
               : int.MaxValue;
    }
  }
}
