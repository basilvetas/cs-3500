

using SimpleChatServer;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat
{
    /// <summary>
    /// Author: Joe Zachary
    ///         H. James de St. Germain (modifier 2013)
    ///         
    /// 
    /// This class contains code to show off a TCP client server system
    /// 
    /// 
    /// </summary>
    public class SimpleChatServer
    {
        /// <summary>
        /// Launches a SimpleChatServer on port 4000.  Keeps the main
        /// thread active to we can see output to the console.
        /// </summary>
        static void Main(string[] args)
        {
            new SimpleChatServer(4000);
            Console.ReadLine();
        }


        // Listens for incoming connection requests
        private TcpListener server;


        /// <summary>
        /// Creates a SimpleChatServer that listens for connection requests on port 4000.
        /// </summary>
        public SimpleChatServer(int port)
        {
            // A TcpListener listens for incoming connection requests
            server = new TcpListener(IPAddress.Any, port);

            // Start the TcpListener
            server.Start();

            // Ask the server to call ConnectionRequested at some point in the future when 
            // a connection request arrives.  It could be a very long time until this happens.
            // The waiting and the calling will happen on another thread.  BeginAcceptSocket 
            // returns immediately, and the constructor returns to Main.
            server.BeginAcceptSocket(ConnectionRequested, null);

            // Lets see what thread we are
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Constructor: thread is " + managedThreadId);

        }


        /// <summary>
        /// This is the callback method that is passed to BeginAcceptSocket.  It is called
        /// when a connection request has arrived at the server.
        /// </summary>
        public void ConnectionRequested(IAsyncResult result)
        {
            // We obtain the socket corresonding to the connection request.  Notice that we
            // are passing back the IAsyncResult object.
            Socket s = server.EndAcceptSocket(result);

            // We ask the server to listen for another connection request.  As before, this
            // will happen on another thread.
            server.BeginAcceptSocket(ConnectionRequested, null);

            // We create a new ClientConnection, which will take care of communicating with
            // the remote client.
            new Client_Communicator_Handler(s);

            // Lets see what thread we are
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Connection Requested: thread is " + managedThreadId);

        }
    }



}

