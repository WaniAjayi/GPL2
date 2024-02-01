using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GPL2
{
    public class Parser
    {
        private readonly string[] regexPatterns =
       {
            @"drawto\s(\d+),\s?(\d+)",
            @"moveto\s(\d+),\s?(\d+)",
            @"rect\s(\w+),\s?(\w+)",
            @"tri\s([a-zA-Z0-9]+),\s?([a-zA-Z0-9]+)",
            @"^circle\s(\d+|\w+)",
            @"clear",
            @"reset",
            @"run",
            @"\bfill (on|off)\s(color:(red|green|blue|yellow))",
            @"\bpen (red|green|blue|yellow)\b",
            @"^if\s+(.+?)(?:\s*,\s*(.*))?$",
            @"endif",
            @"while\s*([^{\n\r]+)",
            @"endwhile",
            @"^\s*([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*([a-zA-Z0-9_]+(\s*[\+\-\*\/]\s*[a-zA-Z0-9_]+)*)\s*$"

        };

        public List<string> delayedExecution = new List<string>();
        private List<string> syntaxErrors = new List<string>();
        public void ParseCommands(string[] commands)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                bool isValidCommand = false;

                foreach (var pattern in regexPatterns)
                {
                        if (Regex.IsMatch(commands[i], pattern))
                        {
                            isValidCommand = true;
                            delayedExecution.Add(commands[i]);
                            break; 
                        }
                }

                if (!isValidCommand)
                {
                    syntaxErrors.Add($"Invalid command on line {i + 1}: {commands[i]}");
                }

            }
        }

        public List<string> GetSyntaxErrors()
        {
            return syntaxErrors;
        }
        public void AddToDelayedCommands(string command)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                delayedExecution.Add(command);
            }
        }

        public List<string> GetDelayedExecutionCommands()
        {
            return delayedExecution;
        }
    }
}
