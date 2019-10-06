using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda_FindingConvexHull
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetIn(new StreamReader("data.txt"));

            string input = Console.ReadLine();

            int testCount = int.Parse(input);

            Console.WriteLine(testCount);

            for (int t = 0; t < testCount; ++t)
            {
                input = Console.ReadLine();

                int lineCount = int.Parse(input);

                List<Point> points = new List<Point>();

                for (int i = 0; i < lineCount; ++i)
                {
                    input = Console.ReadLine();

                    string[] s = input.Split(' ');

                    int one = int.Parse(s[0]);
                    int two = int.Parse(s[1]);

                    points.Add(new Point(one, two));
                }

                if (t != testCount - 1)
                {
                    Console.ReadLine();
                }

                //smallest Y smallest X

                Point origin = LowestYThenLowestX(points);

                points.Remove(origin);

                foreach (Point p in points)
                {
                    p.Angle = CalculateAngle(origin, p);
                    p.Distance = CalculateDistance(origin, p);


                }

                points = points.Distinct(new PointComparer()).ToList(); //remove duplicates

                points = points.OrderBy(m => m.Angle).ThenBy(n => n.Distance).ToList(); //order list

                //foreach (Point p in points)
                //{
                //    Console.WriteLine(p);
                //}

                List<Point> hull = new List<Point>();
                Queue<Point> pointsQueue = new Queue<Point>(points);

                hull.Add(origin);

                hull.Add(pointsQueue.Dequeue());
                hull.Add(pointsQueue.Dequeue());

                while (pointsQueue.Count > 0)
                {
                    hull.Add(pointsQueue.Dequeue());

                    //foreach( Point p in hull )
                    //{
                    //    Console.WriteLine(p);
                    //}

                    while (!ValidHull(hull))
                    {
                        hull.RemoveAt(hull.Count - 2);
                    }
                }

                Console.WriteLine(hull.Count + 1);

                //Console.WriteLine("\n\n\nDone List:");
                foreach (Point p in hull)
                {
                    Console.WriteLine("{0} {1}", p.X, p.Y);
                }

                Console.WriteLine("{0} {1}", hull[0].X, hull[0].Y);

                if (t != testCount - 1)
                {
                    Console.WriteLine("-1");
                }
            }
        }

        private static bool ValidHull(List<Point> hull)
        {
            return (SignedArea(hull[hull.Count - 3], hull[hull.Count - 2], hull[hull.Count - 1]) > 0);
        }

        static int SignedArea(Point one, Point two, Point three)
        {
            // axby – axcy- aybx + aycx + bxcy – bycx

            double area = one.X * two.Y - one.X * three.Y - one.Y * two.X + one.Y * three.X + two.X * three.Y - two.Y * three.X;

            if (area == 0)
            {
                return 0;
            }

            return (int)(area / Math.Abs(area));
        }

        static Point LowestYThenLowestX(List<Point> points)
        {
            int lowestY = points.Min(m => m.Y);
            var lowYs = points.Where(m => (m.Y == lowestY));
            if (lowYs.Count() > 1)
            {
                int lowestX = lowYs.Min(m => m.X);
                return lowYs.Where(m => m.X == lowestX).First();
            }
            else
            {
                return lowYs.First();
            }
        }

        static double CalculateDistance(Point one, Point two)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(one.X - two.X), 2) + Math.Pow(Math.Abs(one.Y - two.Y), 2));
        }

        static double CalculateAngle(Point one, Point two)
        {
            if (Math.Abs(one.X - two.X) == 0)
            {
                return 90;
            }
            double x = Math.Atan((double)Math.Abs(one.Y - two.Y) / Math.Abs(one.X - two.X)) * 180 / Math.PI;
            if (two.X < one.X)
            {
                return 180 - x;
            }
            return x;
        }
    }
}
