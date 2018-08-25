using System.Collections.Generic;
using System.Linq;

using IndRes.LogAnalyzer.Core.Extensions;
using IndRes.LogAnalyzer.Core.Models;
using IndRes.LogAnalyzer.Plugins;

namespace IndRes.LogAnalyzer.Validation
{
  public class MultipleUserNameLoginValidator : IValidator
  {
    private readonly ValidationConfiguration configuration;

    public MultipleUserNameLoginValidator(ValidationConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public ValidationResult[] Validate(List<Event> events)
    {
      var results = new List<ValidationResult>(100);

      foreach (var session in events.ToSessionList())
      {
        var isMultipleUserNameAttempt = session.Select(a => a.Authentication.UserName).Distinct().Count() > 1;

        if (isMultipleUserNameAttempt == false)
        {
          continue;
        }

        var maxTime = session.Max(a => a.Time.ToDateTime());
        var minTime = session.Min(e => e.Time.ToDateTime());

        var attemptedPeriod = maxTime.Subtract(minTime).TotalMinutes;

        if (attemptedPeriod <= configuration.MultipleUsernameAttemptsPeriodLimit)
        {
          results.Add(new ValidationResult()
          {
            IsSuspicious = true,
            Message = $"{session.Key} exceeded unsuccessfull login period with multiple usernames.",
            Type = ViolationType.MultipleUserNameAttempt
          });
        }
      }

      return results.ToArray();
    }
  }
}