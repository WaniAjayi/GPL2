using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL2
{
    internal class DrawTo
    {
        private CursorManager cursorManager;
        private CanvasManager canvasManager;

        public DrawTo(CursorManager cursorManager, CanvasManager canvasManager)
        {
            this.cursorManager = cursorManager;
            this.canvasManager = canvasManager;
        }



        public void Execute(int x, int y)
        {

            Point currentPos = cursorManager.GetCurrentPosition();           // Get the current cursor position
            using (Graphics g = Graphics.FromImage(canvasManager.bitmap))   // Create a Graphics object for drawing
            {
                g.DrawLine(Pens.Black, currentPos, new Point(x, y));
            }
            canvasManager.RefreshSurface();                                 // Update the PictureBox to reflect the drawing
            cursorManager.MoveCursor(x, y);                                 // Update the cursor manager's position to the new point
        }

        public static void ProcessCommand(string command, CursorManager cursorManager, CanvasManager canvasManager)
        {
            string[] parts = command.Split(' ');
            if (parts.Length >= 2)
            {
                string[] coordinates = parts[1].Split(',');
                if (coordinates.Length >= 2 &&

                    int.TryParse(coordinates[0].Trim(), out int x) &&
                    int.TryParse(coordinates[1].Trim(), out int y))
                {
                    var drawto = new DrawTo(cursorManager, canvasManager);
                    drawto.Execute(x, y);
                    //canvasManager.DrawLineTo(x, y);
                }
                else
                {
                    // Handle invalid command format or values
                    throw new ArgumentException("Invalid parameters for DrawTo command.");
                }

            }
            else
            {
                throw new ArgumentException("Command format is incorrect.");
            }
        } 
    }
}
