using System.Collections.Generic;
using System.Drawing;

namespace CirculosCercanos
{
    public class Circle
    {
        private int _x;
        private int _y;
        private int _r;
        private int _id;
        private bool _visited;
        private List<Circle> _adjacents = new List<Circle>();
        private Dictionary<int, List<Point>> _paths = new Dictionary<int, List<Point>>();


        public Circle(int x, int y, int r, int id)
        {
            _x = x;
            _y = y;
            _r = r;
            _id = id;
        }

        public int Id
        {
            get => _id;
        }

        public int X
        {
            get => _x;
        }

        public int Y
        {
            get => _y;
        }

        public int R
        {
            get => _r;
        }

        public bool Visited
        {
            get => _visited;
            set => _visited = value;
        }

        public List<Circle> Adjacents => _adjacents;

        public Dictionary<int, List<Point>> Paths => _paths;

        public bool IsDestination { get; set; }

        public bool HasAgent { get; set; }

        public override string ToString()
        {
            return $"Circle #{_id}: x = {_x} || y = {_y} || r = {_r}";
        }
    }
}