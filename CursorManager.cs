using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL2
{
    public class CursorManager
    {
        private PictureBox pictureBox;
        private Graphics graphics;
        private Rectangle cursorRect;
        private Pen cursorPen;


        public CursorManager(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            this.graphics = pictureBox.CreateGraphics();
            this.cursorRect = new Rectangle(0, 0, 10, 10);
            this.cursorPen = new Pen(Color.Red, 5);
        }
       // private const int DefaultX = 0;
      //  private const int DefaultY = 0;

       // public int CurrentX { get; private set; } = DefaultX;
        //public int CurrentY { get; private set; } = DefaultY;


        public void MoveCursor(int x, int y)
        {
            // Clear the previous cursor
            pictureBox.Invalidate();

            // Update the cursor's position
            cursorRect.Location = new Point(x, y);

            // Draw the new cursor
            graphics.DrawRectangle(cursorPen, cursorRect);
        }

        public Point GetCurrentPosition()
        {
            return cursorRect.Location;
        }

        // Dispose resources if needed
        public void Dispose()
        {
            cursorPen.Dispose();
            graphics.Dispose();
        }

    }
}
