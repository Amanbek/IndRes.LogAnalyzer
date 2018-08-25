using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using IndRes.LogAnalyzer.Core.Models;

namespace IndRes.LogAnalyzer.Validation
{
  public class ValidationProvider
  {
    private const string LogonAuthFailed = "I_LOGON_AUTH_FAILED";

    private readonly ValidationConfiguration configuration;

    private readonly MultipleUserNameLoginValidator multipleUserNameLoginValidator;

    private readonly SingleUserNameAttemptValidatior singleUserNameAttemptValidatior;

    private readonly UserNameAttemptsNumberValidator userNameAttemptsNumberValidator;

    private List<IValidator> validators;

    public ValidationProvider(ValidationConfiguration configuration)
    {
      this.configuration = configuration;
      this.validators = new List<IValidator>()
       {
         new MultipleUserNameLoginValidator(configuration),
         new SingleUserNameAttemptValidatior(configuration),
         new UserNameAttemptsNumberValidator(configuration)
       };
    }

    public ReadOnlyCollection<ValidationResult> Validate(List<Event> events)
    {
      var failedLogonSessions = events
        .Where(e => e.Name == ValidationProvider.LogonAuthFailed)
        .ToList();

      var validationResults = this.validators
        .SelectMany(v => v.Validate(failedLogonSessions))
        .ToList();

      return new ReadOnlyCollection<ValidationResult>(validationResults);
    }
  }
}
