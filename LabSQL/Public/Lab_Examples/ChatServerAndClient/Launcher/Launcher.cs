using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS;
using System.Threading;

namespace CC
{
    class Program
    {
        /// <summary>
        /// Launches a chat server and two chat clients
        /// </summary>
        static void Main(string[] args)
        {
            new ChatServer(5000);
            new Thread(() => ChatClientView.Main()).Start();
            new Thread(() => ChatClientView.Main()).Start();
        }
    }
}
