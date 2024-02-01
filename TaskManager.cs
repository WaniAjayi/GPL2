using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GPL2
{
    /// <summary>
    /// Manages and executes a series of graphical commands on a specified PictureBox.
    /// </summary>
    public class TaskManager
    {
        private Dictionary<string, int> variableDictionary = new Dictionary<string, int>();
        //private TaskManager taskManager;
        private CursorManager cursorManager;
        private CanvasManager canvasManager;
        public AssignmentManager assignmentManager;
        private VariableManager variableManager;
        private IfStatement ifStatement;
        private Parser parser;
        
        
        /// <summary>
        /// Initializes a new instance of the TaskManager class with specified parameters.
        /// </summary>
        /// <param name="pictureBox">The PictureBox control where graphics are drawn.</param>
        /// <param name="cursorManager">Manages the cursor's position and state.</param>
        /// <param name="commands">List of commands to be executed.</param>
        /// <param name="conditionEval">Function delegate for evaluating conditions.</param>
        public TaskManager(PictureBox pictureBox, CursorManager cursorManager, List<string> commands, Func<bool> conditionEval)
        {
            this.cursorManager = cursorManager;
            this.canvasManager = new CanvasManager(pictureBox);
            this.cursorManager = cursorManager;
            this.variableManager = new VariableManager();
            this.ifStatement = new IfStatement();   
            this.parser = new Parser();
        }

        /// <summary>
        /// Executes a list of graphical commands.
        /// </summary>
        /// <param name="commands">The list of commands to be executed.</param>
        public void ExecuteCommands(List<string> commands)
        {
            

            foreach (var command in commands)
            {
                string[] parts = command.Split(' ');
                string commandType = parts[0].ToLower();
                int currentIndex = commands.IndexOf(command);

                if (Regex.IsMatch(commandType, @"^\s*([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*([a-zA-Z0-9_]+(\s*[\+\-\*\/]\s*[a-zA-Z0-9_]+)*)\s*$"))
                        {
                            AssignmentManager assignmentManager = new AssignmentManager();
                            assignmentManager.HandleAssignment(commandType);
                        }
                else
                {

                    switch (commandType)
                    {
                        case "drawto":
                            DrawTo.ProcessCommand(command, cursorManager, canvasManager);
                            break;
                        case "moveto":
                            MoveTo.ProcessCommand(command, cursorManager);
                            break;
                        case "rect":
                            Rect.ProcessCommand(parts[1], canvasManager, cursorManager);
                            break;
                        case "tri":
                            Triangle.ProcessCommand(command, canvasManager, cursorManager);
                            break;
                        case "circle":
                            Circle.ProcessCommand(command, canvasManager, cursorManager, variableManager);
                            break;
                        case "if":
                            ifStatement.HandleIfStatement(commands, currentIndex, conditionEval);
                            break;
                        case "while":
                           
                            WhileLoop whileLoop = new WhileLoop(parser);

                            
                            string condition = string.Join(" ", parts.Skip(1));


                            whileLoop.ProcessWhileLoop(commands.ToArray(), currentIndex, conditionEval);
                            


                            currentIndex = whileLoop.FindEndOfWhile(commands.ToArray(), currentIndex) + 1;
                        break;
                       


                        default:

                        break;
                    }
                }
            }
        }

        private bool conditionEval()
        {
            throw new NotImplementedException();
        }
    }
}