using CustomNetworking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GradingTester
{
    /// <summary>
    ///This is a test class for StringSocketTest and is intended
    ///to contain all StringSocketTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringSocketTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region Staff Tests
        /// <summary>
        /// Opens and returns (with out parameters) a pair of communicating sockets.
        /// </summary>
        private static void OpenSockets(int port, out TcpListener server, out Socket s1, out Socket s2)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            TcpClient client = new TcpClient("localhost", port);
            s1 = server.AcceptSocket();
            s2 = client.Client;
        }

        /// <summary>
        /// Closes stuff down
        /// </summary>
        private static void CloseSockets(TcpListener server, Socket s1, Socket s2)
        {
            try
            {
                s1.Shutdown(SocketShutdown.Both);
                s1.Close();
            }
            finally
            {
            }
            try
            {
                s2.Shutdown(SocketShutdown.Both);
                s2.Close();
            }
            finally
            {
            }
            try
            {
                server.Stop();
            }
            finally
            {
            }
        }

        /// <summary>
        /// Tests whether StringSocket can receive a line of text
        /// </summary>
        [TestMethod()]
        public void Test1_2() { new Test1Class().run(4001); }
        public class Test1Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    SS sender = new SS(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    sender.BeginSend("Hello\n", (e, p) => { }, null);
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual("Hello", line);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Tests whether StringSocket can send a line of text
        /// </summary>
        [TestMethod()]
        public void Test2_2() { new Test2Class().run(4002); }
        public class Test2Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    SS receiver = new SS(s2, new UTF8Encoding());
                    sender.BeginSend("Hello\n", (e, p) => { }, null);
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual("Hello", line);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test1, make sure payload comes through.
        /// </summary>
        [TestMethod()]
        public void Test3_2() { new Test3Class().run(4003); }
        public class Test3Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                object payload = null;
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    SS sender = new SS(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    sender.BeginSend("Hello\n", (e, p) => { payload = p; mre.Set(); }, "Payload");
                    mre.WaitOne();
                    Assert.AreEqual("Payload", payload);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test2, make sure the payload comes through
        /// </summary>
        [TestMethod()]
        public void Test4_2() { new Test4Class().run(4004); }
        public class Test4Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                object payload = null;
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    SS receiver = new SS(s2, new UTF8Encoding());
                    sender.BeginSend("Hello\n", (e, p) => { payload = p; mre.Set(); }, "Payload");
                    mre.WaitOne();
                    Assert.AreEqual("Payload", payload);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test1, but send one character at a time.
        /// </summary>
        [TestMethod()]
        public void Test5_2() { new Test5Class().run(4005); }
        public class Test5Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    SS sender = new SS(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    foreach (char c in "Hello\n")
                    {
                        sender.BeginSend(c.ToString(), (e, p) => { }, null);
                    }
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual("Hello", line);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test2, but send one character at a time.
        /// </summary>
        [TestMethod()]
        public void Test6_2() { new Test6Class().run(4006); }
        public class Test6Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    SS receiver = new SS(s2, new UTF8Encoding());
                    foreach (char c in "Hello\n")
                    {
                        sender.BeginSend(c.ToString(), (e, p) => { }, null);
                    }
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual("Hello", line);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test1, but send a very long string.
        /// </summary>
        [TestMethod()]
        public void Test7_2() { new Test7Class().run(4007); }
        public class Test7Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    SS sender = new SS(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    StringBuilder text = new StringBuilder();
                    for (int i = 0; i < 100000; i++)
                    {
                        text.Append(i);
                    }
                    String str = text.ToString();
                    text.Append('\n');
                    sender.BeginSend(text.ToString(), (e, p) => { }, null);
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual(str, line);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test1, but send a very long string.
        /// </summary>
        [TestMethod()]
        public void Test8_2() { new Test8Class().run(4008); }
        public class Test8Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    SS receiver = new SS(s2, new UTF8Encoding());
                    StringBuilder text = new StringBuilder();
                    for (int i = 0; i < 100000; i++)
                    {
                        text.Append(i);
                    }
                    String str = text.ToString();
                    text.Append('\n');
                    sender.BeginSend(text.ToString(), (e, p) => { }, null);
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual(str, line);
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Send multiple lines, make sure they're received in order.
        /// </summary>
        [TestMethod()]
        public void Test9_2() { new Test9Class().run(4009); }
        public class Test9Class
        {
            public void run(int port)
            {
                int LIMIT = 1000;
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String[] lines = new String[LIMIT];
                ManualResetEvent mre = new ManualResetEvent(false);
                int count = 0;

                try
                {
                    SS sender = new SS(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    for (int i = 0; i < LIMIT; i++)
                    {
                        receiver.BeginReceive((s, e, p) => { lines[(int)p] = s; Interlocked.Increment(ref count); }, i);
                    }
                    for (int i = 0; i < LIMIT; i++)
                    {
                        sender.BeginSend(i.ToString() + "\n", (e, p) => { }, null);
                    }
                    SpinWait.SpinUntil(() => count == LIMIT);
                    for (int i = 0; i < LIMIT; i++)
                    {
                        Assert.AreEqual(i.ToString(), lines[i]);
                    }
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test9, but the class being tested does the sending.
        /// </summary>
        [TestMethod()]
        public void Test10_2() { new Test10Class().run(4010); }
        public class Test10Class
        {
            public void run(int port)
            {
                int LIMIT = 1000;
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String[] lines = new String[LIMIT];
                ManualResetEvent mre = new ManualResetEvent(false);
                int count = 0;

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    SS receiver = new SS(s2, new UTF8Encoding());
                    for (int i = 0; i < LIMIT; i++)
                    {
                        receiver.BeginReceive((s, e, p) => { lines[(int)p] = s; Interlocked.Increment(ref count); }, i);
                    }
                    for (int i = 0; i < LIMIT; i++)
                    {
                        sender.BeginSend(i.ToString() + "\n", (e, p) => { }, null);
                    }
                    SpinWait.SpinUntil(() => count == LIMIT);
                    for (int i = 0; i < LIMIT; i++)
                    {
                        Assert.AreEqual(i.ToString(), lines[i]);
                    }
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test9, except the receive calls are made on separate threads.
        /// </summary>
        [TestMethod()]
        public void Test11_2() { new Test11Class().run(4011); }
        public class Test11Class
        {
            public void run(int port)
            {
                int LIMIT = 1000;
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                List<int> lines = new List<int>();

                try
                {
                    SS sender = new SS(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    for (int i = 0; i < LIMIT; i++)
                    {
                        ThreadPool.QueueUserWorkItem(x =>
                            receiver.BeginReceive((s, e, p) => { lock (lines) { lines.Add(Int32.Parse(s)); } }, null)
                            );
                    }
                    for (int i = 0; i < LIMIT; i++)
                    {
                        sender.BeginSend(i.ToString() + "\n", (e, p) => { }, null);
                    }
                    SpinWait.SpinUntil(() => { lock (lines) { return lines.Count == LIMIT; } });
                    lines.Sort();
                    for (int i = 0; i < LIMIT; i++)
                    {
                        Assert.AreEqual(i, lines[i]);
                    }
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test10, except the send calls are made on separate threads.
        /// </summary>
        [TestMethod()]
        public void Test12_2() { new Test12Class().run(4012); }
        public class Test12Class
        {
            public void run(int port)
            {
                int LIMIT = 1000;
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                List<int> lines = new List<int>();

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    SS receiver = new SS(s2, new UTF8Encoding());
                    for (int i = 0; i < LIMIT; i++)
                    {
                        receiver.BeginReceive((s, e, p) => { lock (lines) { lines.Add(Int32.Parse(s)); } }, null);
                    }
                    for (int i = 0; i < LIMIT; i++)
                    {
                        String s = i.ToString();
                        ThreadPool.QueueUserWorkItem(x =>
                            sender.BeginSend(s + "\n", (e, p) => { }, null));
                    }
                    SpinWait.SpinUntil(() => { lock (lines) { return lines.Count == LIMIT; } });
                    lines.Sort();
                    for (int i = 0; i < LIMIT; i++)
                    {
                        Assert.AreEqual(i, lines[i]);
                    }
                }
                finally
                {
                    //CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        ///A simple test for BeginSend and BeginReceive
        ///</summary>
        [TestMethod()]
        public void Test13_2() { new Test13Class().run(4013); }
        public class Test13Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String s1;
            private object p1;
            private String s2;
            private object p2;

            // Timeout used in test case
            private static int timeout = 2000;

            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;

                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);

                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;

                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());

                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // Make two receive requests
                    receiveSocket.BeginReceive(CompletedReceive1, 1);
                    receiveSocket.BeginReceive(CompletedReceive2, 2);

                    // Now send the data.  Hope those receive requests didn't block!
                    String msg = "Hello world\nThis is a test\n";
                    foreach (char c in msg)
                    {
                        sendSocket.BeginSend(c.ToString(), (e, o) => { }, null);
                    }

                    // Make sure the lines were received properly.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual("Hello world", s1);
                    Assert.AreEqual(1, p1);

                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual("This is a test", s2);
                    Assert.AreEqual(2, p2);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }

            // This is the callback for the first receive request.  We can't make assertions anywhere
            // but the main thread, so we write the values to member variables so they can be tested
            // on the main thread.
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }

            // This is the callback for the second receive request.
            private void CompletedReceive2(String s, Exception o, object payload)
            {
                s2 = s;
                p2 = payload;
                mre2.Set();
            }
        }


        /// <summary>
        /// Puts two BeginReceives into action, then sends two lines, one character at
        /// a time.  Makes sure that the two strings arrive in the right order.
        /// </summary>
        [TestMethod()]
        public void Test14_2() { new Test14Class().run(4014); }
        public class Test14Class
        {
            public void run(int port)
            {
                TcpListener server = null;
                TcpClient client = null;

                try
                {
                    // Create and start a server and client
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);

                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;

                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());

                    // Set up the events
                    e1 = new ManualResetEvent(false);
                    e2 = new ManualResetEvent(false);
                    e3 = new ManualResetEvent(false);

                    // Ask for two lines of text.
                    receiveSocket.BeginReceive(Callback1, null);
                    receiveSocket.BeginReceive(Callback2, null);

                    // Send some text, one character at a time
                    foreach (char c in message1 + "\n" + message2 + "\n")
                    {
                        sendSocket.BeginSend(c.ToString(), SendCallback, null);
                    }

                    // Make sure everything happened properly.
                    Assert.IsTrue(e1.WaitOne(2000));
                    Assert.IsTrue(e2.WaitOne(2000));
                    Assert.IsTrue(e3.WaitOne(2000));

                    Assert.AreEqual(message1, string1);
                    Assert.AreEqual(message2, string2);
                    Assert.AreEqual(message1.Length + message2.Length + 2, count);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }

            // The two strings that are sent
            private String message1 = "This is a test";
            private String message2 = "This is another test";

            // The two strings that are received
            private String string1;
            private String string2;

            // Number of times the SendCallback is called
            private int count;

            // For coordinating parallel activity
            private ManualResetEvent e1;
            private ManualResetEvent e2;
            private ManualResetEvent e3;

            // Callback for the first receive
            public void Callback1(String s, Exception e, object payload)
            {
                string1 = s;
                e1.Set();
            }

            // Callback for the second receive
            public void Callback2(String s, Exception e, object payload)
            {
                string2 = s;
                e2.Set();
            }

            // Callback for the send
            public void SendCallback(Exception e, object payload)
            {
                lock (e3)
                {
                    count++;
                }
                if (count == message1.Length + message2.Length + 2)
                {
                    e3.Set();
                }
            }

        }

        /// <summary>
        /// Use two threads to send threads to a single receiver, then makes sure that
        /// they arrived in the proper order.
        /// </summary>
        [TestMethod()]
        public void Test15_4() { new Test15Class().run(4015); }
        public class Test15Class
        {
            // Received strings are collected here
            private List<String> strings = new List<String>();

            // For serializing the executions of BeginReceive
            private ManualResetEvent mre = new ManualResetEvent(false);

            // Number of strings to send per thread
            private static int COUNT = 10000;

            // Timeout for the ManualResetEvent
            private static int TIMEOUT = 5000;

            public void run(int port)
            {
                TcpListener server = null;
                TcpClient client = null;

                try
                {
                    // Create and start a server and client
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);

                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;

                    // Make sure we're connected.
                    Assert.IsTrue(serverSocket.Connected);
                    Assert.IsTrue(clientSocket.Connected);

                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());

                    // Use two threads to blast strings into the socket
                    Thread t1 = new Thread(() => Blast1(sendSocket));
                    Thread t2 = new Thread(() => Blast2(sendSocket));
                    t1.IsBackground = true;
                    t2.IsBackground = true;
                    t1.Start();
                    t2.Start();

                    // Receive all those messages
                    for (int i = 0; i < 2 * COUNT; i++)
                    {
                        receiveSocket.BeginReceive(Receiver, i);
                        Assert.IsTrue(mre.WaitOne(TIMEOUT), "Didn't receive in timely fashion " + i);
                        mre.Reset();
                    }

                    // Make sure that the strings arrived in the proper order
                    int blast1Count = 0;
                    int blast2Count = 0;

                    foreach (String s in strings)
                    {
                        if (s.StartsWith("Blast1"))
                        {
                            Assert.AreEqual(blast1Count, Int32.Parse(s.Substring(7)), "Bad Blast1: " + s);
                            blast1Count++;
                        }
                        else if (s.StartsWith("Blast2"))
                        {
                            Assert.AreEqual(blast2Count, Int32.Parse(s.Substring(7)), "Bad Blast2: " + s);
                            blast2Count++;
                        }
                        else
                        {
                            Assert.Fail("Bad string: " + s);
                        }
                    }
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }

            }

            /// <summary>
            /// Sends COUNT strings that begin with "Blast1" followed by a number.
            /// </summary>
            public void Blast1(StringSocket s)
            {
                for (int i = 0; i < COUNT; i++)
                {
                    s.BeginSend("Blast1 " + i + "\n", (e, p) => { }, null);
                }
            }

            /// <summary>
            /// Sends COUNT strings that begin with "Blast2" followed by a number.
            /// </summary>
            public void Blast2(StringSocket s)
            {
                for (int i = 0; i < COUNT; i++)
                {
                    s.BeginSend("Blast2 " + i + "\n", (e, p) => { }, null);
                }
            }

            /// <summary>
            /// The callback for receiving strings.  Adds to the strings list
            /// and signals.
            /// </summary>
            public void Receiver(String s, Exception e, object payload)
            {
                lock (strings)
                {
                    strings.Add(s);
                    mre.Set();
                }
            }

        }

        /// <summary>
        /// This test fires off multiple senders and receivers, all running on different
        /// threads.  It makes sure that the strings that are send out are all received.
        /// </summary>
        [TestMethod()]
        public void Test16_5() { new Test16Class().run(4016); }
        public class Test16Class
        {
            private int VERSIONS = 10;         // Number of sending threads; number of receiving threads
            private int COUNT = 5000;          // Number of strings sent/received per thread

            // Collects strings received by receiving threads.
            private List<string> received = new List<string>();

            public void run(int port)
            {
                TcpListener server = null;
                TcpClient client = null;

                try
                {
                    // Create and start a server and client
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);

                    // Obtain the sockets from the two ends of the connection.  
                    // We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;

                    // Make sure we're connected.
                    Assert.IsTrue(serverSocket.Connected);
                    Assert.IsTrue(clientSocket.Connected);

                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());

                    // Create a bunch of threads to write to and read from the socket
                    Thread[] threads = new Thread[VERSIONS * 2];
                    for (int i = 0; i < 2 * VERSIONS; i += 2)
                    {
                        threads[i] = new Thread(new ParameterizedThreadStart(v => Sender((int)v, sendSocket)));
                        threads[i + 1] = new Thread(new ParameterizedThreadStart(v => Receiver(receiveSocket)));
                    }

                    // Launch all the threads
                    for (int i = 0; i < threads.Length; i++)
                    {
                        threads[i].IsBackground = true;
                        threads[i].Start(i / 2);
                    }

                    // Wait for all the threads to finish
                    for (int i = 0; i < threads.Length; i++)
                    {
                        threads[i].Join();
                    }

                    // Make sure everything came through properly.  This is where all the
                    // correctness assertions are.
                    received.Sort();
                    for (int v = 0; v < VERSIONS; v++)
                    {
                        string expected = "";
                        char c = (char)('A' + v);
                        for (int i = 0; i < COUNT; i++)
                        {
                            expected += c;
                            Assert.AreEqual(expected, received[v * COUNT + i]);
                        }
                    }
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }

            }

            /// <summary>
            /// Sends strings consisting of the character 'A' + version.  Strings are sent in
            /// the pattern A, AA, AAA, and so on.  COUNT such strings are sent.
            /// </summary>
            private void Sender(int version, StringSocket socket)
            {
                // Determine what character to use
                char c = (char)('A' + version);

                int count = 0;     // Number if times callback has been called
                String msg = "";   // String to send

                // Sent COUNT strings
                for (int i = 0; i < COUNT; i++)
                {
                    // Make the message one character longer
                    msg += c;

                    // Send the message.  The callback atomically updates count.
                    socket.BeginSend(msg + "\n", (e, p) => { Interlocked.Increment(ref count); }, null);
                }

                // Wait until all of the callbacks have been called
                SpinWait.SpinUntil(() => count == COUNT);
            }

            /// <summary>
            /// Receives COUNT strings and appends them to the received list.
            /// </summary>
            private void Receiver(StringSocket socket)
            {
                int count = 0;    // Number of times callback has been called

                // Receive COUNT strings
                for (int i = 0; i < COUNT; i++)
                {
                    // Receive one string.  The callback appends to the list and updates count
                    socket.BeginReceive((s, e, p) => { lock (received) { received.Add(s); count++; } }, i);
                }

                // Wait until all of the callbacks have been called.
                SpinWait.SpinUntil(() => count == COUNT);
            }

        }

        /// <summary>
        /// This test makes sure that StringSockets exhibit the proper non-blocking behavior.
        /// </summary>
        [TestMethod()]
        public void Test17_4() { new Test17Class().run(4017); }
        public class Test17Class
        {
            public void run(int port)
            {
                TcpListener server = null;
                TcpClient client = null;

                try
                {
                    // Create and start a server and client
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);

                    // Obtain the sockets from the two ends of the connection.  
                    // We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;

                    // Make sure we're connected.
                    Assert.IsTrue(serverSocket.Connected);
                    Assert.IsTrue(clientSocket.Connected);

                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());

                    // We use this Stopwatch to detect blocking behavior
                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    // Coordinate the test cases
                    ManualResetEvent mre1 = new ManualResetEvent(false);
                    ManualResetEvent mre2 = new ManualResetEvent(false);
                    ManualResetEvent mre3 = new ManualResetEvent(false);


                    //************************************** TEST1 *********************************************

                    // Make sure that the call to BeginReceive does not block and that its callback runs after the call to BeginSend.
                    long sendTime = 0;
                    long receiveTime = 0;
                    receiveSocket.BeginReceive((s, e, p) => { receiveTime = watch.ElapsedTicks; mre1.Set(); }, null);
                    sendTime = watch.ElapsedTicks;
                    sendSocket.BeginSend("Hello\n", (e, p) => { mre2.Set(); }, null);
                    mre1.WaitOne();
                    mre2.WaitOne();

                    // Make sure things happened in the expected order
                    Assert.IsTrue(sendTime < receiveTime, "Receive happened before send");


                    //************************************** TEST2 *********************************************

                    // Make sure that a blocked receive callback does not block the socket
                    mre1.Reset();
                    mre2.Reset();
                    sendSocket.BeginSend("Hello\n", (e, p) => { }, null);
                    receiveSocket.BeginReceive((s, e, p) => { mre2.Set(); mre1.WaitOne(); mre3.Set(); }, null);
                    mre2.WaitOne();
                    mre2.Reset();
                    sendSocket.BeginSend("there\n", (e, p) => { }, null);
                    receiveSocket.BeginReceive((s, e, p) => { mre2.Set(); }, null);
                    mre2.WaitOne();
                    mre1.Set();
                    mre3.WaitOne();


                    //************************************** TEST3 *********************************************

                    // Make sure that a blocked send callback does not block the socket
                    mre1.Reset();
                    mre2.Reset();
                    mre3.Reset();
                    sendSocket.BeginSend("Hello\n", (e, p) => { mre2.Set(); mre1.WaitOne(); mre3.Set(); }, null);
                    mre2.WaitOne();
                    mre2.Reset();
                    sendSocket.BeginSend("there\n", (e, p) => { mre2.Set(); }, null);
                    mre2.WaitOne();
                    mre1.Set();
                    mre3.WaitOne();


                    //************************************** TEST4 *********************************************

                    // Make sure that one call to BeginReceive cannot block another
                    mre1.Reset();
                    mre2.Reset();
                    mre3.Reset();
                    receiveSocket.BeginReceive((s, e, p) => { mre1.Set(); }, null);
                    receiveSocket.BeginReceive((s, e, p) => { mre2.Set(); }, null);
                    sendSocket.BeginSend("Hello\nthere\n", (e, p) => { mre3.Set(); }, null);
                    mre1.WaitOne();
                    mre2.WaitOne();
                    mre3.WaitOne();
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
        }
        #endregion

        #region Student Tests

        /// <summary>
        /// Authors: Greg Smith and Jase Bleazard
        /// Attempts sending the newline character by itself. The sockets should
        /// still send and receive a blank String, "".
        /// </summary>
        [TestMethod()]
        public void Test18() { new Test18Class().run(4018); }
        public class Test18Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private String s1;
            private object p1;
            // Timeout used in test case
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    // Make two receive requests
                    receiveSocket.BeginReceive(CompletedReceive1, 1);
                    // Now send the data.  Hope those receive requests didn't block!
                    sendSocket.BeginSend("\n", (e, o) => { }, null);
                    // Make sure the lines were received properly.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual("", s1);
                    Assert.AreEqual(1, p1);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            // This is the callback for the first receive request.  We can't make assertions anywhere
            // but the main thread, so we write the values to member variables so they can be tested
            // on the main thread.
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }
        }

        /// <summary>
        /// Run the original test case 100 times in a row, takes about a minute
        /// </summary>
        [TestMethod()]
        public void Test19()
        {
            int port = 4019;
            for (int i = 0; i < 20; i++)
                new Test1Class().run(port++);
        }

        /// <summary>
        /// Tests to make sure that if Send is called before receive that the string will still be received, and not
        /// discarded. This can happen when the socket is loaded, but does not have any recipients for
        /// it's information.
        ///</summary>
        [TestMethod()]
        public void Test20() { new Test20Class().run(4120); }
        public class Test20Class
        {
            //Data used by the receiveSocket.
            private ManualResetEvent resetEvent;
            private String receivedString;
            private object receivedPayload;
            // Timeout used in test case
            private static int waitTime = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    //Initialize the connection.
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // Communicate between the threads of the test cases
                    resetEvent = new ManualResetEvent(false);
                    //Send the string of data to the socket before receive has been called
                    String msg = "This is a test, bro.\n";
                    sendSocket.BeginSend(msg, (e, o) => { }, null);
                    //Make a receive request after data has been read into the socket.
                    receiveSocket.BeginReceive(CompletedReceive1, 1);
                    //Ensure that the data was received correctly.
                    Assert.AreEqual(true, resetEvent.WaitOne(waitTime), "Timed out waiting 1");
                    Assert.AreEqual("This is a test, bro.", receivedString);
                    Assert.AreEqual(1, receivedPayload);
                }
                finally
                {
                    //Stop the server, and discard the socket connection.
                    server.Stop();
                    client.Close();
                }
            }
            // This is the callback for the receive request.  We can't make assertions anywhere
            // but the main thread, so we write the values to member variables so they can be tested
            // on the main thread.
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                receivedString = s;
                receivedPayload = payload;
                resetEvent.Set();
            }
        }

        /// <author> Conan Zhang and April Martin, modifying code provided by Professor de St. Germain</author>
        /// <date> 11-11-14</date>
        /// <summary>
        /// Tests whether threads are processed in the same order they are received, even if the first thread has a ludicrously long
        /// (and therefore slow) message and the second has a short one.
        /// </summary>
        [TestMethod()]
        public void Test21() { new Test21Class().run(4121); }
        public class Test21Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String slowMsg;
            private object p1;
            private String fastMsg;
            private object p2;
            private int count = 0;
            private int slowOrder;
            private int fastOrder;
            // Timeout used in test case
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // Set slowMsg to an absurdly long string that should take a while to process.
                    // Set fastMsg to a single character.
                    slowMsg = @"{{About|the video game character|other uses|Kirby (disambiguation){{!}}Kirby}}
                    {{Infobox VG character
                    | width = 220px
                    | name = Kirby
                    | image = [[File:Kirby Wii.png|225px]]
                    | caption = Kirby as he appears in ''[[Kirby's Return to Dream Land]]''
                    | series = [[Kirby (series)|''Kirby'' series]]
                    | firstgame = ''[[Kirby's Dream Land]]'' (1992)
                    | creator = [[Masahiro Sakurai]]
                    | artist = Masahiro Sakurai
                    | japanactor = [[Mayumi Tanaka]] (1994)<br>[[Makiko Ohmoto]] (1999-present)
                    }}
                    {{nihongo|'''Kirby'''|????|Kabi}} is a [[Character (arts)|fictional character]] and the protagonist of the 
                    ''[[Kirby (series)|Kirby series]]'' of video games owned by [[Nintendo]]. As one of Nintendo's most famous and familiar icons, 
                    Kirby's round, pink appearance and ability to copy his foe's powers to use as his own has made him a well known figure in video 
                    games, consistently ranked as one of the most iconic video game characters. He made his first appearance in 1992 in ''[[Kirby's 
                    Dream Land]]'' for the [[Game Boy]]. Originally a placeholder, created by [[Masahiro Sakurai]], for the game's early development, 
                    he has since then starred in over 20 games, ranging from [[Action game|action]] [[Platform game|platformers]] to [[Kirby's Pinball
                    Land|pinball]], [[Puzzle game|puzzle]] and [[Kirby Air Ride|racing]] games, and has been featured as a playable fighter in all 
                    ''[[Super Smash Bros.]]'' games. He has also starred in his own [[Kirby: Right Back at Ya|anime]] and manga series. His most 
                    recent appearance is in ''[[Super Smash Bros. for Nintendo 3DS and Wii U]]'', released in 2014 for the [[Nintendo 3DS]] and [[Wii 
                    U]]. Since 1999, he has been voiced by [[Makiko Ohmoto]].
                    Kirby is famous for his ability to inhale objects and creatures to obtain their attributes, as well as his ability to float with 
`                   puffed cheeks. He uses these abilities to rescue various lands, such as his home world of Dream Land, from evil forces and 
                    antagonists, such as [[Dark Matter (Kirby)|Dark Matter]] or [[Nightmare (Kirby)|Nightmare]]. On these adventures he often crosses 
                    paths with his rivals, the gluttonous [[King Dedede]] and the mysterious [[Meta Knight]]. In virtually all his appearances,
                    Kirby is depicted as cheerful, innocent, and food loving but becomes fearless, bold, and brave in the face of danger.
                    == Concept and creation ==";
                    fastMsg = "!";
                    // Send slowMsg before fastMsg
                    sendSocket.BeginSend(slowMsg, slowCallback, 1);
                    sendSocket.BeginSend(fastMsg, fastCallback, 2);
                    // Make sure that (a) neither thread timed out and
                    //(b) slowMsg was sent successfully before fastMsg
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual(0, slowOrder);
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual(1, fastOrder);
                    Assert.AreEqual(2, p2);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            /// <summary>
            /// This is the callback for the first send request.  We can't make assertions anywhere
            /// but the main thread, so we write the values to member variables so they can be tested
            /// on the main thread.
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void slowCallback(Exception o, object payload)
            {
                slowOrder = count;
                count++;
                p1 = payload;
                mre1.Set();
            }
            /// <summary>
            /// This is the callback for the second send request.
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void fastCallback(Exception o, object payload)
            {
                fastOrder = count;
                count++;
                p2 = payload;
                mre2.Set();
            }
        }

        /// <author>Kirk Partridge, Kameron Paulsen</author>
        /// <timecreated>11/12/14</timecreated>
        /// <summary>
        /// This method tests the StringSockets ability
        /// to Send Multiple strings before the BeginReceive
        /// is called.  It Sends both by single characters
        /// and full Strings.
        ///</summary>
        [TestMethod()]
        public void Test22() { new Test22Class().run(4122); }
        public class Test22Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private ManualResetEvent mre3;
            private String s1;
            private object p1;
            private String s2;
            private object p2;
            private String s3;
            private object p3;
            // Timeout used in test case
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    mre3 = new ManualResetEvent(false);
                    // Now send the data.  Hope those receive requests didn't block!
                    String msg = "Hello world\nThis is a test\nStrings";
                    foreach (char c in msg)
                    {
                        sendSocket.BeginSend(c.ToString(), (e, o) => { }, null);
                    }
                    //Second Message to be sent
                    String msg2 = " sure are neat\n";
                    //Send the second message.  Should be appended to the leftovers from the foreach loop ("String").
                    sendSocket.BeginSend(msg2, (e, o) => { }, null);
                    receiveSocket.BeginReceive(CompletedReceive1, 1);
                    receiveSocket.BeginReceive(CompletedReceive2, 2);
                    receiveSocket.BeginReceive(CompletedReceive3, 3);
                    // Make sure the lines were received properly.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual("Hello world", s1);
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual("This is a test", s2);
                    Assert.AreEqual(2, p2);
                    Assert.AreEqual(true, mre3.WaitOne(timeout), "Timed out waiting 3");
                    Assert.AreEqual("Strings sure are neat", s3);
                    Assert.AreEqual(3, p3);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            // This is the callback for the first receive request.  We can't make assertions anywhere
            // but the main thread, so we write the values to member variables so they can be tested
            // on the main thread.
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }
            // This is the callback for the second receive request.
            private void CompletedReceive2(String s, Exception o, object payload)
            {
                s2 = s;
                p2 = payload;
                mre2.Set();
            }
            // This is the callback for the third receive request.
            private void CompletedReceive3(String s, Exception o, object payload)
            {
                s3 = s;
                p3 = payload;
                mre3.Set();
            }
        }

        /// <summary>
        /// <author>Blake McGillis</author>
        /// <datecreated>November 12, 2014</datecreated>
        /// A simple test to ensure that the StringSocket's BeginSend method correctly triggers the user's
        /// sendCallback.
        /// </summary>
        [TestMethod()]
        public void Test23() { new Test23Class().run(4123); }
        public class Test23Class
        {
            //Set up timer and payload variables
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private object p1;
            private object p2;
            // Set up the timeout
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Connect the server and client 
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the connection in StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    //Only BeginSend is called. BeginReceive should not need to be called for BeginSend to work
                    sendSocket.BeginSend("BeginSendTest1", CompletedSend1, 1);
                    sendSocket.BeginSend("BeginSendTest2", CompletedSend2, 2);
                    //Make sure the callbacks were called and the correct payload variables were assigned.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual(2, p2);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            // Callback for the first BeginSend
            private void CompletedSend1(Exception o, object payload)
            {
                p1 = payload;
                mre1.Set();
            }
            // Callback for the second BeginSend
            private void CompletedSend2(Exception o, object payload)
            {
                p2 = payload;
                mre2.Set();
            }
        }

        ///<authors>Basil Vetas, Lance Petersen</authors> 
        ///<date>11/11/14</date>
        ///<summary>
        /// This test is only for the BeginSend() method and tests whether or not
        /// the callback is send after the message is comletely sent. This is done by keeping
        /// a counter variable that is incremented when the callback is called. So if your
        /// BeginSend() did not send a complete message and the callback does not get called,
        /// then the counter will not be incremented and the test will fail.
        /// 
        /// Currently the method only calls BeginSend() once, but if you change the messagesSent
        /// variable, it will increase the number of times we loop through and call BeginSend(), 
        /// and this should be equal to the number of times callback is called, and therefore
        /// equal to the number of times counter is incremented. In short, if you want to change
        /// messagesSend to numbers greater than 1, the test should still pass. 
        /// 
        /// </summary>
        [TestMethod()]
        public void Test24() { new Test24Class().run(4124); }
        public class Test24Class
        {
            // Used in Assert
            int counter = 0;
            // Used in Asser and in loop - if you change this the test should still pass
            int messagesSent = 10;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection. We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // Now send the data. Hope those receive requests didn't block!
                    String msg = "Hello World\nThis is a Test\n";
                    // BeginSend() as many times as messagesSent
                    for (int i = 0; i < messagesSent; i++)
                        sendSocket.BeginSend(msg, Test2Callback, 1);
                    System.Threading.Thread.Sleep(500);
                    Assert.AreEqual(messagesSent, counter);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            /// <summary>
            /// Callback increments the counter
            /// </summary>
            /// <param name="s"></param>
            /// <param name="o"></param>
            /// <param name="payload"></param>
            private void Test2Callback(Exception o, object payload)
            {
                counter++;
            }
        }

        /// <summary>
        /// @Author Eric Albee
        /// @Author Douglas Canada
        /// Testing foreign and odd Chars 
        /// in large quanity to see if the 
        /// socket is recieving the correct sequence.
        /// November 12, 2014
        /// </summary>
        [TestMethod()]
        public void Test25() { new Test25Class().run(4125); }
        public class Test25Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private String s1 = "";
            private object p1;
            // Timeout used in test case
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    // Make a receive request
                    receiveSocket.BeginReceive(CompletedReceive, 1);
                    String msg = "";
                    int SIZE = 1000;
                    string[] letters = new string[SIZE];
                    for (int i = 0; i < SIZE; i++)
                    {
                        letters[i] = (msg + (char)('a' + i));
                    }
                    string sentMessage = "";
                    for (int i = 0; i < SIZE; i++)
                    {
                        sendSocket.BeginSend(letters[i], (e, o) => { }, null);
                        // building the message to be asserted
                        sentMessage += letters[i];
                    }
                    sendSocket.BeginSend("\n", (e, o) => { }, null);
                    // Make sure the line was received properly.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual(sentMessage, s1);
                    Assert.AreEqual(1, p1);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            // This is the callback for the receive request.  We can't make assertions anywhere
            // but the main thread, so we write the values to member variables so they can be tested
            // on the main thread.
            private void CompletedReceive(String s, Exception o, object payload)
            {
                s1 += s;
                p1 = payload;
                mre1.Set();
            }
        }

        /// <summary>
        /// Authors: Clint Wilkinson & Daniel Kenner       
        ///</summary>
        [TestMethod()]
        public void Test26() { new Test26Class().run(4126); }
        public class Test26Class
        {
            public void run(int port)
            {
                new StressTestClass().run(port);
            }
            /// <summary>
            /// Class for Stress Test, based off of Test1Class given as part of PS7.
            /// </summary>
            public class StressTestClass
            {
                // Data that is shared across threads
                private ManualResetEvent mre1;
                private ManualResetEvent mre2;
                private String s1;
                private object p1;
                private String s2;
                private object p2;
                // Timeout used in test case
                private static int timeout = 2000;
                public void run(int port)
                {
                    // Create and start a server and client.
                    TcpListener server = null;
                    TcpClient client = null;
                    try
                    {
                        //setup the server
                        server = new TcpListener(IPAddress.Any, port);
                        server.Start();
                        client = new TcpClient("localhost", port);
                        // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                        // method here, which is OK for a test case.
                        Socket serverSocket = server.AcceptSocket();
                        Socket clientSocket = client.Client;
                        // Wrap the two ends of the connection into StringSockets
                        StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                        StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                        // This will coordinate communication between the threads of the test cases
                        mre1 = new ManualResetEvent(false);
                        mre2 = new ManualResetEvent(false);
                        //test a bunch of little strings
                        for (int i = 0; i <= 25000; i++)
                        {
                            //setup the receive socket
                            receiveSocket.BeginReceive(CompletedReceive1, 1);
                            //generate the string
                            sendSocket.BeginSend("A" + i + "\n", (e, o) => { }, null);
                            //wait a bit
                            Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                            //reset the timer
                            mre1.Reset();
                            //check that we are getting what we are supposed to
                            Assert.AreEqual("A" + i, s1);
                            //write out for debugging.
                            System.Diagnostics.Debug.WriteLine(s1);
                        }
                        //generate a big string to test with
                        String stress = "";
                        Random rand = new Random();
                        //put in character by character
                        for (int i = 0; i <= 25000; i++)
                        {
                            stress += ((char)(65 + rand.Next(26))).ToString();
                        }
                        //setup the receiver socket
                        receiveSocket.BeginReceive(CompletedReceive2, 2);
                        //send the big string
                        sendSocket.BeginSend(stress + "\n", (e, o) => { }, null);
                        System.Diagnostics.Debug.WriteLine(stress);
                        // Now send the data.  Hope those receive requests didn't block!
                        Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                        Assert.AreEqual(stress, s2);
                        Assert.AreEqual(2, p2);
                        //separate from last test
                        System.Diagnostics.Debug.WriteLine("");
                        //write for debugging
                        System.Diagnostics.Debug.WriteLine(s2);
                    }
                    finally
                    {
                        server.Stop();
                        client.Close();
                    }
                }
                // This is the callback for the first receive request.  We can't make assertions anywhere
                // but the main thread, so we write the values to member variables so they can be tested
                // on the main thread.
                private void CompletedReceive1(String s, Exception o, object payload)
                {
                    s1 = s;
                    p1 = payload;
                    mre1.Set();
                }
                // This is the callback for the second receive request.
                private void CompletedReceive2(String s, Exception o, object payload)
                {
                    s2 = s;
                    p2 = payload;
                    mre2.Set();
                }
            }
        }

        // Test 27 was removed due to having an effect on the following test

        /// Created by: Sam Trout and Sam England
        /// <summary>
        /// Test case checks whether or not the callback method is sent on its own threadpool. Fails if it times out because 
        /// the thread is blocked.
        /// </summary>
        [TestMethod()]
        public void Test28() { new Test28Class().run(4128); }
        public class Test28Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre = new ManualResetEvent(false);
            // Timeout used in test case
            private static int timeout = 20000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection. We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    // Now send the data. Will block after newline if callback doesnt come back in its own threadpool
                    String msg = "Hopefully this works\n";
                    String msg2 = "Second message\n";
                    //calls beginsend 2 times for the different messages
                    sendSocket.BeginSend(msg, callback1, 1);
                    sendSocket.BeginSend(msg2, callback2, 2);
                    Assert.AreEqual(true, mre.WaitOne(timeout), "Timed out, callback1 blocked second BeginSend");
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            /// <summary>
            /// This callback creates an infinite while loop and if not handled properly in StringSocket will cause the program
            /// to timeout and fail
            /// </summary>
            /// <param name="e"> Default </param>
            /// <param name="payload"> Default </param>
            private void callback1(Exception e, object payload)
            {
                while (true) ;
            }
            /// <summary>
            /// This callback is for the 2nd string, this callback wont be called unless handled properly in the StringSocket
            /// mre will never be set and the program will timeout
            /// </summary>
            /// <param name="e"> Default </param>
            /// <param name="payload"> Default</param>
            private void callback2(Exception e, object payload)
            {
                mre.Set();
            }
        }

        /// <summary>
        ///James Watts & Stuart Johnsen
        ///
        ///Tests sending a single long String that contains 4 lines, seperated by "\n". The string is the lyrics
        ///to the chorus of Haddaway's "What is Love?" Lines are placed in the correct order using a sequential 
        ///integer from the callback's payload.
        ///</summary>
        [TestMethod()]
        public void Test29() { new Test29Class().run(4129); }
        public class Test29Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre0;
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private ManualResetEvent mre3;
            //The String to be sent
            String whatIsLove = "What is love?\nBaby don't hurt me,\nDon't hurt me\nNo more!\n";
            //A String[] for the received lines of text. Should contain 4 elements when completed.
            String[] receivedLines = new String[4];
            // Timeout used in test case
            //private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection. We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre0 = new ManualResetEvent(false);
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    mre3 = new ManualResetEvent(false);
                    //Setup 4 BeginReceives to receive the 4 lines.
                    for (int i = 0; i < 4; i++)
                    {
                        receiveSocket.BeginReceive(WhatIsLove_Callback, i);
                    }
                    sendSocket.BeginSend(whatIsLove, (e, o) => { }, null);
                    Thread.Sleep(5000);
                    Assert.AreEqual("What is love?", receivedLines[0]);
                    Assert.AreEqual("Baby don't hurt me,", receivedLines[1]);
                    Assert.AreEqual("Don't hurt me", receivedLines[2]);
                    Assert.AreEqual("No more!", receivedLines[3]);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            } //End run
            /// <summary>
            /// The callback for receive requests, uses the callback's payload to place lines in the correct order
            ///in a String array.
            ///The appropriate ManualResetEvent is chosen based on the callback's payload.
            /// </summary>
            private void WhatIsLove_Callback(String s, Exception o, object payload)
            {
                int index = (int)payload;
                receivedLines[index] = s;
                switch (index)
                {
                    case 0:
                        mre0.Set();
                        break;
                    case 1:
                        mre1.Set();
                        break;
                    case 2:
                        mre2.Set();
                        break;
                    case 3:
                        mre3.Set();
                        break;
                }
            }
        }

        /// <summary>
        /// Test that a long string, then a short string, and then a long one are correctly received in in the right sent order.
        /// Written by Zane Zakraisek and Alex Ferro
        /// </summary>
        [TestMethod()]
        public void Test30() { new Test30Class().run(4130); }
        public class Test30Class
        {
            // Data that is shared across threads
            // Used to ensure the correct testing assertion on the main thread
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private ManualResetEvent mre3;
            private String s1;
            private object p1;
            private String s2;
            private object p2;
            private String s3;
            private object p3;
            // Test strings
            private String shortString = "This is a journey through time!\n";
            private String longString = "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single" +
            "domestic sufficed to serve him. He breakfasted and dined at the club" +
            "or near friends, which is certainly more unusual. He lived alone" +
            "in his house in Saville Row, whither none penetrated. A single\n";
            // Timeout used in test case
            private static int timeout = 2000;
            /// <summary>
            /// Run the test on the specified port
            /// </summary>
            /// <param name="port"></param>
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection. We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    mre3 = new ManualResetEvent(false);
                    receiveSocket.BeginReceive(CompletedReceive1, 1);
                    receiveSocket.BeginReceive(CompletedReceive2, 2);
                    receiveSocket.BeginReceive(CompletedReceive3, 3);
                    sendSocket.BeginSend(longString, (e, o) => { }, null);
                    sendSocket.BeginSend(shortString, (e, o) => { }, null);
                    sendSocket.BeginSend(longString, (e, o) => { }, null);
                    // Make sure the lines were received properly.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual(longString.Replace("\n", ""), s1);
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual(shortString.Replace("\n", ""), s2);
                    Assert.AreEqual(2, p2);
                    Assert.AreEqual(true, mre3.WaitOne(timeout), "Timed out waiting 3");
                    Assert.AreEqual(longString.Replace("\n", ""), s3);
                    Assert.AreEqual(3, p3);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            /// <summary>
            /// This is the callback for the first receive request. We can't make assertions anywhere
            /// but the main thread, so we write the values to member variables so they can be tested
            /// on the main thread.
            /// </summary>
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }
            /// <summary>
            /// This is the callback for the second receive request.
            /// </summary>
            private void CompletedReceive2(String s, Exception o, object payload)
            {
                s2 = s;
                p2 = payload;
                mre2.Set();
            }
            /// <summary>
            /// This is the callback for the third receive request.
            /// </summary>
            private void CompletedReceive3(String s, Exception o, object payload)
            {
                s3 = s;
                p3 = payload;
                mre3.Set();
            }
        }

        /// File: PS7ForumTestCase.cs
        /// Authors: Eric Stubbs, CJ Dimaano
        /// CS 3500 - Fall 2014
        /// <summary>
        /// Authors: CJ Dimaano and Eric Stubbs
        /// Date: November 12, 2014
        /// </summary>
        [TestClass]
        public class Test31Class
        {
            /// <summary>
            /// This tests that a small string with no \n characters and a large 
            /// string with many newline characters will be received in the 
            /// correct order and with their correct payload. 
            /// </summary>
            [TestMethod]
            public void Test31() { new PS7TestCase().run(4131); }
            public class PS7TestCase
            {
                // One short string with no '\n' characters and one long string with many.
                private string reallyLongMessagePart1 = "Lorem ipsum dolor sit amet,";
                private string reallyLongMessagePart2 = " consectetur adipiscing \nelit. Aliquam lacinia eros quis odio convallis, sit amet suscipit quam\n dapibus. Praesent at arcu lacus. Donec eget iaculis felis. Curabitur\n vestibulum molestie volutpat. Donec imperdiet odio a lectus imperdiet, non pulvinar nulla aliquet. Quisque luctus dui elit\n, non accumsan ante interdum et. Phasellus turpis magna, iaculis nec fermentum eget, imperdiet et metus.\n Integer id fermentum elit. Nullam lacinia nisl et purus eleifend, eget imperdiet magna blandit. Sed dignissim pellentesque tortor. Pellentesque vitae consectetur quam. Etiam consectetur ornare laoreet. Pellentesque auctor ac eros et pulvinar. \nNunc dapibus, libero nec \nscelerisque lacinia, magna sapien malesuada ligula, eu tristique ju\nsto risus nec neque. Quisque tincidunt arcu non purus posuere luctus. Suspendisse id lectus in est luctus pellentesque a non mauris. Phasellus ornare mauris ut justo elementum facilisis. Proin sagittis egestas est ac luctus. Donec aliquet gr\navida velit, sollicitudin sagittis augue aliquet et. Pellentesque venenatis accumsan mi quis dictum. Aliquam eu mauris pharetra, volutpat urna at, accumsan enim. Interdum et malesuada fames ac ante ipsum p\nrimis in faucibus. Sed elit turpis, hendrerit dapibus sollicitudin porttitor, convallis et eros. Mauris lacus mi, mollis eget lacinia quis, condimentum eu ex. Suspendisse mollis sit amet neque a congue. Vestibulum scelerisque hendrerit felis sit amet fringilla. \nVestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis tincidunt maximus eros scelerisque volutpat. Quisque scelerisque neque eget dictum fermentum\n. Curabitur eget bibendum arcu. Fusce tempus, nisl vel malesuada commodo, enim lorem sollicitudin libero, ac condimentum tortor nisl sed elit. Nunc est purus, cursus sed justo ut,\n scelerisque posuere nibh. Nulla condimentum tincidunt mauris, sodales dictum ante cursus nec. \nAenean purus nisl, ultricies quis scelerisque nec, \niaculis nec nisl. Fusce eu magna augue. In arcu sem, accumsan vitae ligula ut, porta commodo sapien. Phasellus vel pellentesque risus. Proin in arcu leo. Donec sed dolor et arcu bibendum vehicula sed vel felis. Sed in lacinia diam. Etiam ac elit id ipsum mattis bibendum. \nSuspendisse tempor, elit quis efficitur suscipit, erat erat pellentesque sem, eget elementum nibh tortor vel \nnibh. Curabitur semper lacus non nibh aliquet tincidunt. Praesent porttitor pretium ullamcorper. Fusce consequat ex vitae elit euismod mollis. Cras at ipsum in nisl aliquet euismod ac non\n elit. Integer vitae diam auctor, pellentesque nulla et, vulputate quam. In dictum feugiat blandit. Quisque et tristique sem. Sed egestas ultricies nibh sed auctor. Cras ullamcorper quam sit\n amet lectus fermentum, non feugiat leo molestie. Curabitur imperdiet turpis nec eros pulvinar, eget laoreet tell\nus blandit. Proin sagittis quam sed massa \nluctus sollicitudin. Nulla et eros an\nte.\n";
                // Array to hold the messages sent.
                private string[] messageParts;
                // Array to hold the messages received.
                private string[] messagePartsReceived;
                // An int that keeps track of how many receive callbacks have returned.
                private int receiveCount = 0;
                public void run(int port)
                {
                    TcpListener server = null;
                    TcpClient client = null;
                    // Store each message in a different array bucket.
                    string wholeMessage = reallyLongMessagePart1 + reallyLongMessagePart2;
                    this.messageParts = wholeMessage.Split('\n');
                    messagePartsReceived = new string[messageParts.Length];
                    try
                    {
                        // Run the server and the client.
                        server = new TcpListener(IPAddress.Any, port);
                        server.Start();
                        client = new TcpClient("localhost", port);
                        // Create the sockets for the client and server.
                        Socket serverSocket = server.AcceptSocket();
                        Socket clientSocket = client.Client;
                        // Create StringSockets for the client and server. 
                        StringSocket serverStringSocket = new StringSocket(serverSocket, new UTF8Encoding());
                        StringSocket clientStringSocket = new StringSocket(clientSocket, new UTF8Encoding());
                        // Call a beginReceive for each message (each time there is a \n).
                        for (int i = 0; i < this.messageParts.Length - 1; i++)
                            clientStringSocket.BeginReceive(MessageReceived, i);
                        // Send the two parts of the message.
                        serverStringSocket.BeginSend(reallyLongMessagePart1, (e, o) => { }, null);
                        serverStringSocket.BeginSend(reallyLongMessagePart2, (e, o) => { }, null);
                        // Wait until all the messages have been received back (all the callbacks have been called).
                        while (this.receiveCount != this.messageParts.Length - 1) ;
                        lock (this.messagePartsReceived)
                        {
                            // The array index relates to the payload, so the messages that were sent 
                            // should be the same as the messages received back.
                            for (int i = 0; i < this.messageParts.Length - 1; i++)
                                Assert.AreEqual(this.messageParts[i], this.messagePartsReceived[i]);
                        }
                    }
                    finally
                    {
                        server.Stop();
                        client.Close();
                    }
                }
                /// <summary>
                /// The callback for beginReceive.
                /// </summary>
                private void MessageReceived(String s, Exception e, object payload)
                {
                    lock (this.messagePartsReceived)
                    {
                        // Keep track of each message received
                        this.receiveCount++;
                        // Save the message that was received to the array slot corresponding to its payload number.
                        int i = (int)payload;
                        messagePartsReceived[i] = s;
                    }
                }
            }
        }

        /// <summary>
        ///A simple test for BeginSend and BeginReceive
        ///This is a test to ensure that the user handles a blank message that is not a zero-byte message (empty string)
        /// This test is a modification of the test that Dr. de Saint Germain. 
        /// Modifiers: Daniel Clyde & Alex Whitelock
        ///</summary>
        [TestMethod()]
        public void Test32() { new Test32Class().run(4132); }
        public class Test32Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String emptyMessage;
            private object p1;
            private String finalMessage;
            private object p2;
            // Timeout used in test case
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection. We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    // Make 14 receive requests which will get the data from "Hello world" and 14
                    for (int i = 0; i < 14; i++)
                        receiveSocket.BeginReceive(receiveEmpty, 1);
                    receiveSocket.BeginReceive(recieveFinal, 2);
                    // Now send the data. Hope those receive requests didn't block!
                    //This string contains 13 newLine symbols
                    String lines = "\n\n\n\n\n\n\n\n\n\n\n\n\n";
                    //This string contains "Hello world" followed by a newLine, 13 newLine characters, and then "This is a test" followed by a final newLine character.
                    String msg = "Hello world\n" + lines + "This is a test\n";
                    sendSocket.BeginSend(msg.ToString(), (e, o) => { }, null);
                    //Expect the StringSocket to continue receiving data until the final message is received ("This is a test").
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual("This is a test", finalMessage);
                    Assert.AreEqual(2, p2);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            // This is the callback for all the \n requests
            private void receiveEmpty(String s, Exception o, object payload)
            {
                emptyMessage = s;
                p1 = payload;
                mre1.Set();
            }
            // This is the callback for the final receive request and should capture the this is a test.
            private void recieveFinal(String s, Exception o, object payload)
            {
                finalMessage = s;
                p2 = payload;
                mre2.Set();
            }
        }

        /// <summary>
        /// Val Nicholas Hallstrom
        /// This test makes sure that your program handles sparatic newlines properly.
        /// I used the setupServerClient method from another useful test I found on the forum since the way I was trying to setup the sockets didn't seem to work :/
        /// </summary>
        [TestMethod()]
        public void Test33()
        {
            int timeout = 1000;
            StringSocket sendSocket;
            StringSocket receiveSocket;
            string s1 = "";
            int p1 = 0;
            string s2 = "";
            int p2 = 0;
            string s3 = "";
            int p3 = 0;
            string s4 = "";
            int p4 = 0;
            string s5 = "";
            int p5 = 0;
            ManualResetEvent mre1 = new ManualResetEvent(false);
            ManualResetEvent mre2 = new ManualResetEvent(false);
            ManualResetEvent mre3 = new ManualResetEvent(false);
            ManualResetEvent mre4 = new ManualResetEvent(false);
            ManualResetEvent mre5 = new ManualResetEvent(false);
            setupServerClient(63333, out sendSocket, out receiveSocket);
            receiveSocket.BeginReceive((s, e, p) => { s1 = s; p1 = (int)p; mre1.Set(); }, 1);
            receiveSocket.BeginReceive((s, e, p) => { s2 = s; p2 = (int)p; mre2.Set(); }, 2);
            receiveSocket.BeginReceive((s, e, p) => { s3 = s; p3 = (int)p; mre3.Set(); }, 3);
            receiveSocket.BeginReceive((s, e, p) => { s4 = s; p4 = (int)p; mre4.Set(); }, 4);
            receiveSocket.BeginReceive((s, e, p) => { s5 = s; p5 = (int)p; mre5.Set(); }, 5);
            sendSocket.BeginSend("\nWhat's\n\nup?\n\n", (e, p) => { }, null);
            Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
            Assert.AreEqual("", s1);
            Assert.AreEqual(1, p1);
            Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
            Assert.AreEqual("What's", s2);
            Assert.AreEqual(2, p2);
            Assert.AreEqual(true, mre3.WaitOne(timeout), "Timed out waiting 3");
            Assert.AreEqual("", s3);
            Assert.AreEqual(3, p3);
            Assert.AreEqual(true, mre4.WaitOne(timeout), "Timed out waiting 4");
            Assert.AreEqual("up?", s4);
            Assert.AreEqual(4, p4);
            Assert.AreEqual(true, mre5.WaitOne(timeout), "Timed out waiting 5");
            Assert.AreEqual("", s5);
            Assert.AreEqual(5, p5);
        }
        public void setupServerClient(int port, out StringSocket sendSocket, out StringSocket receiveSocket)
        {
            // Create and start a server and client.
            TcpListener server = null;
            TcpClient client = null;
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            client = new TcpClient("localhost", port);
            // Obtain the sockets from the two ends of the connection. We are using the blocking AcceptSocket()
            // method here, which is OK for a test case.
            Socket serverSocket = server.AcceptSocket();
            Socket clientSocket = client.Client;
            // Wrap the two ends of the connection into StringSockets
            sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
            receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
        }

        /// <summary>
        /// Test Author: Douglas Thompson
        /// 11/12/2014
        /// A simple test for UTF-Characters: ?, é, ™ (trademark), and © (copyright).
        /// The test should complete with the UTF-Characters intact while receiving the string.
        ///</summary>
        [TestMethod()]
        public void Test34() { new Test34Class().run(4134); }
        public class Test34Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private String s1;
            private object p1;
            private String s2;
            private object p2;
            // Timeout used in test case
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    // Make two receive requests
                    receiveSocket.BeginReceive(CompletedReceive1, 1);
                    receiveSocket.BeginReceive(CompletedReceive2, 2);
                    // Now send the data with an integral character, an accent, trademark symbol and copyright character. Hope those receive requests didn't block!
                    String msg = "? 3x dx\nPokémon™ © Nintendo\n";
                    foreach (char c in msg)
                    {
                        sendSocket.BeginSend(c.ToString(), (e, o) => { }, null);
                    }
                    // Make sure the lines were received properly.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual("? 3x dx", s1);
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual("Pokémon™ © Nintendo", s2);
                    Assert.AreEqual(2, p2);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            // This is the callback for the first receive request.  We can't make assertions anywhere
            // but the main thread, so we write the values to member variables so they can be tested
            // on the main thread.
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }
            // This is the callback for the second receive request.
            private void CompletedReceive2(String s, Exception o, object payload)
            {
                s2 = s;
                p2 = payload;
                mre2.Set();
            }
        }

        /// <summary>
        ///Tests a very long string in a different language
        ///</summary>
        [TestMethod()]
        public void Test35() { new Test35Class().run(4135); }
        public class Test35Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            private ManualResetEvent mre3;
            private ManualResetEvent mre4;
            private String s1;
            private object p1;
            private String s2;
            private object p2;
            private String s3;
            private object p3;
            private String s4;
            private object p4;
            // Timeout used in test case
            private static int timeout = 20000;
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    client = new TcpClient("localhost", port);
                    // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                    // method here, which is OK for a test case.
                    Socket serverSocket = server.AcceptSocket();
                    Socket clientSocket = client.Client;
                    // Wrap the two ends of the connection into StringSockets
                    StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                    StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    mre3 = new ManualResetEvent(false);
                    mre4 = new ManualResetEvent(false);
                    // Make two receive requests
                    receiveSocket.BeginReceive(CompletedReceive1, 1);
                    receiveSocket.BeginReceive(CompletedReceive2, 2);
                    receiveSocket.BeginReceive(CompletedReceive3, 3);
                    receiveSocket.BeginReceive(CompletedReceive4, 4);
                    // Now send the data.
                    String msg = "a\nb\nDer er et yndigt land, det står med brede bøge nær salten østerstrand :|Det bugter sig i bakke, dal, det hedder gamle Danmark "
                    + "og det er Frejas sal :| Der sad i fordums tid de harniskklædte kæmper, udhvilede fra strid :| Så drog de frem til fjenders mén, nu hvile deres "
                    + "bene bag højens bautasten :| Det land endnu er skønt, thi blå sig søen bælter, og løvet står så grønt :| Og ædle kvinder, skønne møer og mænd "
                    + "og raske svende bebo de danskes øer :| Hil drot og fædreland! Hil hver en danneborger, som virker, hvad han kan! :| Vort gamle Danmark skal bestå, "
                    + "så længe bøgen spejler sin top i bølgen blå\nc\n";
                    foreach (char c in msg)
                    {
                        sendSocket.BeginSend(c.ToString(), (e, o) => { }, null);
                    }
                    // Make sure the lines were received properly.
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual("a", s1);
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual("b", s2);
                    Assert.AreEqual(2, p2);
                    Assert.AreEqual(true, mre3.WaitOne(timeout), "Timed out waiting 3");
                    Assert.AreEqual("Der er et yndigt land, det står med brede bøge nær salten østerstrand :|Det bugter sig i bakke, dal, det hedder gamle Danmark "
                    + "og det er Frejas sal :| Der sad i fordums tid de harniskklædte kæmper, udhvilede fra strid :| Så drog de frem til fjenders mén, nu hvile deres "
                    + "bene bag højens bautasten :| Det land endnu er skønt, thi blå sig søen bælter, og løvet står så grønt :| Og ædle kvinder, skønne møer og mænd "
                    + "og raske svende bebo de danskes øer :| Hil drot og fædreland! Hil hver en danneborger, som virker, hvad han kan! :| Vort gamle Danmark skal bestå, "
                    + "så længe bøgen spejler sin top i bølgen blå", s3);
                    Assert.AreEqual(3, p3);
                    Assert.AreEqual(true, mre4.WaitOne(timeout), "Timed out waiting 4");
                    Assert.AreEqual("c", s4);
                    Assert.AreEqual(4, p4);
                }
                finally
                {
                    server.Stop();
                    client.Close();
                }
            }
            // This is the callback for the first receive request.  We can't make assertions anywhere
            // but the main thread, so we write the values to member variables so they can be tested
            // on the main thread.
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }
            // This is the callback for the second receive request.
            private void CompletedReceive2(String s, Exception o, object payload)
            {
                s2 = s;
                p2 = payload;
                mre2.Set();
            }
            // This is the callback for the third receive request
            private void CompletedReceive3(String s, Exception o, object payload)
            {
                s3 = s;
                p3 = payload;
                mre3.Set();
            }
            // This is the callback for the fourth receive request.
            private void CompletedReceive4(String s, Exception o, object payload)
            {
                s4 = s;
                p4 = payload;
                mre4.Set();
            }
        }

        /// <summary>
        /// Author: Chaofeng Zhou and PinEn Chen
        /// Testing two client and thread sending
        /// this test is based the test provided by Jim
        ///</summary>
        [TestMethod()]
        public void Test36() { new Test36Class().run(4136); }
        public class Test36Class
        {
            // Data that is shared across threads
            private ManualResetEvent mre1;
            private ManualResetEvent mre2;
            // recieved string from client 1
            private String s1;
            // payload from client 1
            private object p1;
            // recieved string from client 2
            private String s2;
            // payload from client 2
            private object p2;
            // Timeout used in test case
            private static int timeout = 2000;
            public void run(int port)
            {
                // Create and start a server and tow clients.
                TcpListener server = null;
                TcpClient client1 = null;
                TcpClient client2 = null;
                try
                {
                    // create a sever
                    server = new TcpListener(IPAddress.Any, port);
                    // start sever
                    server.Start();
                    // create client1
                    client1 = new TcpClient("localhost", port);
                    // create client2
                    client2 = new TcpClient("localhost", port);
                    // get sockets for the two clients and server
                    Socket serverSocket1 = server.AcceptSocket();
                    Socket clientSocket1 = client1.Client;
                    Socket serverSocket2 = server.AcceptSocket();
                    Socket clientSocket2 = client2.Client;
                    // Wrap the four ends of the connection into StringSockets
                    StringSocket sendSocket1 = new StringSocket(serverSocket1, new UTF8Encoding());
                    StringSocket sendSocket2 = new StringSocket(serverSocket2, new UTF8Encoding());
                    StringSocket receiveSocket1 = new StringSocket(clientSocket1, new UTF8Encoding());
                    StringSocket receiveSocket2 = new StringSocket(clientSocket2, new UTF8Encoding());
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    // Make two receive requests for client 1 and client 2
                    receiveSocket1.BeginReceive(CompletedReceive1, 1);
                    receiveSocket2.BeginReceive(CompletedReceive2, 2);
                    // Now send the data. Hope those receive requests didn't block!
                    String msg1 = "Hello, PinEn Chen.\nHow is your midterm.\n";
                    String msg2 = "Hello\nIt is not your business.\n";
                    // client 1 sends message 1 and client 2 sends messae 2 
                    // using thread
                    ThreadStart threadFunc1 = new ThreadStart(() => SocketSending(msg1, sendSocket1));
                    ThreadStart threadFunc2 = new ThreadStart(() => SocketSending(msg2, sendSocket2));
                    Thread worker1 = new Thread(threadFunc1);
                    Thread worker2 = new Thread(threadFunc2);
                    worker1.Start();
                    worker2.Start();
                    worker1.Join();
                    worker2.Join();
                    // Make sure the first time for the two clients work
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual("Hello, PinEn Chen.", s1);
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual("Hello", s2);
                    Assert.AreEqual(2, p2);
                    // This will coordinate communication between the threads of the test cases
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);
                    receiveSocket1.BeginReceive(CompletedReceive1, 1);
                    receiveSocket2.BeginReceive(CompletedReceive2, 2);
                    // Make sure the second time for the two clients work 
                    Assert.AreEqual(true, mre1.WaitOne(timeout), "Timed out waiting 1");
                    Assert.AreEqual("How is your midterm.", s1);
                    Assert.AreEqual(1, p1);
                    Assert.AreEqual(true, mre2.WaitOne(timeout), "Timed out waiting 2");
                    Assert.AreEqual("It is not your business.", s2);
                    Assert.AreEqual(2, p2);
                }
                finally
                {
                    server.Stop();
                    client1.Close();
                    client2.Close();
                }
            }
            // call back for client 1
            private void CompletedReceive1(String s, Exception o, object payload)
            {
                s1 = s;
                p1 = payload;
                mre1.Set();
            }
            // call back for client 2
            private void CompletedReceive2(String s, Exception o, object payload)
            {
                s2 = s;
                p2 = payload;
                mre2.Set();
            }
            // a function used for sending message through string socket
            // use this method because I want to do this on different thread
            private void SocketSending(String s, StringSocket ss)
            {
                foreach (char c in s)
                {
                    ss.BeginSend(c.ToString(), (e, o) => { }, null);
                }
            }
        }

        /// <summary>
        /// Authors: Brant Nielsen and Janelle Michaelis
        /// 
        /// Tests multithreading functionality. Sets up BeginReceive callbacks on a client from multiple different
        /// concurrent threads, and sends multiple random messages to the client on different concurrent threads.
        /// Makes sure that all messages are received properly (though not in any particular order).
        /// </summary>
        [TestMethod]
        public void Test37() { new Test37Class().run(4137); }
        public class Test37Class
        {
            private Random rng;
            private List<string> sentMessages;
            private StringSocket receiveSocket;
            private StringSocket sendSocket;
            private int messagesToSendPerThread = 100;
            private int randomMessageMinLength = 4;
            private int randomMessageMaxLength = 30;
            private int messagesSentCount;
            private object messagesSentCountLock;
            public void run(int port)
            {
                rng = new Random();
                sentMessages = new List<string>();
                messagesSentCount = 0;
                messagesSentCountLock = new object();
                TcpListener server = new TcpListener(IPAddress.Any, port);
                server.Start();
                TcpClient client = new TcpClient("localhost", port);
                // Obtain the sockets from the two ends of the connection. We are using the blocking AcceptSocket()
                // method here, which is OK for a test case.
                Socket serverSocket = server.AcceptSocket();
                Socket clientSocket = client.Client;
                // Wrap the two ends of the connection into StringSockets
                sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                //Set up BeginReceive callbacks on 3 different threads
                Task waitForMessages1 = new Task(waitForExpectedMessages);
                Task waitForMessages2 = new Task(waitForExpectedMessages);
                Task waitForMessages3 = new Task(waitForExpectedMessages);
                waitForMessages1.Start();
                waitForMessages2.Start();
                waitForMessages3.Start();
                //Wait for all BeginReceive statements to be executed
                while (!(waitForMessages1.IsCompleted && waitForMessages2.IsCompleted && waitForMessages3.IsCompleted))
                {
                    Thread.Sleep(10);
                }
                //Send random messages on 3 different threads
                Task sendRandomMessages1 = new Task(sendRandomMessages);
                Task sendRandomMessages2 = new Task(sendRandomMessages);
                Task sendRandomMessages3 = new Task(sendRandomMessages);
                sendRandomMessages1.Start();
                sendRandomMessages2.Start();
                sendRandomMessages3.Start();
                //Wait for all BeginSend statements to be executed
                while (!(sendRandomMessages1.IsCompleted && sendRandomMessages2.IsCompleted && sendRandomMessages3.IsCompleted))
                {
                    Thread.Sleep(10);
                }
                //Ensure that the messages are given sufficient time to be sent (200ms to be safe)
                Thread.Sleep(200);
                //Ensure that all BeginSend callbacks were executed
                Assert.AreEqual(messagesToSendPerThread * 3, messagesSentCount);
                //Ensure that all messages that were sent were actually received (when a message is received, it's removed from the sentMessages list)
                Assert.AreEqual(0, sentMessages.Count);
            }
            /// <summary>
            /// Thread that will run BeginReceive calls on the receiving socket
            /// </summary>
            private void waitForExpectedMessages()
            {
                for (int i = 0; i < messagesToSendPerThread; i++)
                {
                    receiveSocket.BeginReceive(beginReceiveCallback, null);
                    //One in ten times, sleep for 5 milliseconds. This ensures that the threads will complete at varying speeds.
                    if (rng.Next(10) == 0)
                    {
                        Thread.Sleep(5);
                    }
                }
            }
            /// <summary>
            /// Callback for the BeginReceive method on the receiving socket. Will ensure that all received messages
            /// were actually sent, and will remove properly sent messages from the sentMessages list to help ensure
            /// that all sent messages were received. 
            /// </summary>
            private void beginReceiveCallback(String receivedString, Exception exception, object payload)
            {
                lock (sentMessages)
                {
                    //Was this message acutally sent? The method below will return -1 if it wasn't
                    int receivedStringIndex = sentMessages.IndexOf(receivedString);
                    Assert.AreNotEqual(-1, receivedStringIndex);
                    //Remove the message from the sentMessages list (will help us ensure that all sent messages were received)
                    sentMessages.RemoveAt(receivedStringIndex);
                }
            }
            /// <summary>
            /// Thread that will run BeginSend calls on the sending socket. Will send random messages to the client, and
            /// record that these messsages were sent using the sentMessages List.
            /// </summary>
            private void sendRandomMessages()
            {
                for (int i = 0; i < messagesToSendPerThread; i++)
                {
                    //Generate a random message
                    string message = generateRandomString(rng, randomMessageMinLength, randomMessageMaxLength);
                    //Add this message to our sentMessages list
                    lock (sentMessages)
                    {
                        sentMessages.Add(message);
                    }
                    //Sent the message using the sending string socket
                    sendSocket.BeginSend(message + "\n", (e, p) =>
                    {
                        //Make sure that the callback was actually executed. If there's a discrepency between the number of messages
                        //sent and the number of callbacks executed, we'll know some were not executed correctly.
                        lock (messagesSentCountLock)
                        {
                            messagesSentCount++;
                        }
                    }, null);
                    //One in ten times, sleep for 5 milliseconds. This ensures that the threads will complete at varying speeds.
                    if (rng.Next(10) == 0)
                    {
                        Thread.Sleep(5);
                    }
                }
            }
            /// <summary>
            /// Added as a private member so this doesn't have to be re-created on every call to generateRandomMessage
            /// </summary>
            private readonly string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            /// <summary>
            /// Generates a random alphanumeric string of minLength and maxLength (both inclusive) using the specified
            /// random number generator.
            /// </summary>
            private string generateRandomString(Random rng, int minLength, int maxLength)
            {
                int totalLength = rng.Next(minLength, maxLength + 1);
                StringBuilder finalString = new StringBuilder();
                for (int i = 1; i <= totalLength; i++)
                {
                    finalString.Append(rng.Next(alphanumericCharacters.Length));
                }
                return finalString.ToString();
            }
        }

        /// <summary>
        /// Authors: Jarom Norris and Sarah Cotner
        /// November 2014
        /// University of Utah CS 3500 with Dr. de St. Germain
        /// This is a simple test to make sure that string socket is written to be non-blocking,
        /// regardless of inappropriate callbacks. Uses the functions BlockingTestCallback1 and
        /// BlockingTestCallback2.
        /// </summary>
        [TestMethod()]
        public void Test38()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 4138);
            server.Start();
            TcpClient client = new TcpClient("localhost", 4138);
            Socket serverSocket = server.AcceptSocket();
            Socket clientSocket = client.Client;
            StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
            StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
            sendSocket.BeginSend("This test wa", (e, p) => { }, 1);
            sendSocket.BeginSend("s made by\n", (e, p) => { }, 2);
            sendSocket.BeginSend("Jarom and Sarah!\n", (e, p) => { }, 3);
            receiveSocket.BeginReceive(BlockingTestCallback1, 4);
            receiveSocket.BeginReceive(BlockingTestCallback2, 5);
        }
        public void BlockingTestCallback1(string s, Exception e, object payload)
        {
            while (true) Thread.Sleep(500);
        }
        public void BlockingTestCallback2(string s, Exception e, object payload)
        {
            Assert.AreEqual(s, "Jarom and Sarah!");
            Assert.AreEqual(payload, 5);
        }

        /// <author> Chris Weeter </author>
        /// <summary>
        /// Load tests the Socket by sending 100 messages
        /// </summary>
        [TestMethod()]
        public void Test39() { new Test39Class().run(4139); }
        class Test39Class
        {
            public void run(int port)
            {
                // Create and start a server and client.
                TcpListener server = null;
                TcpClient client = null;
                // Number of runs is set
                int runs = 100;
                string lastMessage = "100\n";
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                client = new TcpClient("localhost", port);
                // Obtain the sockets from the two ends of the connection.                  
                Socket serverSocket = server.AcceptSocket();
                Socket clientSocket = client.Client;
                // Wrap the two ends of the connection into StringSockets
                StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
                // Puts in a receive request to the socket
                receiveSocket.BeginReceive(GetCallBack, null);
                // For loop to run the BeginSend method 100 times, incrementing the message sent each time
                for (int i = 0; i < runs; i++)
                {
                    sendSocket.BeginSend(i.ToString(), SendCallBack, null);
                }
                // Sends the final message with the newline character added on
                sendSocket.BeginSend(lastMessage, SendCallBack, null);
            }
            /// <summary>
            /// Gets the string that was sent over the socket
            /// </summary>
            /// <param name="s"> String message that should contain the message that was passed through the socket</param>
            /// <param name="e"> Exception that should be null</param>
            /// <param name="payload"> Payload that was passed </param>
            void GetCallBack(String s, Exception e, Object payload)
            {
                Assert.IsNotNull(s);
                Assert.AreEqual("0123456789101112131415161718192021222324252627282930313233343536373839404142434445464748495051525354555657585960616263646566676869707172737475767778798081828384858687888990919293949596979899100", s);
                Assert.IsNull(e);
                Assert.IsNull(payload);
            }
            /// <summary>
            /// Send Callback Method to ensure that an exception wasn't thrown after each BeginSend, and that the payload was passed correctly
            /// </summary>
            /// <param name="e">Exception should be null</param>
            /// <param name="payload">Payload should be null </param>
            void SendCallBack(Exception e, Object payload)
            {
                // Checks to make sure that there was no exception thrown
                Assert.IsNull(e);
                Assert.IsNull(payload);
            }
        }
        #endregion
    }
}