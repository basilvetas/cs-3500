using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LectureExamples
{
    /// <summary>
    /// Examples of properties
    /// </summary>
    public class PropertyDemo
    {
        public static void Main(string[] args)
        {
            // Person1
            Person1 p1 = new Person1("Jack", 28);
            p1.name = "Jill";
            Console.WriteLine(p1.age);

            // Person2
            Person2 p2 = new Person2("Jack", 28);
            p2.SetName("Jill");
            Console.WriteLine(p2.GetAge());

            // Person3
            Person3 p3 = new Person3("Jack", 28);
            p3.Name = "Jill";
            Console.WriteLine(p3.Age);

            Person5 p5 = new Person5("Jack", 30);
            p5.Age = 100;
            Console.WriteLine(p5.Age);

            Console.ReadLine();
        }
    }


    /// <summary>
    /// A Person class using public member variables.  Don't do this 
    /// in either C# or Java!
    /// </summary>
    public class Person1
    {
        public String name;
        public int age;
        public Person1(String n, int a)
        {
            name = n;
            age = a;
        }
    }


    /// <summary>
    /// A Person class using private member variables with
    /// getters and setters.  Kind of verbose!
    /// </summary>
    public class Person2
    {
        private int age;
        private String name;

        public Person2(String n, int a)
        {
            this.name = n;
            this.age = a;
        }

        public int GetAge()
        {
            return age;
        }

        public String GetName()
        {
            return name;
        }

        public void SetAge(int age)
        {
            this.age = age;
        }

        public void SetName(String name)
        {
            this.name = name;
        }
    }


    /// <summary>
    /// A Person class using properties.  Very succinct!
    /// </summary>
    public class Person3
    {
        public Person3(String n, int a)
        {
            Name = n;
            Age = a;
        }

        public String Name { get; set; }
        public int Age { get; set; }
    }


    /// <summary>
    /// Customization is possible.
    /// </summary>
    public class Person4
    {
        public Person4(String n, int a)
        {
            Name = n;
            Age = a;
        }

        public String Name { get; private set; }
        public int Age { get; private set; }
    }

    /// <summary>
    /// Customization is possible.
    /// </summary>
    public class Person5
    {
        private int _age;

        public Person5(String n, int a)
        {
            Name = n;
            Age = a;
        }

        public String Name { get; private set; }

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    _age = value;
                }
            }
        }
    }

}
