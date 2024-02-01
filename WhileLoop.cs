using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GPL2
{
    public class WhileLoop
    {

        public TaskManager taskManager;
        private Parser parser;
        private string condition;

        public WhileLoop(Parser parser)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public void ProcessWhileLoop(string[] commands, int startIndex, Func<bool> conditionEval)
        {
            if (commands == null)
                throw new ArgumentNullException(nameof(commands));

            if (startIndex < 0 || startIndex >= commands.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Invalid startIndex value");

            string command = commands[startIndex];
            string[] parts = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2 || parts[0].ToLower() != "while")
                throw new InvalidOperationException("Invalid While loop command");

            condition = string.Join(" ", parts.Skip(1));
            int endIndex = FindEndOfWhile(commands, startIndex);
            List<string> whileCommands = commands.Skip(startIndex + 1).Take(endIndex - startIndex - 1).ToList();
            conditionEval = () => EvaluateCondition(condition);

            if (taskManager == null)
                throw new ArgumentNullException(nameof(taskManager));


            ExecuteWhileLoop(whileCommands, conditionEval, taskManager);
        }

        private void ExecuteWhileLoop(List<string> whileCommands, Func<bool> conditionEval, TaskManager taskManager)
        {
            while (conditionEval())
            {
                foreach (var command in whileCommands)
                {
                    taskManager.ExecuteCommands(new List<string> { command });
                }
            }
        }

        public int FindEndOfWhile(string[] commands, int startIndex)
        {
            for (int i = startIndex + 1; i < commands.Length; i++)
            {
                if (commands[i].Trim().ToLower() == "endwhile")
                    return i;
            }
            throw new InvalidOperationException("No matching 'endwhile' for while");
        }

        public bool EvaluateCondition(string condition)
        {
            var match = Regex.Match(condition, @"(\S+)\s*([<>=!]+)\s*(\S+)");
            if (!match.Success)
                throw new ArgumentException("Invalid condition format.");

            string operand1 = match.Groups[1].Value;
            string operatorVar = match.Groups[2].Value;
            string operand2 = match.Groups[3].Value;

            operand1 = CheckOperand(operand1);
            operand2 = CheckOperand(operand2);

            return PerformComparison(operand1, operatorVar, operand2);
        }

        private string CheckOperand(string operand)
        {
            if (VariableManager.IsVariable(operand) && VariableManager.VariableExists(operand))
                return VariableManager.GetVariableValue(operand);

            return operand;
        }

        private bool PerformComparison(string operand1, string operatorVar, string operand2)
        {
            // Assumes operands are integers, adjust if necessary
            int opVar1 = int.Parse(operand1);
            int opVar2 = int.Parse(operand2);

            switch (operatorVar)
            {
                case "==": return opVar1 == opVar2;
                case "!=": return opVar1 != opVar2;
                case "<": return opVar1 < opVar2;
                case ">": return opVar1 > opVar2;
                case "<=": return opVar1 <= opVar2;
                case ">=": return opVar1 >= opVar2;
                default: throw new ArgumentException("Invalid operator.");
            }
        }
    }
}
