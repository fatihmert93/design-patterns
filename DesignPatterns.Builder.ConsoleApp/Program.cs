using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder.ConsoleApp
{

    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;


        public HtmlElement()
        {
            
        }
        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder
    {
        HtmlElement root = new HtmlElement();
        private readonly string _rootName;

        public HtmlBuilder(string rootName)
        {
            this._rootName = rootName;
            root.Name = rootName;
        }

        public void AddChild(string childName, string childText)
        {
            HtmlElement element = new HtmlElement(childName,childText);
            root.Elements.Add(element);
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
             root = new HtmlElement{Name = _rootName};
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li","world");
            Console.WriteLine(builder.ToString());

            Console.ReadKey();

        }
    }
}
