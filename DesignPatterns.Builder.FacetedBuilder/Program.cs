using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder.FacetedBuilder
{

    public class Person
    {
        //address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;

        public int AnnualIncome;

        public override string ToString()
        {
            return
                $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder
    {
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }

        public PersonAddressBuilder At(string streenAddress)
        {
            person.StreetAddress = streenAddress;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PersonBuilder pb = new PersonBuilder();
            Person person = pb.Lives.In("edirne").Works.AsA("spider")
                .Lives.At("123 Long Road")
                    .In("Longon")
                    .WithPostCode("34310")
                .Works.At("Medyanet")
                    .AsA("engineer")
                    .Earning(130000);

            Console.WriteLine(person);

            Console.ReadKey();
        }
    }
}
