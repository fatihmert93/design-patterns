using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainOfResponsibility.MethodChain
{

    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature;
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null) next.Add(cm);
            else next = cm;
        }

        public virtual void Handle() => next?.Handle();
    }

    public class NoBonusesModifer : CreatureModifier
    {
        public NoBonusesModifer(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            // noting
            Console.WriteLine("No bonuses for you!");
        }
    }

    public class DoubleAttackModidier : CreatureModifier
    {
        public DoubleAttackModidier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense *= 3;
            base.Handle();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var goblin = new Creature("Goblin",2,2);
            Console.WriteLine(goblin);

            var root = new CreatureModifier(goblin);

            root.Add(new NoBonusesModifer(goblin));

            Console.WriteLine("Let's double the goblin's attack");
            root.Add(new DoubleAttackModidier(goblin));

            Console.WriteLine("Let's increase goblin's defense");
            root.Add(new IncreaseDefenseModifier(goblin));

            root.Handle();

            Console.WriteLine(goblin);

            Console.ReadKey();
        }
    }
}
