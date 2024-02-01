using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GPL2
{
    /// <summary>
    /// Manages variables, provides logic for adding, updating, retrieving, and checking the existence of variables.
    /// </summary>
    public class VariableManager
    {
        private static Dictionary<string, string> variables = new Dictionary<string, string>();


        /// <summary>
        /// Checks if the input qualifies as a valid variable name.
        /// </summary>
        /// <param name="input">The string to be checked.</param>
        /// <returns>Returns true if the input is a valid variable name; otherwise, false.</returns>
        public static bool IsVariable(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z_][a-zA-Z0-9]*$");
        }


        /// <summary>
        /// Adds a new variable or updates the value of an existing variable.
        /// </summary>
        /// <param name="variableName">The name of the variable to add or update.</param>
        /// <param name="value">The value to be assigned to the variable.</param>
        public static void AddOrUpdateVariable(string variableName, string value)
        {
            variables[variableName] = value; // Adds or updates the variable in the dictionary
        }


        /// <summary>
        /// Retrieves the value of a specified variable.
        /// </summary>
        /// <param name="variableName">The name of the variable whose value is to be retrieved.</param>
        /// <returns>The value of the specified variable.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the specified variable does not exist.</exception>
        public static string GetVariableValue(string variableName)
        {
            if (variables.TryGetValue(variableName, out string value))
            {
                return value;
            }
            throw new KeyNotFoundException($"Variable '{variableName}' not found.");
        }

        public static bool VariableExists(string variableName)
        {
            return variables.ContainsKey(variableName);
        }
    }
}
