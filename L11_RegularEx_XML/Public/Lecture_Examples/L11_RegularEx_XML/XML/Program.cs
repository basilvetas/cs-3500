using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XML
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteExample1();
            WriteExample2();
            //Console.ReadLine();

            ReadExample1();
            ReadExample2();
            Console.ReadLine();
        }

        /// <summary>
        /// Uses nested elements exclusively
        /// </summary>
        public static void WriteExample1 () {
            using (XmlWriter writer = XmlWriter.Create("states1.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("States");

                writer.WriteStartElement("State");
                writer.WriteElementString("Name", "Utah");
                writer.WriteElementString("Capital", "Salt Lake City");
                writer.WriteEndElement();

                writer.WriteStartElement("State");
                writer.WriteElementString("Name", "Nevada");
                writer.WriteElementString("Capital", "Reno");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }


        /// <summary>
        /// Uses mixture of nested elements and attributes
        /// </summary>
        public static void WriteExample2()
        {
            using (XmlWriter writer = XmlWriter.Create("states2.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("States");

                writer.WriteStartElement("State");
                writer.WriteAttributeString("Name", "Utah");
                writer.WriteAttributeString("Capital", "Salt Lake City");
                writer.WriteEndElement();

                writer.WriteStartElement("State");
                writer.WriteAttributeString("Name", "Nevada");
                writer.WriteAttributeString("Capital", "Reno");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Reads the file written by WriteExample1
        /// </summary>
        public static void ReadExample1()
        {
            using (XmlReader reader = XmlReader.Create("states1.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "States":
                                break;

                            case "State":
                                Console.WriteLine();
                                break;

                            case "Name":
                                Console.Write("State name = ");
                                reader.Read();
                                Console.WriteLine(reader.Value);
                                break;

                            case "Capital":
                                Console.Write("State capital = ");
                                reader.Read();
                                Console.WriteLine(reader.Value);
                                break;
                        }
                    }
                }

            }
        }
    

        /// <summary>
        /// Reads the file written by WriteExample2
        /// </summary>
        public static void ReadExample2()
        {
            using (XmlReader reader = XmlReader.Create("states2.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "States":
                                break;

                            case "State":
                                Console.WriteLine();
                                Console.WriteLine("State name = " + reader["Name"]);
                                Console.WriteLine("State capital = " + reader["Capital"]);
                                break;
                        }
                    }
                }
            }
        }
    }
}
