using System;
using System.Collections.Generic;
using System.Linq;

using IndRes.LogAnalyzer.Core.Models;

namespace IndRes.LogAnalyzer.Validation
{
  public class ValidationProvider
  {
    private const string LogonAuthFailed = "I_LOGON_AUTH_FAILED";

    private readonly ValidationConfiguration configuration;

    public ValidationProvider(ValidationConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public ValidationResult[] Validate(List<Event> events)
    {
      var multipleUserNameResult = this.ValidateMultipleUserNameAttemptsPeriod(events);
      var singleUserNameResult = this.ValidateSingleUserNameAttemptsPeriod(events);
      var usernameNumberResult = this.ValidateUserNameAttemptsNumber(events);

      return multipleUserNameResult
        .Union(singleUserNameResult)
        .Union(usernameNumberResult)
        .ToArray();
    }

    private ValidationResult[] ValidateMultipleUserNameAttemptsPeriod(List<Event> events)
    {
      var eventsByIps = events.GroupBy(e => e.Session.RemoteAddress);

      var results = new List<ValidationResult>(100);

      foreach (var ipInfo in eventsByIps)
      {
        var failedAttempts = ipInfo.Where(i => i.Name == ValidationProvider.LogonAuthFailed).ToList();
        var attemptedPeriod = failedAttempts.Max(a => ValidationProvider.ToDateTime(a.Time)).Subtract(failedAttempts.Min(e => ValidationProvider.ToDateTime(e.Time))).TotalMinutes;

        if (attemptedPeriod > configuration.SameUsernameAttemptsPeriodLimit)
        {
          results.Add(new ValidationResult()
          {
            IsSuspicious = true,
            Message = $"{ipInfo.Key} exceeded unsuccessfull login period."
          });
        }
      }

      return results.ToArray();
    }

    private ValidationResult[] ValidateSingleUserNameAttemptsPeriod(List<Event> events)
    {
      var eventsByIps = events
        .GroupBy(e => e.Session.RemoteAddress)
        .SelectMany(auth => auth.GroupBy(a => a.Authentication.UserName));

      var results = new List<ValidationResult>(100);

      foreach (var ipInfo in eventsByIps)
      {
        var failedAttempts = ipInfo.Where(i => i.Name == ValidationProvider.LogonAuthFailed).ToList();
        var attemptedPeriod = failedAttempts.Max(a => ValidationProvider.ToDateTime(a.Time)).Subtract(failedAttempts.Min(e => ValidationProvider.ToDateTime(e.Time))).TotalMinutes;

        if (attemptedPeriod > configuration.SameUsernameAttemptsPeriodLimit)
        {
          results.Add(new ValidationResult()
          {
            IsSuspicious = true,
            Message = $"{ipInfo.Key} exceeded unsuccessfull login period."
          });
        }
      }

      return results.ToArray();
    }

    private ValidationResult[] ValidateUserNameAttemptsNumber(List<Event> events)
    {
      var ips = events.GroupBy(e => e.Session.RemoteAddress);
      var results = new List<ValidationResult>(100);

      foreach (var ipInfo in ips)
      {
        var userNameAttempts = ipInfo.Where(i => i.Name == LogonAuthFailed).Select(e => e.Authentication.UserName).Count();

        if (userNameAttempts > configuration.UserNameAttemptsLimit)
        {
          results.Add(new ValidationResult()
          {
            IsSuspicious = true,
            Message = $"{ipInfo.Key} tried to login unsuccessfully {userNameAttempts} times."
          });
        }
      }
      return results.ToArray();
    }

    public static DateTime ToDateTime(string datetime)
    {
      if (DateTime.TryParse(datetime, out var parsedDateTime))
      {
        return parsedDateTime;
      }

      return DateTime.MinValue;
    }
  }
}
