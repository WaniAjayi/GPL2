using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL2
{
    internal class MoveTo
    {
        private CursorManager cursorManager;

        public MoveTo(CursorManager cursorManager)
        {
            this.cursorManager = cursorManager;
        }

        public void Execute(int x, int y)
        {
            cursorManager.MoveCursor(x, y);
        }

        public static void ProcessCommand(string command, CursorManager cursorManager)
        {
            string[] parts = command.Split(' ');
            if (parts.Length >= 2)
            {
                string[] coordinates = parts[1].Split(',');
                if (coordinates.Length == 2 &&
                    int.TryParse(coordinates[0], out int x) &&
                    int.TryParse(coordinates[1], out int y))
                {
                    MoveTo moveTo = new MoveTo(cursorManager);
                    moveTo.Execute(x, y);
                }
                else
                {
                    // Handle invalid coordinate format or values
                }
            }
            else
            {
                // Handle invalid command format
            }
        }
    }
}
