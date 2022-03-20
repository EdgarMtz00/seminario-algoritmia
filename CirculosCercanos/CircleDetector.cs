using System;
using System.Collections.Generic;
using System.Drawing;

namespace CirculosCercanos
{
    public class CircleDetector
    {
        private Bitmap _bmp;
        private Bitmap _animationBmp;

        private Graphics _graphics;
        private Graphics _animationGraphics;
        
        private Brush _brush = new SolidBrush(Color.Red);
        private Pen _pen = new Pen(Color.Chartreuse);
        private Pen _edgePen = new Pen(Color.Tomato);
        private Brush _agentBrush = new SolidBrush(Color.Goldenrod);

        private List<Circle> _circlesList = new List<Circle>();

        public CircleDetector(Bitmap bmp, Bitmap animationBmp, String filename)
        {
            this._bmp = new Bitmap(filename);
            this._animationBmp = animationBmp;
            this._graphics = Graphics.FromImage(bmp);
            this._animationGraphics = Graphics.FromImage(_animationBmp);
        }

        public List<Circle> CircleSearch()
        {
            for (int y = 0; y < _bmp.Height; y++)
            {
                for (int x = 0; x < _bmp.Width; x++)
                {
                    if (isBlack(_bmp.GetPixel(x, y)))
                    {
                        bool isNewCircle = true;
                        foreach (Circle circle in _circlesList)
                        {
                            if (PointBelongsToCircle(x, y, circle))
                            {
                                isNewCircle = false;
                            }
                        }
                        
                        if (isNewCircle)
                        {
                            SearchAndMarkCenter(x, y);
                            y++;
                        }
                    }
                }
            }
            return _circlesList;
        }
 
        private void SearchAndMarkCenter(int x, int y)
        {
            int y0 = y;
            
            while (!isWhite(_bmp.GetPixel(x, y + 1)))
                y++;
            int yCenter = (y0 + y) / 2;

            while (!isWhite(_bmp.GetPixel(x - 1, y)))
                x--;
            int x0 = x;
            
            while (!isWhite( _bmp.GetPixel(x + 1, y)))
                x++;
            int xCenter = (x0 + x) / 2;

            int r = Math.Abs(y - yCenter) + 5;
            
            _circlesList.Add(new Circle(xCenter, yCenter, r, _circlesList.Count + 1));
            Font font = new Font(FontFamily.GenericSansSerif, r/2);
            _graphics.DrawString(_circlesList.Count.ToString(), font, _brush, xCenter, yCenter);
            _graphics.DrawLine(_pen, xCenter - r/4, yCenter, xCenter + r/4, yCenter);
            _graphics.DrawLine(_pen, xCenter, yCenter - r/4, xCenter, yCenter + r/4);
        }

        public void DrawEdges()
        {
            for (int i = 0; i < _circlesList.Count; i++)
            {
                Circle circle1 = _circlesList[i];
                foreach (Circle circle2 in circle1.Adjacents)
                {
                    _graphics.DrawLine(_edgePen, circle1.X, circle1.Y, circle2.X, circle2.Y);
                }
            }
        }

        private bool isWhite(Color color)
        {
            return color.R == 255 && color.G == 255 && color.B == 255;
        }

        
        private bool isBlack(Color color)
        {
            return color.R == 0 && color.G == 0 && color.B == 0;
        }
        
        private bool PointBelongsToCircle(int x, int y, Circle circle)
        {
            return Math.Pow(x - circle.X, 2) + Math.Pow(y - circle.Y, 2) <= Math.Pow(circle.R + 3, 2);
        }

        public void DrawAgent(Point point)
        {
            _animationGraphics.Clear(Color.Transparent);
            _animationGraphics.FillEllipse(_agentBrush, point.X-25, point.Y-25, 50, 50);
        }
    }
}