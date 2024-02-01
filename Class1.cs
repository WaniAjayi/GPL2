using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL2
{
    internal class Class1
    {
        private string CheckVariable(string variableName)
        {


            if (VariableManager.VariableExists(variableName))
            {
                return VariableManager.GetVariableValue(variableName);
            }
            else { return variableName; }
            throw new InvalidOperationException($"Undeclared variable '{variableName}'.");
        }

        public bool PerformComparison(string operand1, string OperatorVar, string operand2)
        {
            int opVar1 = int.Parse(operand1);
            int opVar2 = int.Parse(operand2);

            switch (OperatorVar)
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
