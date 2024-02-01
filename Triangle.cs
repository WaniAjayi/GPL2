using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL2
{
    internal class Triangle
    {
        private Point[] points;

        public Triangle(Point basePoint, int baseLength, int height)
        {
            points = new Point[3];
            points[0] = basePoint; // Base-left point
            points[1] = new Point(basePoint.X + baseLength, basePoint.Y); // Base-right point
            points[2] = new Point(basePoint.X + baseLength / 2, basePoint.Y + height); // Top point
        }

        public static void ProcessCommand(string command, CanvasManager canvasManager, CursorManager cursorManager)
        {
            ExceptionHandler.ExecuteWithHandling(() =>
            {
                var parts = command.Split(' ');
                if (parts.Length >= 2)
                {
                    var dimensions = parts[1].Split(',');
                    if (dimensions.Length == 2)
                    {
                        int baseLength = GetDimensionValue(dimensions[0]);
                        int height = GetDimensionValue(dimensions[1]);


                        Point currentPosition = cursorManager.GetCurrentPosition();
                        Triangle triangle = new Triangle(currentPosition, baseLength, height);
                        DrawTriangle(canvasManager, triangle.points);
                    }
                    else
                    {
                        MessageBox.Show("Invalid dimension format. Expected format: baseLength,height", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });
        }

        private static void DrawTriangle(CanvasManager canvasManager, Point[] points)
        {
            using (Graphics g = Graphics.FromImage(canvasManager.bitmap))
            {
                g.DrawPolygon(Pens.Black, points);
            }
            canvasManager.RefreshSurface();
        }

        private static int GetDimensionValue(string input)
        {
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            else if (VariableManager.VariableExists(input))
            {
                string variableValue = VariableManager.GetVariableValue(input);
                if (int.TryParse(variableValue, out value))
                {
                    return value;
                }
                else
                {
                    throw new FormatException($"Variable '{input}' does not contain a valid integer.");
                }
            }
            else
            {
                throw new ArgumentException($"'{input}' is neither a valid integer nor a known variable.");
            }
            
        }
    }
}
