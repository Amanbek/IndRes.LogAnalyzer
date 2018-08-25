using System.Collections.Generic;
using System.Linq;

using IndRes.LogAnalyzer.Core.Extensions;
using IndRes.LogAnalyzer.Core.Models;
using IndRes.LogAnalyzer.Plugins;

namespace IndRes.LogAnalyzer.Validation
{
  public class UserNameAttemptsNumberValidator : IValidator
  {
    private readonly ValidationConfiguration configuration;

    public UserNameAttemptsNumberValidator(ValidationConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public ValidationResult[] Validate(List<Event> events)
    {
      var results = new List<ValidationResult>(100);

      foreach (var session in events.ToSessionList())
      {
        var userNameAttempts = session.Select(e => e.Authentication.UserName).Count();

        if (userNameAttempts > configuration.UserNameAttemptsLimit)
        {
          results.Add(new ValidationResult()
          {
            IsSuspicious = true,
            Message = $"{session.Key} tried to login unsuccessfully {userNameAttempts} times.",
            Type = ViolationType.UserNameNumberAttempt
          });
        }
      }

      return results.ToArray();
    }
  }
}