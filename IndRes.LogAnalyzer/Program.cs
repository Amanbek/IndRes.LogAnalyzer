using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using IndRes.LogAnalyzer.BootStrapper;
using IndRes.LogAnalyzer.Commands;
using IndRes.LogAnalyzer.Core;

namespace IndRes.LogAnalyzer
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var c = ClientBootStrapper.Configure())
      {
        while (true)
        {
          var processor = c.GetInstance<CommandProcessor>();
          Console.WriteLine("Choose option:");
          processor.GetOptions().ForEach(Console.WriteLine);
          processor.ProcessCommand(Console.ReadLine()).Wait();
        }
      }
    }
  }
}
