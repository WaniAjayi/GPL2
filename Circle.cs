using GPL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL2
{
    public class Circle
    {
        public Point Center { get; private set; }
        public int Radius { get; private set; }

        public Circle(Point center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public static void ProcessCommand(string command, CanvasManager canvasManager, CursorManager cursorManager, VariableManager variableManager)
        {
            var parts = command.Split(' ');
            if (parts.Length >= 2)
            {
                bool isRadiusParsed = int.TryParse(parts[1], out int radius);

                if (!isRadiusParsed)
                {
                    // Try to get the value from the variable manager
                    string variableValue = VariableManager.GetVariableValue(parts[1]);
                    isRadiusParsed = int.TryParse(variableValue, out radius);
                }

                if (isRadiusParsed)
                {
                    Point currentPosition = cursorManager.GetCurrentPosition();
                    Circle circle = new Circle(currentPosition, radius);
                    DrawCircle(canvasManager, circle.Center, circle.Radius);
                }
                else
                {
                    // Handle parsing error or variable not found
                }
            }
            else
            {
                // Handle parsing error
            }
        }

        private static void DrawCircle(CanvasManager canvasManager, Point center, int radius)
        {
            using (Graphics g = Graphics.FromImage(canvasManager.bitmap))
            {
                g.DrawEllipse(Pens.Black, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }
            canvasManager.RefreshSurface();
        }
    }
}
