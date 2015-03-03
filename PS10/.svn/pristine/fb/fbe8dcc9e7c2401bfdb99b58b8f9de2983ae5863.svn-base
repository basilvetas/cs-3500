using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using BoggleClient;
using BC;
using System.Collections.Generic;

namespace BoggleClientTest
{
    [TestClass]
    public class BoggleClientTest
    {
         ///<summary>
         ///Test connect and disconnect for one player
         ///</summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BoggleClientTest1()
        {
            ManualResetEvent mre1 = new ManualResetEvent(false);
            string[] args = { "30", "..\\..\\..\\Resources\\dictionary.txt" };
            new BoggleServer.BoggleServer(args);
            BoggleClientModel player1 = new BoggleClientModel();
            player1.Connect("localhost");
            player1.Disconnect();
            mre1.WaitOne();
        }

        /// <summary>
        /// Test getters and setters
        /// </summary>
        [TestMethod]
        public void BoggleClientTest2()
        {
            string[] args = { "30", "..\\..\\..\\Resources\\dictionary.txt" };
            new BoggleServer.BoggleServer(args);
            BoggleClientModel player1 = new BoggleClientModel();
            player1.setBoard("ABCDEFGHIJKLMNOP");
            Assert.AreEqual("ABCDEFGHIJKLMNOP", player1.getBoard());
            player1.setPlayerName("BASIL");
            Assert.AreEqual("BASIL", player1.getPlayerName());
            player1.setPlayerScore("100");
            Assert.AreEqual("100", player1.getPlayerScore());
            player1.setOpponent("LANCE");
            Assert.AreEqual("LANCE", player1.getOpponent());
            player1.setOpponentScore("0");
            Assert.AreEqual("0", player1.getOpponentScore());
            player1.setTime("500");
            Assert.AreEqual("500", player1.getTime());
        }

        /// <summary>
        /// Test getters and setters
        /// </summary>
        [TestMethod]
        public void BoggleClientTest3()
        {
            string[] args = { "30", "..\\..\\..\\Resources\\dictionary.txt" };
            new BoggleServer.BoggleServer(args);
            BoggleClientModel player1 = new BoggleClientModel();
            player1.setBoard("ABCDEFGHIJKLMNOP");
            Assert.AreEqual("ABCDEFGHIJKLMNOP", player1.getBoard());
            player1.setPlayerName("BASIL");
            Assert.AreEqual("BASIL", player1.getPlayerName());
            player1.setPlayerScore("100");
            Assert.AreEqual("100", player1.getPlayerScore());
            player1.setOpponent("LANCE");
            Assert.AreEqual("LANCE", player1.getOpponent());
            player1.setOpponentScore("0");
            Assert.AreEqual("0", player1.getOpponentScore());
            player1.setTime("500");
            Assert.AreEqual("500", player1.getTime());
        }

        [TestMethod]
        public void BoggleClientTest10()
        {
            string[] args = { "30", "..\\..\\..\\Resources\\dictionary.txt" };
            new BoggleServer.BoggleServer(args);
            BoggleClientModel player1 = new BoggleClientModel();
            BoggleClientModel player2 = new BoggleClientModel();
            player1.Connect("localhost");
            player2.Connect("localhost");
            player1.SendPlayerName("Basil");
            player2.SendPlayerName("Lance");

            player1.ScoreLineEvent += testScore;
        }

        private void testScore(string obj)
        {
            throw new NotImplementedException();
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
                BoggleClientModel client = null;

                try
                {
                    string[] args = { "200", "../../../Resources/dictionary.txt" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    client = new BoggleClientModel();
                    client.Connect("localhost");                    

                    mre = new ManualResetEvent(false);

                    // have player one join the boggle game
                    client.SendPlayerName("Basil");

                    // waits for one second, expecting the callback to trigger the mre signal
                    Assert.IsTrue(mre.WaitOne(1000));
                }
                finally
                {
                    client.Disconnect();
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
                BoggleClientModel clientOne = null;
                BoggleClientModel clientTwo = null;

                try
                {
                    string[] args = { "20", "../../../Resources/dictionary.txt", "jimiergsatnesaps" };
                    server = new BoggleServer.BoggleServer(args);

                    // create player one connection
                    clientOne = new BoggleClientModel();                    
                    clientOne.Connect("localhost");

                    // create player two connection
                    clientTwo = new BoggleClientModel();
                    clientTwo.Connect("localhost");
                    
                    mre1 = new ManualResetEvent(false);
                    mre2 = new ManualResetEvent(false);

                    // have player one join the boggle game                    
                    clientOne.SendPlayerName("Basil");
                    // have player two join the boggle game                    
                    clientTwo.SendPlayerName("Lance");                    

                    // waits for one second, expecting the callback to trigger the mre1 signal
                    Assert.IsTrue(mre1.WaitOne(1000));
                    Assert.AreEqual("JIMIERGSATNESAPS", clientOne.getBoard());
                    Assert.AreEqual("BASIL", clientOne.getPlayerName());                    

                    // waits for one second, expecting the callback to trigger the mre2 signal
                    Assert.IsTrue(mre2.WaitOne(1000));
                    Assert.AreEqual("JIMIERGSATNESAPS", clientTwo.getBoard());
                    Assert.AreEqual("LANCE", clientTwo.getPlayerName());

                    //Reset the mres
                    mre1.Reset();
                    mre2.Reset();


                    //Check that the timer is working correctly
                    for (int i = 0; i < 29; i++)
                    {                       
                        mre1.Reset();
                        mre2.Reset();
                    }

                    clientOne.SendWord("to");
                    clientOne.SendWord("sap");
                    clientOne.SendWord("seat");
                    clientOne.SendWord("sat");

                    Thread.Sleep(11000);

                    clientTwo.SendWord("to");
                    clientTwo.SendWord("sap");
                    clientTwo.SendWord("irta");
                    clientTwo.SendWord("rat");

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
                    clientOne.Disconnect();
                    clientTwo.Disconnect();
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

    } // add tests above this
}
