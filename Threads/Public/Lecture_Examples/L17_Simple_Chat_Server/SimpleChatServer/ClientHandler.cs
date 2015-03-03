using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SimpleChatServer
{

    /// <summary>
    /// Author: Joe Zachary
    ///         H. James de St. Germain (modifier 2013)
    ///         
    /// 
    /// Represents a connection with a remote client.  Takes care of receiving and sending
    /// information to that client according to the protocol.
    /// 
    /// 
    /// </summary>
    class Client_Communicator_Handler
    {
        /// <summary>
        ///  The socket through which we communicate with the remote client
        /// </summary>
        private Socket socket;

        /// <summary>
        /// Text that has been received from the client but not yet dealt with
        /// </summary>
        private String incoming;

        /// <summary>
        /// Text that needs to be sent to the client but has not yet gone
        /// </summary>
         private String outgoing;

        /// <summary>
        /// Encoding used for incoming/outgoing data
        /// </summary>
        private static System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        /// <summary>
        /// a way to give communicators a unique id
        /// </summary>
        private static int id_counter = 0;

        /// <summary>
        /// a unique id for the communicator
        /// </summary>
        private int my_id;

        /// <summary>
        ///  Records whether an asynchronous send attempt is ongoing
        /// </summary>
        private bool sendIsOngoing = false;

        /// <summary>
        ///   For synchronizing sends
        /// </summary>
        private readonly object sendSync = new object();


        /// <summary>
        /// Creates a ClientConnection from the socket, then begins communicating with it.
        /// </summary>
        public Client_Communicator_Handler(Socket s)
        {
            // Record the socket and clear incoming
            socket = s;
            incoming = "";
            outgoing = "";
            this.my_id = id_counter++;

            // Send a welcome message to the remote client
            SendMessage("Welcome!\r\n");

            Console.WriteLine("Local Socket on:" + this.socket.LocalEndPoint);
            Console.WriteLine("Remote Socket on:" + this.socket.RemoteEndPoint);

            // Ask the socket to call MessageReceive as soon as up to 1024 bytes arrive.
            byte[] buffer = new byte[1024];
            socket.BeginReceive(buffer, 0, buffer.Length,
                                SocketFlags.None, MessageReceived, buffer);
        }


        /// <summary>
        /// Called when some data has been received.
        /// </summary>
        private void MessageReceived(IAsyncResult result)
        {
            // Lets see what thread we are
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("I am " + my_id + " Message Recieved : thread is " + managedThreadId);

            // Get the buffer to which the data was written.
            byte[] buffer = (byte[])(result.AsyncState);

            // Figure out how many bytes have come in
            int bytes = socket.EndReceive(result);

            // simulate race condition
            // if (my_id == 0)
            //{
            //    Thread.Sleep(5000);
            //}

            // If no bytes were received, it means the client closed its side of the socket.
            // Report that to the console and close our socket.
            if (bytes == 0)
            {
                Console.WriteLine("Socket closed");
                socket.Close();
            }
            // Otherwise, decode and display the incoming bytes.  Then request more bytes.
            else
            {
                // Convert the bytes into a string
                incoming += encoding.GetString(buffer, 0, bytes);
                Console.WriteLine(incoming);

                // Echo any complete lines, converted to upper case
                int index;
                while ((index = incoming.IndexOf('\n')) >= 0)
                {
                    String line = incoming.Substring(0, index);
                    if (line.EndsWith("\r"))
                    {
                        line = line.Substring(0, index - 1);
                    }
                    SendMessage(line.ToUpper() + "\r\n");
                    incoming = incoming.Substring(index + 1);


                    // JIM: Question (1) suppose I want to allow the other side to disconnect:
                    if (Regex.IsMatch( line, "END") )
                    {
                        socket.Close();  // JIM: what is wrong with this code  - see answer (1) in README
                    }
                }

                // Ask for some more data
                socket.BeginReceive(buffer, 0, buffer.Length,
                    SocketFlags.None, MessageReceived, buffer);
            }
        }


        /// <summary>
        /// Sends a string to the client
        /// </summary>
        private void SendMessage(String message)
        {
            // Lets see what thread we are
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("I am " + my_id + " Send Message : thread is " + managedThreadId);

            // Get exclusive access to send mechanism
            lock (sendSync)
            {
                // Append the message to the unsent string
                outgoing += message;

                // If there's not a send ongoing, start one.
                if (!sendIsOngoing)
                {
                    sendIsOngoing = true;
                    SendBytes();
                }
            }
        }


        /// <summary>
        /// Attempts to send the entire outgoing string.
        /// </summary>
        private void SendBytes()
        {
            if (outgoing == "")
            {
                sendIsOngoing = false;
            }
            else
            {
                byte[] outgoingBuffer = encoding.GetBytes(outgoing);
                outgoing = "";
                socket.BeginSend(outgoingBuffer, 0, outgoingBuffer.Length,
                                 SocketFlags.None, MessageSent, outgoingBuffer);
            }
        }


        /// <summary>
        /// Called when a message has been successfully sent
        /// </summary>
        private void MessageSent(IAsyncResult result)
        {
            // Find out how many bytes were actually sent
            int bytes = socket.EndSend(result);

            // Get exclusive access to send mechanism
            lock (sendSync)
            {
                // Get the bytes that we attempted to send
                byte[] outgoingBuffer = (byte[])result.AsyncState;
                
                // The socket has been closed
                if (bytes == 0)
                {
                    socket.Close();
                    Console.WriteLine("Socket closed");
                }

                // Prepend the unsent bytes and try sending again.
                else
                {
                    outgoing = encoding.GetString(outgoingBuffer, bytes, 
                                                  outgoingBuffer.Length - bytes) + outgoing;
                    SendBytes();
                }
            }

        }
    }
}
