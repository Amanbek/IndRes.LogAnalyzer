﻿using System.Threading.Tasks;

namespace IndRes.LogAnalyzer.Commands
{
  public interface ICommand
  {
    CommandExecuteResult Process();
  }
}
