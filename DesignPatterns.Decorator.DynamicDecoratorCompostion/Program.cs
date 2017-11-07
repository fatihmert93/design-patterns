using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Decorator.DynamicDecoratorCompostion
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle: IShape
    {
        private float radius;

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }
        public string AsString() => $"A circle with raius {radius}";

        public override string ToString()
        {
            return AsString();
        }
    }

    public class Square : IShape
    {
        private float side;

        public Square(float side)
        {
            this.side = side;
        }
        public string AsString() => $"A square with side {side}";

        public override string ToString()
        {
            return AsString();
        }
    }

    public class ColoredShape : IShape
    {
        private IShape shape;
        private string color;

        public ColoredShape(IShape shape, string color)
        {
            this.shape = shape;
            this.color = color;
        }
        public string AsString() => $"{shape.AsString()} has the color {color}";

        public override string ToString()
        {
            return AsString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var square = new Square(1.23f);
            Console.WriteLine(square);

            var redSquare = new ColoredShape(square,"red");
            Console.WriteLine(redSquare);

            Console.ReadKey();
        }
    }
}
