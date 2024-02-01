using GPL2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GPL2
{

    public class AssignmentManager
    {
        /// <summary>
        /// Process assignment statements and evaluate expression.
        /// </summary>
        /// <param name="command">The assignment command to be processed.</param>
        public void HandleAssignment(string command)
        {
            string[] parts = command.Split(new char[] { '=' }, 2);
            if (parts.Length != 2)
            {
                // handle error here
                return;
            }

            string variable = parts[0].Trim();
            string expOrValue = parts[1].Trim();

            char[] operators = { '+', '-', '*', '/' };
            if (expOrValue.IndexOfAny(operators) != -1)
            { 
                    try
                    {
                        string evaluatedExpression = EvaluateExpression(expOrValue);
                        VariableManager.AddOrUpdateVariable(variable, evaluatedExpression);
                    }
                    catch (Exception ex)
                    { 

                    }
            }
            else
            {

                VariableManager.AddOrUpdateVariable(variable, expOrValue);
            }
        }

    

        /// <summary>
        /// Evaluate a given expression.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>The result of the evaluated expression.</returns>
        public string EvaluateExpression(string expression)
        {

            char[] operators = new char[] { '+', '-', '*', '/', '<', '>', '=' }; // Defining the operators
            var parts = ExpressionSplit(expression, operators);
            for (int i = 0; i < parts.Length; i++)
            {
                if (VariableManager.VariableExists(parts[i]))
                {
                    // Replace variable with its value
                    parts[i] = VariableManager.GetVariableValue(parts[i]);
                }
            }
            return ComplexExpEvaluate(string.Join(" ", parts));
        }

        /// <summary>
        /// A simple method to evaluate an expression.
        /// This is a placeholder and should be replaced with actual evaluation logic.
        /// </summary>
        /// <param name="expression">The expression to be evaluated.</param>
        /// <returns>The result of the evaluation.</returns>
        private string[] ExpressionSplit(string expression, char[] operators)
        {
            var parts = new List<string>();
            var currentPart = new StringBuilder();

            foreach (char c in expression)
            {
                if (operators.Contains(c))
                {
                    if (currentPart.Length > 0)
                    {
                        parts.Add(currentPart.ToString());
                        currentPart.Clear();
                    }
                    parts.Add(c.ToString());
                }
                else
                {
                    currentPart.Append(c);
                }
            }
            if (currentPart.Length > 0)
            {
                parts.Add(currentPart.ToString());
            }

            return parts.ToArray();
        }


        private string ComplexExpEvaluate(string expression)
        {
            try
            {
                
                var result = new DataTable().Compute(expression, null);     // Creates a new DataTable instance and uses its Compute method to evaluate the expression

                
                return result.ToString();       // Converts the result to string and retursn it
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
