
using System;

namespace SimpleFigures
{
    abstract class Figure    // parent class
    {
        public abstract void Move(int x, int y);

        public abstract void Rotate(int angle);
    }

    class Point : Figure    // child class
    {
        private int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

        public override void Rotate(int angle)
        {
            double radians = Math.PI * angle / 180.0;
            double newX = x * Math.Cos(radians) - y * Math.Sin(radians);
            double newY = x * Math.Sin(radians) + y * Math.Cos(radians);
            x = (int)newX;
            y = (int)newY;
        }

        public override string ToString()
        {
            return $"Point - Point({x}, {y})";
        }
    }

    class Line : Figure    // child class
    {
        private Point start, end;
        public Line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public override void Move(int dx, int dy)
        {
            start.Move(dx, dy);
            end.Move(dx, dy);
        }

        public override void Rotate(int angle)
        {
            start.Rotate(angle);
            end.Rotate(angle);
        }

        public override string ToString()
        {
            return $"Line - Start: {start}, End: {end}";
        }
    }

    class Circle : Figure    // child class
    {
        private Point center; 
        private int radius;

        public Circle(Point center, int r)
        {
            this.center = center;
            this.radius = r;
        }

        public override void Move(int dx, int dy)
        {
            center.Move(dx, dy);
        }

        public override void Rotate(int angle)
        {
            center.Rotate(angle);
        }

        public override string ToString()
        {
            return $"Circle - Center: {center}, Radius: {radius}";
        }
    }

    class Aggregation : Figure
    {
        private List<Figure> figures = new List<Figure>();
        private static Random random = new Random();

        public void AddRandomFigures(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int figureType = random.Next(3); // Randomly selects between 0, 1, and 2
                switch (figureType)
                {
                    case 0:
                        figures.Add(new Point(random.Next(0, 100), random.Next(0, 100)));
                        break;
                    case 1:
                        figures.Add(new Line(
                            new Point(random.Next(0, 100), random.Next(0, 100)),
                            new Point(random.Next(0, 100), random.Next(0, 100))));
                        break;
                    case 2:
                        figures.Add(new Circle(
                            new Point(random.Next(0, 100), random.Next(0, 100)),
                            random.Next(1, 50)));
                        break;
                }
            }
        }

        public override void Move(int dx, int dy)
        {
            foreach (var figure in figures)
            {
                figure.Move(dx, dy);
            }
        }

        public override void Rotate(int angle)
        {
            foreach (var figure in figures)
            {
                figure.Rotate(angle);
            }
        }

        public override string ToString()
        {
            return $"(Aggregation)\n{string.Join(", \n", figures)}";
        }
    }

    class Program
    {

        static void Main(string[] args)
        {

            var aggregation = new Aggregation();
            int randomNumber = new Random().Next(2, 10);

            aggregation.AddRandomFigures(randomNumber);
            Console.WriteLine("Figures Added:");
            Console.WriteLine(aggregation);

            Console.Write("\nEnter the x-coordinate to move (int): ");
            int coorX = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the y-coordinate to move (int): ");
            int coorY = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the degree to rotate (int): ");
            int degree = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nBefore Move and Rotate:");
            Console.WriteLine(aggregation);

            aggregation.Move( coorX, coorY);
            aggregation.Rotate(degree);

            Console.WriteLine("\nAfter Move and Rotate:");
            Console.WriteLine(aggregation);
        }
    }
}