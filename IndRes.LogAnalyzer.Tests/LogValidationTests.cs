using System.Linq;

using IndRes.LogAnalyzer.Plugins;
using IndRes.LogAnalyzer.Validation;

using NUnit.Framework;

namespace IndRes.LogAnalyzer.Tests
{
  [TestFixture]
  public class LogValidationTests : TestsBase
  {
    [Test]
    public void TestResultEmptyWhenNoViolations()
    {
      var events = TestsBase.GetEvents(@"Logs\noviolations.xml");
      var validationConfiguration = new ValidationConfiguration()
      {
        SameUsernameAttemptsPeriodLimit = 1,
        MultipleUsernameAttemptsPeriodLimit = 1,
        UserNameAttemptsLimit = 1
      };

      var validationProvider = new ValidationProvider(validationConfiguration);
      var validationCollection = validationProvider.Validate(events);

      Assert.IsEmpty(validationCollection);
    }

    [Test]
    public void TestResultIsNotEmptyWhenSingleUsernameViolationExist()
    {
      var events = TestsBase.GetEvents(@"Logs\singleUserName.xml");
      var validationProvider = TestsBase.CreateValidationProvider();

      var validationCollection = validationProvider.Validate(events);
      var singleUserNameViolations = validationCollection.Select(v => v.Type == ViolationType.SingleUserNameAttempt);

      Assert.AreEqual(singleUserNameViolations.Count(), 1);
    }

    [Test]
    public void TestResultIsNotEmptyWhenMultipleUsernameViolationExist()
    {
      var events = TestsBase.GetEvents(@"Logs\singleUserName.xml");
      var validationProvider = TestsBase.CreateValidationProvider();

      var validationCollection = validationProvider.Validate(events);
      var singleUserNameViolations = validationCollection.Select(v => v.Type == ViolationType.MultipleUserNameAttempt);

      Assert.AreEqual(singleUserNameViolations.Count(), 1);
    }

    [Test]
    public void TestResultIsNotEmptyWhenUsernameNumberViolationExist()
    {
      var events = TestsBase.GetEvents(@"Logs\singleUserName.xml");
      var validationProvider = TestsBase.CreateValidationProvider();

      var validationCollection = validationProvider.Validate(events);
      var singleUserNameViolations = validationCollection.Select(v => v.Type == ViolationType.UserNameNumberAttempt);

      Assert.AreEqual(singleUserNameViolations.Count(), 1);
    }
  }
}
