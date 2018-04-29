using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Random_value
{ 
    /// <summary>
    /// Graph image generator
    /// </summary>
    class GraphicsImageGenerator
    {
        // field
        private Bitmap bitmap;
        public Bitmap Bitmap
        {
            get { return bitmap; }
            private set { bitmap = value; }
        }
        public GraphicsImageGenerator(PointF[] listOfPoints, int width, int height, float scale)
        {
            bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            // setting blur
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // "Pen" for drawing
            Pen black_pen = new Pen(Color.Black, 1);
            Pen p = new Pen(Color.Red, 2);
            // Scaling
            for (int i = 0; i < listOfPoints.Length; i++)
            {
                listOfPoints[i].X = listOfPoints[i].X * scale;
                listOfPoints[i].Y = listOfPoints[i].Y * scale + height / 2;
            }
            // ots drawing.
            g.DrawLines(p, listOfPoints);
            // Grid
            g.DrawLine(black_pen, 0, 0, 0, height);
            g.DrawLine(black_pen, 0, height / 2, width, height / 2);
        }
    }
}
