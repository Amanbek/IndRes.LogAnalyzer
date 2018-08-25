using IndRes.LogAnalyzer.Plugins;

namespace IndRes.LogAnalyzer.Validation
{
  public class ValidationResult
  {
    public string Message { get; set; }

    public bool IsSuspicious { get; set; }

    public ViolationType Type { get; set; }
  }
}