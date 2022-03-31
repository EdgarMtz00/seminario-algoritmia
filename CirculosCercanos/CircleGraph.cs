﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CirculosCercanos
{
    public class CircleGraph
    {
        private readonly List<Circle> _circles;
        private readonly Bitmap _bitmap;
        private bool _outsideFlag;
        private static readonly Random Rng = new Random();
        public List<Circle> animationList = new List<Circle>();

        public CircleGraph(List<Circle> circles, String filename)
        {
            _bitmap = new Bitmap(filename);
            _circles = circles;
            for (int i = 0; i < circles.Count; i++)
            {
                for (int j = i + 1; j < circles.Count; j++)
                {
                    if (CreatePath(circles[i], circles[j]))
                    {
                        circles[i].Adjacents.Add(circles[j]); 
                        circles[j].Adjacents.Add(circles[i]);
                    }
                }
            }
        }

        public List<Circle> Bfs(Circle start)
        {
            Reset();
            LinkedList<TreeNode> queue = new LinkedList<TreeNode>();
            start.Visited = true;

            Tree tree = new Tree(start);
            queue.AddLast(tree.Root);
            while (queue.Any())
            {
                TreeNode node = queue.First();
                queue.RemoveFirst();
                foreach (Circle adjacent in node.C.Adjacents)
                {
                    if (!adjacent.Visited)
                    {
                        adjacent.Visited = true;
                        TreeNode son = node.AddSon(adjacent);
                        queue.AddLast(son);
                        if (adjacent.IsDestination)
                        {
                            return son.ListFathers();
                        }
                    }
                }
            }

            return null;
        }

        private List<Circle> Dfs(Circle circle)
        {
            animationList.Add(circle);
            circle.Visited = true;
            List<Circle> randomOrder = circle.Adjacents.OrderBy(a => Rng.Next()).ToList();
            foreach (Circle circleAdjacent in randomOrder)
            {
                if (circleAdjacent.IsDestination)
                {
                    animationList.Add(circleAdjacent);
                    List<Circle> res = new List<Circle> {circleAdjacent};
                    return res;
                }
                if (!circleAdjacent.Visited)
                {
                    List<Circle> possibleRes = Dfs(circleAdjacent);
                    if (possibleRes != null)
                    {
                        possibleRes.Add(circleAdjacent);
                        return possibleRes;
                    }
                    else
                    {
                        animationList.Add(circle);
                    }
                }
            }

            return null;
        }

        public List<Circle> FindDestination()
        {
            Reset();
            Circle agent = null;
            foreach (Circle circle in _circles)
            {
                if (circle.HasAgent)
                {
                    agent = circle;
                }
            }

            if (agent != null)
            {
                List<Circle> res = Dfs(agent);
                if (res != null)
                {
                    res.Add(agent);
                    res.Reverse();
                    return res;  
                }

            }

            return null;
        }

        public void Reset()
        {
            foreach (Circle circle in _circles)
            {
                circle.Visited = false;
            }
        }
        private bool CreatePath(Circle origin, Circle destination)
        {
            List<Point> path = new List<Point>();
            float m;
            int increment;
            int b;
            int x = origin.X;
            int y = origin.Y;

            if (Math.Abs(origin.Y - destination.Y) > Math.Abs(origin.X - destination.X))
            {
                b = origin.X;
                m = (origin.X - destination.X == 0)
                    ? 0.0f
                    : (float) (destination.X - origin.X) / (destination.Y - origin.Y);
                
                increment = (origin.Y < destination.Y) ? 1 : -1;
                
                while (!PointIsObstacle(x, y))
                {
                    y+= increment;
                    x = (int) Math.Round(m * (y - origin.Y) + b);
                    path.Add(new Point(x, y));
                }
            }
            else
            {
                b = origin.Y;
                m = origin.Y - destination.Y == 0
                    ? 0.0f
                    : (float) (destination.Y - origin.Y) / (destination.X - origin.X);
                
                increment = origin.X < destination.X ? 1 : -1;
                
                while(!PointIsObstacle(x, y))
                {
                    x += increment;
                    y = (int) Math.Round(m * (x - origin.X) + b);
                    path.Add(new Point(x, y));
                }
            }

            if (PointBelongsToCircle(x, y, destination))
            {
                List<Point> reversedPath = new List<Point>();
                foreach (Point point in path.AsEnumerable().Reverse())
                {
                    reversedPath.Add(point);
                }
                origin.Paths.Add(destination.Id, path);
                destination.Paths.Add(origin.Id, reversedPath);
                
                _outsideFlag = false;
                return true;
            }
            
            _outsideFlag = false;
            return false;
        }
        
        private bool PointBelongsToCircle(int x, int y, Circle circle)
        {
            return Math.Pow(x - circle.X, 2) + Math.Pow(y - circle.Y, 2) <= Math.Pow(circle.R + 3, 2);
        }

        private bool PointIsObstacle(int x, int y)
        {
            Color color = _bitmap.GetPixel(x, y);
            if (isWhite(color) )
            {
                _outsideFlag = true;
            }
            
            return _outsideFlag? !isWhite(color)  : !isGray(color);
        }

        private bool isWhite(Color color)
        {
            return color.R == 255 & color.G == 255 && color.B == 255;
        }

        private bool isGray(Color color)
        {
            return color.R == color.G && color.G == color.B;
        }
        
        public void SetAgent(int id)
        {
            foreach (Circle circle in _circles)
            {
                circle.HasAgent = circle.Id == id;
            }
        }
        
        public void SetDestiny(int id)
        {
            foreach (Circle circle in _circles)
            {
                circle.IsDestination = circle.Id == id;
            }
        }
    }

}