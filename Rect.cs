using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPL2
{
    public class Rect
    {
        public Rectangle Rectangle { get; private set; }

        public Rect(int height, int width, Point position)
        {
            Rectangle = new Rectangle(position.X, position.Y, width, height);
        }

        public static void ProcessCommand(string command, CanvasManager canvasManager, CursorManager cursorManager)
        {
            
            var dimensions = command.Trim().Split(',');
            if (dimensions.Length == 2)
            {
                string heightValue = dimensions[0].Trim();
                string widthValue = dimensions[1].Trim();

                int height, width;
                if (int.TryParse(heightValue, out height))
                {
                    if (int.TryParse(widthValue, out width))
                    {
                        Point currentPosition = cursorManager.GetCurrentPosition();
                        Rect rect = new Rect(height, width, currentPosition);
                        DrawRectangle(canvasManager, rect.Rectangle);
                    }
                    else if (VariableManager.VariableExists(widthValue))
                    {
                        // Use the variable value as width
                        int.TryParse(VariableManager.GetVariableValue(widthValue), out width);
                        Point currentPosition = cursorManager.GetCurrentPosition();
                        Rect rect = new Rect(height, width, currentPosition);
                        DrawRectangle(canvasManager, rect.Rectangle);
                    }
                    else
                    {
                       
                    }
                }
                else if (VariableManager.VariableExists(heightValue))
                {
                    // Use the variable value as height
                    int.TryParse(VariableManager.GetVariableValue(heightValue), out height);

                    if (int.TryParse(widthValue, out width))
                    {
                        Point currentPosition = cursorManager.GetCurrentPosition();
                        Rect rect = new Rect(height, width, currentPosition);
                        DrawRectangle(canvasManager, rect.Rectangle);
                    }
                    else if (VariableManager.VariableExists(widthValue))
                    {
                        // Use the variable value as width
                        int.TryParse(VariableManager.GetVariableValue(widthValue), out width);
                        Point currentPosition = cursorManager.GetCurrentPosition();
                        Rect rect = new Rect(height, width, currentPosition);
                        DrawRectangle(canvasManager, rect.Rectangle);
                    }
                    else
                    {
                        // Handle parsing error for width
                    }
                }
                else
                {
                    // Handle parsing error for height
                }
            }
            else
            {
                // Handle invalid command format
            }
        }
    

        private static void DrawRectangle(CanvasManager canvasManager, Rectangle rect)
        {
            using (Graphics g = Graphics.FromImage(canvasManager.bitmap))
            {
                g.DrawRectangle(Pens.Black, rect);
            }
            canvasManager.RefreshSurface();
        }
    }
}
