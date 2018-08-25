using System;
using System.Collections.Generic;
using System.IO;

using IndRes.LogAnalyzer.Core;
using IndRes.LogAnalyzer.Core.LogReader;
using IndRes.LogAnalyzer.Core.Models;
using IndRes.LogAnalyzer.Validation;

using NUnit.Framework;

namespace IndRes.LogAnalyzer.Tests
{
  public class TestsBase
  {
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
      var dir = Path.GetDirectoryName(typeof(LogValidationTests).Assembly.Location);
      if (dir != null)
      {
        Environment.CurrentDirectory = dir;
        Directory.SetCurrentDirectory(dir);
      }
      else
        throw new Exception("Path.GetDirectoryName(typeof(TestingWithReferencedFiles).Assembly.Location) returned null");
    }

    protected static ValidationProvider CreateValidationProvider()
    {
      var validationConfiguration = new ValidationConfiguration()
      {
        SameUsernameAttemptsPeriodLimit = 2,
        MultipleUsernameAttemptsPeriodLimit = 2,
        UserNameAttemptsLimit = 2
      };

      return new ValidationProvider(validationConfiguration);
    }

    protected static List<Event> GetEvents(string logLocation)
    {
      return TestsBase.GetReader(logLocation).ReadLogEvents();
    }

    protected static ILogReader GetReader(string logLocation)
    {
      var configuration = new LogReaderConfiguration()
      {
        LogLocation = logLocation
      };

      return new LogReader(configuration);
    }
  }
}