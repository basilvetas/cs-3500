using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace LabSQL
{
    public class Demo
    {
        /// <summary>
        /// The connection string.
        /// Your CADE login name serves as both your database name and your uid
        /// Your uNID serves as your password
        /// </summary>
        public const string connectionString = "server=atr.eng.utah.edu;database=LabTest;uid=LabTestUser;password=TestPassword";

        /// <summary>
        ///  Test several connections and print the output to the console
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            AllPeople();
            Console.WriteLine();
            AllPhones();
            Console.WriteLine();
            PeoplesPhones();
            Console.ReadLine();
        }


        public static void AllPeople()
        {
            // Connect to the DB
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open a connection
                    conn.Open();

                    // Create a command
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = "select ID, Name from People";

                    // Execute the command and cycle through the DataReader object
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["ID"] + " " + reader["Name"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public static void AllPhones()
        {
            // Connect to the DB
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open a connection
                    conn.Open();

                    // Create a command
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = "select ID, Number from Phones";

                    // Execute the command and cycle through the DataReader object
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["ID"] + " " + reader["Number"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public static void PeoplesPhones()
        {
            // Connect to the DB
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open a connection
                    conn.Open();

                    // Create a command
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = "select Name, Number from People, Phones where People.ID = Phones.ID";

                    // Execute the command and cycle through the DataReader object
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Name"] + " " + reader["Number"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
            
