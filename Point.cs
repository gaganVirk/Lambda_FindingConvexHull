using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda_FindingConvexHull
{
    class Point : IComparable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public double Angle { get; set; }

        public double Distance { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;

            Angle = 0;
            Distance = 0;
        }

        public override string ToString()
        {
            return string.Format("Angle: {0} Distance: {1} X: {2} Y: {3}", Angle, Distance, X, Y);
        }

        public int CompareTo(Point other)
        {
            return ((X.CompareTo(other.X)) + (Y.CompareTo(other.Y)));
        }
    }

    class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point x, Point y)
        {
            return x.X.CompareTo(y.X) == 0 && x.Y.CompareTo(y.Y) == 0;
        }

        public int GetHashCode(Point obj)
        {
            return (int)Math.Pow(obj.X, obj.Y);
        }
    }
}
