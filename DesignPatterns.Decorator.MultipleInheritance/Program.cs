using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Decorator.MultipleInheritance
{
    public interface IBird
    {
        void Fly();
    }

    public class Bird : IBird
    {
        public void Fly()
        {
            WriteLine("Soaring in the sky");
        }
    }

    public interface ILizard
    {
        void Crawl();
    }

    public class Lizard : ILizard
    {
        public void Crawl()
        {
            WriteLine("Crawling in the dirt");
        }
    }

    public class Dragon : IBird, ILizard
    {
        readonly Bird _bird = new Bird();
        readonly Lizard _lizard = new Lizard();
        public void Fly()
        {
            _bird.Fly();
        }

        public void Crawl()
        {
            _lizard.Crawl();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}