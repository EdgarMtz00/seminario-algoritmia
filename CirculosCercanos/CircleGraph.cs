using System;
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
        public Circle destination;

        private Pen kruskalPen = new Pen(Color.Orange, 15);
        private Pen primPen = new Pen(Color.Green, 7);

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
            double lookingAngle = AngleBetweenPoints(circle.ToPoint(), destination.ToPoint());
            List<Circle> randomOrder =
                circle.Adjacents.OrderBy(a =>
                {
                    double angle = AngleBetweenPoints(circle.ToPoint(), a.ToPoint());
                    if (Math.Abs(angle - lookingAngle) > 180)
                    {
                        return (180 - Math.Abs(angle)) + (180 - Math.Abs(lookingAngle));
                    }

                    return Math.Abs(angle - lookingAngle);
                }).ToList();
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

                    animationList.Add(circle);
                }
            }

            return null;
        }

        private double AngleBetweenPoints(Point a, Point b)
        {
            double res = Math.Atan2(b.Y - a.Y, b.X - a.X) * (180 / Math.PI);
            return res;
        }


        public List<Circle> FindDestination(int indexDestination)
        {
            Reset();
            destination = _circles[indexDestination];
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


        private List<Circle> BfsArray(List<Circle> vertices)
        {
            List<Circle> result = new List<Circle>();

            Queue<Circle> queue = new Queue<Circle>();
            queue.Enqueue(vertices[0]);

            while (queue.Any())
            {
                Circle next = queue.Dequeue();

                foreach (Circle adjacent in next.Adjacents)
                {
                    if (!result.Contains(adjacent))
                    {
                        result.Add(adjacent);
                        vertices.Remove(adjacent);
                        queue.Enqueue(adjacent);
                    }
                }
            }

            return result;
        }

        public void CreateTrees(Bitmap bmpImage)
        {
            Reset();
            List<Circle> vertices = new List<Circle>(_circles);
            Queue<List<Circle>> forest = new Queue<List<Circle>>();

            while (vertices.Count > 0)
            {
                forest.Enqueue(BfsArray(vertices));
            }


            foreach (List<Circle> tree in forest)
            {
                List<Tuple<Circle, Circle>> primCandidates = new List<Tuple<Circle, Circle>>();
                foreach (Circle circle in forest.Count != 1 ? tree : _circles)
                {
                    foreach (Circle circleAdjacent in circle.Adjacents)
                    {
                        if (!circleAdjacent.Visited)
                        {
                            primCandidates.Add(new Tuple<Circle, Circle>(circle, circleAdjacent));
                        }
                    }

                    circle.Visited = true;
                }

                primCandidates = primCandidates.OrderBy(a => distanceBetweenCircles(a.Item1, a.Item2)).ToList();

                Queue<Tuple<Circle, Circle>> kruskalCandidates = new Queue<Tuple<Circle, Circle>>(primCandidates);

                Queue<Tuple<Circle, Circle>> kruskalMap = Kruskal(kruskalCandidates, tree.Count - 1);
                Queue<Tuple<Circle, Circle>> primMap = Prim(primCandidates, tree.Count - 1);
                drawEdges(kruskalMap, kruskalPen, bmpImage);
                drawEdges(primMap, primPen, bmpImage);
            }
        }

        private Queue<Tuple<Circle, Circle>> Prim(List<Tuple<Circle, Circle>> candidates, int n)
        {
            List<Tuple<Circle, Circle>> promising = new List<Tuple<Circle, Circle>>();
            promising.Add(candidates[0]);
            candidates.RemoveAt(0);

            while (promising.Count != n)
            {
                Tuple<Circle, Circle> edge = PrimSelect(candidates, promising);

                if (PrimCheck(edge, promising))
                {
                    promising.Add(edge);
                }
            }

            return new Queue<Tuple<Circle, Circle>>(promising);
        }

        private bool PrimCheck(Tuple<Circle, Circle> edge, List<Tuple<Circle, Circle>> promising)
        {
            bool flagItem1 = false;
            bool flagItem2 = false;

            foreach (Tuple<Circle, Circle> tuple in promising)
            {
                if (edge.Item1 == tuple.Item1 || edge.Item1 == tuple.Item2)
                {
                    flagItem1 = true;
                }

                if (edge.Item2 == tuple.Item1 || edge.Item2 == tuple.Item2)
                {
                    flagItem2 = true;
                }

                if (flagItem1 & flagItem2)
                {
                    break;
                }
            }

            return !(flagItem1 & flagItem2);
        }

        private Tuple<Circle, Circle> PrimSelect(List<Tuple<Circle, Circle>> candidates,
            List<Tuple<Circle, Circle>> promising)
        {
            Tuple<Circle, Circle> result = null;
            float minDistance = Single.MaxValue;
            foreach (Tuple<Circle, Circle> edge in promising)
            {
                foreach (Tuple<Circle, Circle> candidate in candidates)
                {
                    if (edge.Item1 == candidate.Item1 || edge.Item1 == candidate.Item2 ||
                        edge.Item2 == candidate.Item1 || edge.Item2 == candidate.Item2)
                    {
                        float distance = distanceBetweenCircles(candidate.Item2, candidate.Item1);
                        if (minDistance > distance)
                        {
                            result = candidate;
                            minDistance = distance;
                        }
                    }
                }
            }

            candidates.Remove(result);
            return result;
        }

        private float distanceBetweenCircles(Circle a, Circle b)
        {
            return (float) Math.Sqrt(Math.Pow(a.X - a.X, 2) + Math.Pow(a.Y - a.Y, 2));
        }

        private Queue<Tuple<Circle, Circle>> Kruskal(Queue<Tuple<Circle, Circle>> candidates, int n)
        {
            Queue<Tuple<Circle, Circle>> result = new Queue<Tuple<Circle, Circle>>();
            Subset[] CC = Subset.SubsetsForCollection(_circles);

            while (result.Count != n)
            {
                Tuple<Circle, Circle> a_i = candidates.Dequeue();
                int ind1 = Subset.Find(CC, a_i.Item1.Id);
                int ind2 = Subset.Find(CC, a_i.Item2.Id);

                if (ind1 != ind2)
                {
                    result.Enqueue(a_i);
                    CC[ind1].Join(CC[ind2]);
                    CC[ind2] = null;
                }
            }

            return result;
        }

        public void drawEdges(Queue<Tuple<Circle, Circle>> edges, Pen pen, Bitmap bmpImage)
        {
            Graphics graphics = Graphics.FromImage(bmpImage);
            foreach (var tuple in edges)
            {
                graphics.DrawLine(pen, tuple.Item1.X, tuple.Item1.Y, tuple.Item2.X, tuple.Item2.Y);
            }
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
                m = (origin.Y - destination.Y == 0)
                    ? 0.0f
                    : (float) (destination.X - origin.X) / (destination.Y - origin.Y);

                increment = (origin.Y < destination.Y) ? 1 : -1;

                while (!PointIsObstacle(x, y))
                {
                    y += increment;
                    x = (int) Math.Round(m * (y - origin.Y) + b);
                    path.Add(new Point(x, y));
                }
            }
            else
            {
                b = origin.Y;
                m = origin.X - destination.X == 0
                    ? 0.0f
                    : (float) (destination.Y - origin.Y) / (destination.X - origin.X);

                increment = origin.X < destination.X ? 1 : -1;

                while (!PointIsObstacle(x, y))
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
            if (isWhite(color))
            {
                _outsideFlag = true;
            }

            return _outsideFlag ? !isWhite(color) : !isGray(color);
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