using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoggleServer;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using CustomNetworking;
using System.Collections.Generic;


namespace BoggleServerTest
{
    /// <summary>
    /// Unit tests for our BoggleServer
    /// </summary>
    /// <author>Basil Vetas, Lance Petersen</author>
    /// <date>November 18, 2014</date>
    [TestClass]
    public class BoggleServerTest
    {
        /// <summary>
        /// Starts a new server with two arguments, stops it, and then starts a new server
        /// with three arguments, and stops it.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            BoggleServer.BoggleServer twoArgServer = null;
            BoggleServer.BoggleServer threeArgServer = null;

            try
            {
                // make boggle with 2 arguments
                string[] twoArgs = { "200", "../../../Resources/dictionary.txt" };
                twoArgServer = new BoggleServer.BoggleServer(twoArgs);
            }
            finally
            {
                // close the server
                twoArgServer.Stop();                
            }

            try
            {
                // make boggle with 3 arguments
                string[] threeArgs = { "200", "../../../Resources/dictionary.txt", "jimiergsatnesaps" };
                threeArgServer = new BoggleServer.BoggleServer(threeArgs);            
            }
            finally 
            {
                // close the server
                threeArgServer.Stop();
            }
        }

        /// <summary>
        /// A simple test for BoggleServer for a single player connection
        ///</summary>
        [TestMethod()]
        public void TestMethod2()
        {
            new Test2Class().run();
        }

        public class Test2Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre;            

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient client = null;               

                try
                {
                    string[] args = { "200", "../../../Resources/dictionary.txt" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    client = new TcpClient("localhost", 2000);
                    Socket clientSocket = client.Client;
                    StringSocket player = new StringSocket(clientSocket, new UTF8Encoding());

                    mre = new ManualResetEvent(false);

                    // have player one join the boggle game
                    player.BeginSend("PLAY Basil\n", (o, e) => { mre.Set(); }, null);

                    // waits for one second, expecting the callback to trigger the mre signal
                    Assert.IsTrue(mre.WaitOne(1000));
                }
                finally 
                {
                    server.Stop();
                    client.Close();
                }
                     
            }

        }

        /// <summary>
        /// A simple test for 2 arg BoggleServer for a two player connections
        ///</summary>
        [TestMethod()]
        public void TestMethod3()
        {
            new Test3Class().run();
        }

        public class Test3Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient clientOne = null;
                TcpClient clientTwo = null;

                try
                {
                    string[] args = { "200", "../../../Resources/dictionary.txt" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    clientOne = new TcpClient("localhost", 2000);
                    Socket clientOneSocket = clientOne.Client;
                    StringSocket playerOne = new StringSocket(clientOneSocket, new UTF8Encoding());                    

                    // create player two connection
                    clientTwo = new TcpClient("localhost", 2000);
                    Socket clientTwoSocket = clientTwo.Client;
                    StringSocket playerTwo = new StringSocket(clientTwoSocket, new UTF8Encoding());

                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // have player one join the boggle game
                    playerOne.BeginSend("PLAY Basil\n", (o, e) => { mre1.Set(); }, null);

                    // have player two join the boggle game
                    playerTwo.BeginSend("PLAY Lance\n", (o, e) => { mre2.Set(); }, null);

                    // waits for one second, expecting the callback to trigger the mre1 signal
                    Assert.IsTrue(mre1.WaitOne(1000));

                    // waits for one second, expecting the callback to trigger the mre2 signal
                    Assert.IsTrue(mre2.WaitOne(1000));
                }
                finally
                {
                    server.Stop();
                    clientOne.Close();
                    clientTwo.Close();
                }
            }
        }

        /// <summary>
        /// Tries to create a boggle server with too many arguments
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            // make boggle with too many arguments
            string[] badArgs = { "200", "../../../Resources/dictionary.txt", "ascdefdlsakgldgj", "too many arguments" };
            BoggleServer.BoggleServer badArgServer = new BoggleServer.BoggleServer(badArgs);

            // want to get the result of console as a string?
            //Assert.AreEqual("Invalid number of arguments.", Console.ReadLine().ToString());                
        }

        /// <summary>
        /// Tries to create a boggle server with a string as the time argument that
        /// cannot be parsed to an int
        /// </summary>
        [TestMethod]
        public void TestMethod5()
        {
            // make boggle with too many arguments
            string[] badArgs = { "hello world", "../../../Resources/dictionary.txt" };
            BoggleServer.BoggleServer badArgServer = new BoggleServer.BoggleServer(badArgs);

            // want to get the result of console as a string?            
        }

        /// <summary>
        /// Tries to create a boggle server with a string as the time argument that
        /// can be parsed to an int, but is a negative number
        /// </summary>
        [TestMethod]
        public void TestMethod6()
        {
            // make boggle with too many arguments
            string[] badArgs = { "-200", "../../../Resources/dictionary.txt" };
            BoggleServer.BoggleServer badArgServer = new BoggleServer.BoggleServer(badArgs);

            // want to get the result of console as a string?            
        }

        /// <summary>
        /// Tries to create a boggle server with three parameters, but the third
        /// parameter is not 16 characters long (invalid boggle board size)
        /// </summary>
        [TestMethod]
        public void TestMethod7()
        {             
            // make boggle with too many arguments
            string[] badArgs = { "200", "../../../Resources/dictionary.txt", "jimi" };
            BoggleServer.BoggleServer badArgServer = new BoggleServer.BoggleServer(badArgs);

            // want to get the result of console as a string?            
        }

        /// <summary>
        /// Tries to create a boggle server with three parameters, but the third
        /// parameter contains invalid characters
        /// </summary>
        [TestMethod]
        public void TestMethod8()
        {
            // make boggle with too many arguments
            string[] badArgs1 = { "200", "../../../Resources/dictionary.txt", "jimiergs8tnesaps" };
            string[] badArgs2 = { "200", "../../../Resources/dictionary.txt", "jimiergs*tnesaps" };
            BoggleServer.BoggleServer badArgServer = new BoggleServer.BoggleServer(badArgs1);
            badArgServer = new BoggleServer.BoggleServer(badArgs2);

            // want to get the result of console as a string?            
        }

        /// <summary>
        /// Tries to create a boggle server with empty or null dictionary
        /// </summary>
        [TestMethod]
        public void TestMethod9()
        {
            // make boggle with too many arguments
            string[] badArgs1 = { "200", "", "jimiergsatnesaps" };
            string[] badArgs2 = { "200", null, "jimiergsatnesaps" };
            BoggleServer.BoggleServer badArgServer = new BoggleServer.BoggleServer(badArgs1);
            badArgServer = new BoggleServer.BoggleServer(badArgs2);

            // want to get the result of console as a string?            
        }

        /// <summary>
        /// Tries to create a boggle server with a bad path to dictionary
        /// </summary>
        [TestMethod]
        public void TestMethod10()
        {
            // make boggle with too many arguments
            string[] badArgs = { "200", "dictionary", "jimiergsatnesaps" };            
            BoggleServer.BoggleServer badArgServer = new BoggleServer.BoggleServer(badArgs);            

            // want to get the result of console as a string?            
        }

        /// <summary>
        /// A test for BoggleServer for a single player connection with bad names:
        /// empty name or doesn't start with PLAY 
        ///</summary>
        [TestMethod()]
        public void TestMethod11()
        {
            new Test11Class().run();
        }

        public class Test11Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre;

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient client = null;

                try
                {
                    string[] args = { "200", "../../../Resources/dictionary.txt" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    client = new TcpClient("localhost", 2000);
                    Socket clientSocket = client.Client;
                    StringSocket player = new StringSocket(clientSocket, new UTF8Encoding());

                    mre = new ManualResetEvent(false);

                    // have player one join the boggle game                   
                    player.BeginSend("", (o, e) => { mre.Set(); }, null);
                    player.BeginSend("foo bar", (o, e) => { mre.Set(); }, null);                   

                    // waits for one second, expecting the callback to trigger the mre signal
                    Assert.IsTrue(mre.WaitOne(1000));
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }

            }

        }

        /// <summary>
        /// A simple test for 3 arg BoggleServer for a two player connections
        ///</summary>
        [TestMethod()]
        public void TestMethod12()
        {
            new Test12Class().run();
        }

        public class Test12Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient clientOne = null;
                TcpClient clientTwo = null;

                try
                {
                    string[] args = { "200", "../../../Resources/dictionary.txt", "jimiergsatnesaps" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    clientOne = new TcpClient("localhost", 2000);
                    Socket clientOneSocket = clientOne.Client;
                    StringSocket playerOne = new StringSocket(clientOneSocket, new UTF8Encoding());

                    // create player two connection
                    clientTwo = new TcpClient("localhost", 2000);
                    Socket clientTwoSocket = clientTwo.Client;
                    StringSocket playerTwo = new StringSocket(clientTwoSocket, new UTF8Encoding());

                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // have player one join the boggle game
                    playerOne.BeginSend("PLAY Basil\n", (o, e) => { mre1.Set(); }, null);

                    // have player two join the boggle game
                    playerTwo.BeginSend("PLAY Lance\n", (o, e) => { mre2.Set(); }, null);

                    // waits for one second, expecting the callback to trigger the mre1 signal
                    Assert.IsTrue(mre1.WaitOne(1000));

                    // waits for one second, expecting the callback to trigger the mre2 signal
                    Assert.IsTrue(mre2.WaitOne(1000));
                }
                finally
                {
                    server.Stop();
                    clientOne.Close();
                    clientTwo.Close();
                }

            }

        }

        /// <summary>
        /// A simple test for 3 arg BoggleServer for a two player connections
        /// tests whether the players receive the START game message
        /// sending a word
        ///</summary>
        [TestMethod()]
        public void TestMethod13()
        {
            new Test13Class().run();
        }

        public class Test13Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String s1;
            private String s2;
            private object p1;
            private object p2;

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient clientOne = null;
                TcpClient clientTwo = null;                

                try
                {
                    string[] args = { "200", "../../../Resources/dictionary.txt", "jimiergsatnesaps" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    clientOne = new TcpClient("localhost", 2000);
                    Socket clientOneSocket = clientOne.Client;
                    StringSocket playerOne = new StringSocket(clientOneSocket, new UTF8Encoding());

                    // create player two connection
                    clientTwo = new TcpClient("localhost", 2000);
                    Socket clientTwoSocket = clientTwo.Client;
                    StringSocket playerTwo = new StringSocket(clientTwoSocket, new UTF8Encoding());

                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // have player one join the boggle game
                    playerOne.BeginSend("PLAY Basil\n", (o, e) => { }, null);

                    // have player two join the boggle game
                    playerTwo.BeginSend("PLAY Lance\n", (o, e) => { }, null);                    

                    playerOne.BeginReceive(CompletedReceiveOne, 1);
                    playerTwo.BeginReceive(CompletedReceiveTwo, 2);

                    // waits for one second, expecting the callback to trigger the mre1 signal
                    Assert.IsTrue(mre1.WaitOne(1000));
                    Assert.AreEqual("START JIMIERGSATNESAPS 200 LANCE", s1);
                    Assert.AreEqual(1, p1);

                    // waits for one second, expecting the callback to trigger the mre2 signal
                    Assert.IsTrue(mre2.WaitOne(1000));
                    Assert.AreEqual("START JIMIERGSATNESAPS 200 BASIL", s2);
                    Assert.AreEqual(2, p2);
                }
                finally
                {
                    server.Stop();
                    clientOne.Close();
                    clientTwo.Close();
                }

            }

            /// <summary>
            /// receive callback one
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveOne(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }

            /// <summary>
            /// receive callback two
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveTwo(String s, Exception o, object payload)
            {
                s2 = s;
                p2 = payload;
                mre2.Set();
            }

        }

        /// <summary>
        /// A simple test for 3 arg BoggleServer for a two player connections
        /// tests whether the players receive the START game message
        /// sending a word
        ///</summary>
        [TestMethod()]
        public void TestMethod14()
        {
            new Test14Class().run();
        }

        public class Test14Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String s1;
            private String s2;
            private object p1 = new object();
            private object p2 = new object();

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient clientOne = null;
                TcpClient clientTwo = null;

                try
                {
                    string[] args = { "5", "../../../Resources/dictionary.txt", "jimiergsatnesaps" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    clientOne = new TcpClient("localhost", 2000);
                    Socket clientOneSocket = clientOne.Client;
                    StringSocket playerOne = new StringSocket(clientOneSocket, new UTF8Encoding());

                    // create player two connection
                    clientTwo = new TcpClient("localhost", 2000);
                    Socket clientTwoSocket = clientTwo.Client;
                    StringSocket playerTwo = new StringSocket(clientTwoSocket, new UTF8Encoding());

                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // have player one join the boggle game
                    playerOne.BeginSend("PLAY Basil\n", (o, e) => { }, null);

                    // have player two join the boggle game
                    playerTwo.BeginSend("PLAY Lance\n", (o, e) => { }, null);

                    playerOne.BeginReceive(CompletedReceiveOne, 1);
                    playerTwo.BeginReceive(CompletedReceiveTwo, 2);

                    // waits for one second, expecting the callback to trigger the mre1 signal
                    Assert.IsTrue(mre1.WaitOne(1000));
                    Assert.AreEqual("START JIMIERGSATNESAPS 5 LANCE", s1);
                    Assert.AreEqual(1, p1);

                    // waits for one second, expecting the callback to trigger the mre2 signal
                    Assert.IsTrue(mre2.WaitOne(1000));
                    Assert.AreEqual("START JIMIERGSATNESAPS 5 BASIL", s2);
                    Assert.AreEqual(2, p2);

                    Thread.Sleep(5000);

                    //Check that the timer is working correctly
                    for (int i = 0; i < 5; i++)
                    {
                        //Reset the mre's 
                        mre1.Reset();
                        mre2.Reset();

                        //Try to get the time remaining.
                        playerOne.BeginReceive(CompletedReceiveOne, 1);
                        playerTwo.BeginReceive(CompletedReceiveTwo, 2);

                        // waits for one second, expecting the callback to trigger the mre1 signal
                        Assert.IsTrue(mre1.WaitOne(1000));
                        Assert.AreEqual("TIME "+(4-i), s1);
                        Assert.AreEqual(1, p1);

                        // waits for one second, expecting the callback to trigger the mre2 signal
                        Assert.IsTrue(mre2.WaitOne(1000));
                        Assert.AreEqual("TIME " + (4 - i), s2);
                        Assert.AreEqual(2, p2);
                    }
                }
                finally
                {
                    server.Stop();
                    clientOne.Close();
                    clientTwo.Close();
                }

            }

            /// <summary>
            /// receive callback one
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveOne(String s, Exception o, object payload)
            {
                lock (p1)
                {
                    s1 = s;
                    p1 = payload;
                    mre1.Set();
                }
            }

            /// <summary>
            /// receive callback two
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveTwo(String s, Exception o, object payload)
            {
                lock (p2)
                {
                    s2 = s;
                    p2 = payload;
                    mre2.Set();
                }
            }

        }

        /// <summary>
        /// A simple test for 3 arg BoggleServer for a two player connections
        /// tests whether the players receive the START game message
        /// sending a word
        ///</summary>
        [TestMethod()]
        public void TestMethod16()
        {
            new Test16Class().run();
        }

        public class Test16Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String s1;
            private String s2;
            private object p1 = new object();
            private object p2 = new object();
            private HashSet<string> messagesReceivedOne = new HashSet<string>();
            private HashSet<string> messagesReceivedTwo = new HashSet<string>();

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient clientOne = null;
                TcpClient clientTwo = null;

                try
                {
                    string[] args = { "20", "../../../Resources/dictionary.txt", "serspatglinesers" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    clientOne = new TcpClient("localhost", 2000);
                    Socket clientOneSocket = clientOne.Client;
                    StringSocket playerOne = new StringSocket(clientOneSocket, new UTF8Encoding());

                    // create player two connection
                    clientTwo = new TcpClient("localhost", 2000);
                    Socket clientTwoSocket = clientTwo.Client;
                    StringSocket playerTwo = new StringSocket(clientTwoSocket, new UTF8Encoding());

                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // have player one join the boggle game
                    playerOne.BeginSend("PLAY Basil\n", (o, e) => { }, null);

                    // have player two join the boggle game
                    playerTwo.BeginSend("PLAY Lance\n", (o, e) => { }, null);

                    playerOne.BeginReceive(CompletedReceiveOne, 1);
                    playerTwo.BeginReceive(CompletedReceiveTwo, 2);

                    // waits for one second, expecting the callback to trigger the mre1 signal
                    Assert.IsTrue(mre1.WaitOne(1000));
                    Assert.AreEqual("START SERSPATGLINESERS 20 LANCE", s1);

                    // waits for one second, expecting the callback to trigger the mre2 signal
                    Assert.IsTrue(mre2.WaitOne(1000));
                    Assert.AreEqual("START SERSPATGLINESERS 20 BASIL", s2);

                    //Reset the mres
                    mre1.Reset();
                    mre2.Reset();
                    
                    //Check that the timer is working correctly
                    for (int i = 0; i < 29; i++)
                    {
                        //Try to get the time remaining.
                        playerOne.BeginReceive(CompletedReceiveOne, 1);
                        playerTwo.BeginReceive(CompletedReceiveTwo, 2);
                        mre1.Reset();
                        mre2.Reset();
                    }
                                       
                    playerOne.BeginSend("WORD step\n", (o, e) => { }, null);
                    playerOne.BeginSend("WORD large\n", (o, e) => { }, null);
                    playerOne.BeginSend("WORD larger\n", (o, e) => { }, null);
                    playerOne.BeginSend("WORD genital\n", (o, e) => { }, null);
                    playerOne.BeginSend("WORD trainers\n", (o, e) => { }, null);

                    Thread.Sleep(22000);                   
                }
                finally
                {
                    server.Stop();
                    clientOne.Close();
                    clientTwo.Close();
                }

            }

            /// <summary>
            /// receive callback one
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveOne(String s, Exception o, object payload)
            {
                lock (p1)
                {
                    messagesReceivedOne.Add(s);
                    s1 = s;
                    mre1.Set();
                }
            }

            /// <summary>
            /// receive callback two
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveTwo(String s, Exception o, object payload)
            {
                lock (p2)
                {
                    messagesReceivedTwo.Add(s);
                    s2 = s;
                    mre2.Set();
                }
            }

        }

        /// <summary>
        /// A simple test for 3 arg BoggleServer for a two player connections
        /// tests whether the players receive the START game message
        /// sending a word
        ///</summary>
        [TestMethod()]
        public void TestMethod15()
        {
            new Test15Class().run();
        }

        public class Test15Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String s1;
            private String s2;
            private object p1 = new object();
            private object p2 = new object();
            private HashSet<string> messagesReceivedOne = new HashSet<string>();
            private HashSet<string> messagesReceivedTwo = new HashSet<string>();

            public void run()
            {
                BoggleServer.BoggleServer server = null;
                TcpClient clientOne = null;
                TcpClient clientTwo = null;

                try
                {
                    string[] args = { "20", "../../../Resources/dictionary.txt", "jimiergsatnesaps" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    clientOne = new TcpClient("localhost", 2000);
                    Socket clientOneSocket = clientOne.Client;
                    StringSocket playerOne = new StringSocket(clientOneSocket, new UTF8Encoding());

                    // create player two connection
                    clientTwo = new TcpClient("localhost", 2000);
                    Socket clientTwoSocket = clientTwo.Client;
                    StringSocket playerTwo = new StringSocket(clientTwoSocket, new UTF8Encoding());

                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // have player one join the boggle game
                    playerOne.BeginSend("PLAY Basil\n", (o, e) => { }, null);

                    // have player two join the boggle game
                    playerTwo.BeginSend("PLAY Lance\n", (o, e) => { }, null);

                    playerOne.BeginReceive(CompletedReceiveOne, 1);
                    playerTwo.BeginReceive(CompletedReceiveTwo, 2);

                    // waits for one second, expecting the callback to trigger the mre1 signal
                    Assert.IsTrue(mre1.WaitOne(1000));
                    Assert.AreEqual("START JIMIERGSATNESAPS 20 LANCE", s1);

                    // waits for one second, expecting the callback to trigger the mre2 signal
                    Assert.IsTrue(mre2.WaitOne(1000));
                    Assert.AreEqual("START JIMIERGSATNESAPS 20 BASIL", s2);

                    //Reset the mres
                    mre1.Reset();
                    mre2.Reset();


                    //Check that the timer is working correctly
                    for (int i = 0; i < 29; i++)
                    {
                        //Try to get the time remaining.
                        playerOne.BeginReceive(CompletedReceiveOne, 1);
                        playerTwo.BeginReceive(CompletedReceiveTwo, 2);
                        mre1.Reset();
                        mre2.Reset();
                    }

                    playerOne.BeginSend("WORD to\n", (o, e) => { }, null);
                    playerOne.BeginSend("WORD sap\n", (o, e) => { }, null);
                    playerOne.BeginSend("WORD seat\n", (o, e) => { }, null);
                    playerOne.BeginSend("WORD sat\n", (o, e) => { }, null);

                    Thread.Sleep(11000);

                    playerTwo.BeginSend("WORD to\n", (o, e) => { }, null);
                    playerTwo.BeginSend("WORD sap\n", (o, e) => { }, null);
                    playerTwo.BeginSend("WORD irta\n", (o, e) => { }, null);
                    playerTwo.BeginSend("WORD rat\n", (o, e) => { }, null);

                    //add one for to
                    messagesReceivedOne.Add("SCORE " + 1 + " " + 0);
                    messagesReceivedOne.Add("SCORE " + 0 + " " + 0);
                    messagesReceivedOne.Add("SCORE " + 1 + " " + 0);

                    //add one for to
                    messagesReceivedTwo.Add("SCORE " + 0 + " " + 0);
                    messagesReceivedTwo.Add("SCORE " + 0 + " " + 0);
                    messagesReceivedTwo.Add("SCORE " + 1 + " " + 0);

                    Thread.Sleep(11000);

                    ////Do asserts
                    //// waits for one second, expecting the callback to trigger the mre1 signal
                    //Assert.IsTrue(mre1.WaitOne(1000));
                    //Assert.AreEqual("STOP 1 sat 1 rat 1 sap 1 seat 1 irta", s1);

                    //// waits for one second, expecting the callback to trigger the mre2 signal
                    //Assert.IsTrue(mre2.WaitOne(1000));
                    //Assert.AreEqual("STOP 1 rat 1 sat 1 sap 1 irta 1 seat", s2);
                }
                finally
                {
                    server.Stop();
                    clientOne.Close();
                    clientTwo.Close();
                }

            }

            /// <summary>
            /// receive callback one
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveOne(String s, Exception o, object payload)
            {
                lock (p1)
                {
                    messagesReceivedOne.Add(s);
                    s1 = s;
                    mre1.Set();
                }
            }

            /// <summary>
            /// receive callback two
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void CompletedReceiveTwo(String s, Exception o, object payload)
            {
                lock (p2)
                {
                    messagesReceivedTwo.Add(s);
                    s2 = s;
                    mre2.Set();
                }
            }

        }
    }
}
