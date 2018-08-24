namespace IndRes.LogAnalyzer.Validation
{
  public class ValidationConfiguration
  {
    public int UserNameAttemptsLimit { get; set; }

    public int SameUsernameAttemptsPeriodLimit { get; set; }

    public int MultipleUsernameAttemptsPeriodLimit { get; set; }
  }
}
