using System;
using System.Collections.Generic;
using System.Drawing;

namespace CirculosCercanos
{
    public class CircleDetector
    {
        private Bitmap _bmp;

        private Graphics _graphics;
        
        private Brush _brush = new SolidBrush(Color.Red);
        private Pen _pen = new Pen(Color.Chartreuse);

        private List<Circle> _circlesList = new List<Circle>();

        public CircleDetector(Bitmap bmp, String filename)
        {
            this._bmp = new Bitmap(filename);
            this._graphics = Graphics.FromImage(bmp);
        }

        public List<Circle> CircleSearch()
        {
            for (int y = 0; y < _bmp.Height; y++)
            {
                for (int x = 0; x < _bmp.Width; x++)
                {
                    if (!isWhite(_bmp.GetPixel(x, y)))
                    {
                        bool isNewCircle = true;
                        foreach (Circle circle in _circlesList)
                        {
                            if (Math.Pow(x - circle.X, 2) + Math.Pow(y - circle.Y, 2) <= Math.Pow(circle.R + 3, 2))
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
            DrawClosestPairPointsLine();
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

            int r = Math.Abs(y - yCenter);
            
            _circlesList.Add(new Circle(xCenter, yCenter, r, _circlesList.Count + 1));
            Font font = new Font(FontFamily.GenericSansSerif, r / 2 == 0 ? 1 : r/2);
            _graphics.DrawString(_circlesList.Count.ToString(), font, _brush, xCenter, yCenter);
            _graphics.DrawLine(_pen, xCenter - r/4, yCenter, xCenter + r/4, yCenter);
            _graphics.DrawLine(_pen, xCenter, yCenter - r/4, xCenter, yCenter + r/4);
        }

        private void DrawClosestPairPointsLine()
        {
            Tuple<Circle, Circle> closestPairPoint = null;
            double minDistance = Double.MaxValue;
            foreach (Circle c1 in _circlesList)
            {
                foreach (Circle c2 in _circlesList)
                {
                    if (c1 != c2)
                    {
                        double distance = Math.Sqrt(Math.Pow(c2.X - c1.X, 2) + Math.Pow(c2.Y - c1.Y, 2)); 
                        if (minDistance > distance)
                        {
                            minDistance = distance;
                            closestPairPoint = Tuple.Create(c1, c2);
                        }
                    }
                }
            }

            if (closestPairPoint != null)
            {
                Circle item1 = closestPairPoint.Item1;
                Circle item2 = closestPairPoint.Item2;
            
                _graphics.DrawLine(_pen, item1.X, item1.Y, item2.X, item2.Y);
            }
        }

        private bool isWhite(Color color)
        {
            return color.R == 255 && color.G == 255 && color.B == 255;
        }
    }
}