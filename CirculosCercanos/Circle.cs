namespace CirculosCercanos
{
    public class Circle
    {
        private int _x;
        private int _y;
        private int _r;
        private int _id;
    
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public Circle(int x, int y, int r, int id)
        {
            this._x = x;
            this._y = y;
            this._r = r;
            this._id = id;
        }

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public int R
        {
            get => _r;
            set => _r = value;
        }

        public override string ToString()
        {
            return $"Circle #{_id}: x = {_x} || y = {_y} || r = {_r}";
        }
    }
}