using System;
using System.Collections.Generic;

namespace GPL2
{
    /// <summary>
    /// This class handles the processing of 'if' statements within a given set of commands.
    /// </summary>
    public class IfStatement
    {
        /// <summary>
        /// Stores indices of 'if' statements within a stack for tracking nested or multiline 'if' statements.
        /// </summary>
        public Stack<int> ifStatementIndex = new Stack<int>();

        /// <summary>
        /// Handles the parsing and execution of 'if' statements within a list of commands.
        /// </summary>
        /// <param name="commands">The list of commands to be processed.</param>
        /// <param name="currentIndex">The current index in the command list being processed.</param>
        /// <param name="conditionEval">A function delegate that evaluates the condition of the 'if' statement.</param>
        /// <exception cref="InvalidOperationException">Thrown when an 'endif' is encountered without a corresponding 'if' statement.</exception>
        public void HandleIfStatement(List<string> commands, int currentIndex, Func<bool> conditionEval)
        {
            string currentCommand = commands[currentIndex].Trim();

            if (currentCommand.StartsWith("if"))
            {

                bool singleLineIf = currentIndex + 1 < commands.Count && // Ensure there is at least one command after 'if'
                     !(currentIndex + 2 < commands.Count && !commands[currentIndex + 2].Trim().Equals("endif")); // Check if it's not a multi-line 'if'

                if (singleLineIf)           //checks if program conditions match rules for singleline IF statements.
                {
                    
                    string[] parts = currentCommand.Split(new char[] { ' ' }, 2);       // Split the 'if' statement to get the condition
                    if (parts.Length >= 2)
                    {
                        string expression = parts[1];
                        
                        return;
                    }
                    else
                    {

                        string condition = parts[1].Trim();
                        bool conditionResult = conditionEval();

                        if (conditionResult)
                        {
                            Parser parser = new Parser();
                            
                            parser.AddToDelayedCommands(commands[currentIndex + 1].Trim());
                            
                        }
                        else
                        {

                        }
                    }


                }
                if (!singleLineIf)
                {
                    
                    ifStatementIndex.Push(currentIndex);            // Stores the index of 
                }

                else if (currentCommand.Equals("endif"))
                {
                    if (ifStatementIndex.Count > 0)
                    {
                        // Pop for multi-line if
                        ifStatementIndex.Pop();
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unexpected 'endif' at line {currentIndex + 1}");
                    }
                }
            }
        }





        /// <summary>
        /// Checks if there are any unmatched 'if' statements left in the stack.
        /// </summary>
        /// <returns>Returns true if there are unmatched 'if' statements; otherwise, false.</returns>
        /// <exception cref="InvalidOperationException">Thrown when an unmatched 'if' statement is detected.</exception>
        public bool HasUnmatchedIfStatements()
        {
            if (ifStatementIndex.Count > 0)
            {
                // Error handling for unmatched if statements
                throw new InvalidOperationException($"Unmatched 'if' at line {ifStatementIndex.Peek() + 1}");
            }
            return false;
        }
    }
}
