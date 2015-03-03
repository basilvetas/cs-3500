using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_Examples
{
    struct ABC
    {
        public int a;

        public ABC(int a)
        {
            this.a = a;
        }
        public void add_one()
        {
            this.a++;
        }

    }

    class DEF
    {
        public int a;
        public DEF(int a)
        {
            this.a = a;
        }

        public void add_one()
        {
            this.a++;
        }
    }

    class Struct_Example
    {
        static void Main(string[] args)
        {
            ABC var_abc = new ABC(100);
            DEF var_def = new DEF(100);

            Console.WriteLine(var_abc.a);
            Console.WriteLine(var_def.a);

            Console.WriteLine("--------------- Using Methods ----------------------------");

            var_abc.add_one();
            var_def.add_one();
            Console.WriteLine(var_abc.a);
            Console.WriteLine(var_def.a);

            Console.WriteLine("--------------- Using Static Functions ----------------------------");

            add_one( var_abc );
            add_one(var_def);
            Console.WriteLine(var_abc.a);
            Console.WriteLine(var_def.a);

            Console.Read();
        }

        private static void add_one(DEF var_def)
        {
            var_def.add_one();
        }

        private static void add_one(ABC var_abc)
        {
            var_abc.add_one();
        }
    }
}
