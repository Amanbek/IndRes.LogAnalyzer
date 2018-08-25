using System.Collections.Generic;

using IndRes.LogAnalyzer.Core.Models;

namespace IndRes.LogAnalyzer.Validation
{
  public interface IValidator
  {
    ValidationResult[] Validate(List<Event> events);
  }
}