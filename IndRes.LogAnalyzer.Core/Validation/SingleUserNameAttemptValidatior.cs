using System.Collections.Generic;
using System.Linq;

using IndRes.LogAnalyzer.Core.Extensions;
using IndRes.LogAnalyzer.Core.Models;
using IndRes.LogAnalyzer.Plugins;

namespace IndRes.LogAnalyzer.Validation
{
  public class SingleUserNameAttemptValidatior : IValidator
  {
    private readonly ValidationConfiguration configuration;

    public SingleUserNameAttemptValidatior(ValidationConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public ValidationResult[] Validate(List<Event> events)
    {
      var results = new List<ValidationResult>(100);

      foreach (var session in events.ToSessionList())
      {
        var isSingleUserNameAttempt = session.Select(a => a.Authentication.UserName).Distinct().Count() == 1;

        if (isSingleUserNameAttempt == false)
        {
          continue;
        }

        var maxTime = session.Max(a => a.Time.ToDateTime());
        var minTime = session.Min(e => e.Time.ToDateTime());

        var attemptedPeriod = maxTime.Subtract(minTime).TotalMinutes;

        if (attemptedPeriod <= configuration.SameUsernameAttemptsPeriodLimit)
        {
          results.Add(new ValidationResult()
          {
            IsSuspicious = true,
            Message = $"{session.Key} exceeded unsuccessfull login period with single username.",
            Type = ViolationType.SingleUserNameAttempt
          });
        }
      }

      return results.ToArray();
    }
  }
}