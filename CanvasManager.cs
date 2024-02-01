using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL2
{
    /// <summary>
    /// Manages a drawing canvas within a PictureBox control.
    /// </summary>
    public class CanvasManager
    {
        /// <summary>
        /// Gets the Bitmap used as a drawing surface.
        /// </summary>
        public Bitmap bitmap { get; private set; }
        private PictureBox pictureBox;

        /// <summary>
        /// Initializes a new instance of the CanvasManager class with a specified PictureBox.
        /// </summary>
        /// <param name="pictureBox">The PictureBox control that will display the Bitmap.</param>
        public CanvasManager(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            InitializeBitmap();
        }


        /// <summary>
        /// Initializes the Bitmap with the size of the PictureBox and sets it as the PictureBox's image.
        /// </summary>
        private void InitializeBitmap()
        {
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bitmap;
        }


        /// <summary>
        /// Refreshes the PictureBox to update its display.
        /// </summary>
        public void RefreshSurface()
        {
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Disposes of the Bitmap resource.
        /// </summary>
        public void Dispose()
        {
            bitmap.Dispose();
        }
    }

}
