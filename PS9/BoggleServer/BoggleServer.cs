using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomNetworking;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using BB;
using System.Timers;
using System.Threading;

namespace BoggleServer
{
    /// <summary>
    /// This program creates the server side of our Boggle game.
    /// </summary>
    /// <author>Basil Vetas, Lance Petersen</author>
    /// <date>November 18, 2014</date>
    public class BoggleServer
    {
        // Listens for incoming connections
        private TcpListener server;

        // One StringSocket per connected client
        private List<StringSocket> allSockets;        

        // the number of seconds that each Boggle game should last (passed in as parameter)
        private int gameLength;

        // the path name of a file that contains all the legal words (passed in as parameter)
        private HashSet<string> legalWords;

        // optional string of exactly 16 letters used to initialize each Boggle board (optional parameter)
        private string initialBoggleBoard;

        // A player that is waiting to be matched with a new player. 
        private Player waitingPlayer;

        // An object to lock on
        private readonly Object lockObject;

        /// <summary>
        /// Will set up the Boggle Server
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // start a new server based on the given arguments and read console line
            new BoggleServer(args);
            Console.ReadLine();
        }

        /// <summary>
        /// Public constructor to check the arguments that are passed in and create a new
        /// BoggleServer based on the given arguments by calling a private constructor. 
        /// </summary>
        /// <param name="args"></param>
        public BoggleServer (string[] args) 
        {          
            // the server can only take two or three arguments
            if ((args.Length != 2) && (args.Length != 3))
            {
                Console.Error.WriteLine("Invalid number of arguments.");
                return;
            }
            
            int time; // holds int for game length                       

            // if the first arg is an int, get it as our timer
            if (int.TryParse(args[0], out time))
            {
                if (!(time > 0))    // if the time is not positive, return with error
                {
                    Console.Error.WriteLine("Invalid time argument. Must be positive.");
                    return;
                }
            }
            else // otherwise return with error
            {
                Console.Error.WriteLine("Invalid time argument. Must be a positive integer.");
                return;
            }

            string pathname = args[1]; // holds path to legal words

            string initBoard; // holds initial board string if provided

            // if there are three parameters, use it
            if (args.Length == 3)
            {
                if (args[2].Length != 16) // if the initial board is not 16 characters
                {
                    // return with error
                    Console.Error.WriteLine("Initial Boggle board setup must have 16 characters.");
                    return;
                }

                if (!(Regex.IsMatch(args[2], @"^[a-zA-Z]+$"))) // if it is not a letter
                {
                    // return with error
                    Console.Error.WriteLine("Initial Boggle board can only be a letter a-z.");
                    return;
                }

                initBoard = args[2];
            }
            else initBoard = null; // otherwise set it to null            

            // initialize BoggleServer private member variables
            this.gameLength = time;            
            this.initialBoggleBoard = initBoard;
            this.waitingPlayer = null;
            this.lockObject = new Object();

            string legalWordsFile = pathname;

            // if the pathname is null or empty, return with error
            if (ReferenceEquals(legalWordsFile, null) || legalWordsFile.Equals(""))
            {
                // return with error
                Console.Error.WriteLine("Legal words dictionary cannot be null or empty.");
                return;
            }
            else this.legalWords = getLegalWords(legalWordsFile); // else read in legal words and store        

            // if legalWords is null after getting the words then there was an error so return
            if (ReferenceEquals(legalWords, null))
                return;

            allSockets = new List<StringSocket>(); // will hold all the string sockets between clients            

            server = new TcpListener(IPAddress.Any, 2000); // create new server for boggle
            server.Start(); // start the server

            // begin accepting connections from players
            server.BeginAcceptSocket(ReceivePlayerCallback, null);

            //Thread t = new Thread(failMe);
            //t.Start();
        }

        /// <summary>
        /// Makes the server not stop (Hack)
        /// </summary>
        //private void failMe()
        //{
        //    bool IShouldChangeThis = true;
        //    while (IShouldChangeThis)
        //    {
        //        Thread.Sleep(10000);
        //    }
        //    Console.Read();
        //}

        /// <summary>
        /// Helper method to get the legal words for our Boggle game
        /// </summary>
        /// <param name="pathname"></param>
        /// <returns></returns>
        private HashSet<string> getLegalWords(string pathname)
        {
            HashSet<string> boggleWords = new HashSet<string>();

            try // try to read in the dictionary file
            {
                using (StreamReader reader = new StreamReader(pathname))
                {
                    string word; // holds a legal word

                    // while there is still another word in the file to read
                    while (!(ReferenceEquals(word = reader.ReadLine(), null)))
                    {
                        boggleWords.Add(word);  // add it to our set of legal words
                    }                   
                }
            }
            catch(Exception e) // catch any exception
            {
                Console.WriteLine("The dictionary file could not be read: ");
                Console.WriteLine(e.Message);
                return null;
            }

            return boggleWords; // return list of legal words
        }

        /// <summary>
        /// Deals with connection requests when a new player joins the Boggle game
        /// </summary>
        /// <param name="ar">Resulting status from asynchronous operation</param>
        /// <citation>Chat Server from Lab Examples</citation>
        private void ReceivePlayerCallback(IAsyncResult ar)
        {
            // create a new socket using the received player connection
            try
            {
                Socket socket = server.EndAcceptSocket(ar);
            
                // wrap the socket in a string socket for the new player connection
                StringSocket playerConnection = new StringSocket(socket, UTF8Encoding.Default);
                
                // add it to our list of all the string sockets
                allSockets.Add(playerConnection);

                // let the string socket begin accepting input to read the player name
                playerConnection.BeginReceive(ReceiveNameCallback, playerConnection);

                // let the server accept additional connections from new players
                server.BeginAcceptSocket(ReceivePlayerCallback, null);
            }
            catch { }
        }

        /// <summary>
        /// Receives the first line of text from the client, which contains the name of the new
        /// boggle player.  Uses it to compose and send back a welcome message.
        /// 
        /// Expects a "PLAY @" message after a new user has connected
        /// 
        /// Invariant: the object parameter will always be a String Socket
        /// </summary>
        /// <param name="name">The string input from player</param>
        /// <param name="exception">A possible exception</param>
        /// <param name="payload">A String Socket</param>
        /// <citation>Chat Server from Lab Examples</citation>
        private void ReceiveNameCallback(String name, Exception exception, object payload)
        {
            StringSocket ss = (StringSocket) payload;  // invariant safe to cast as string socket

            // If the name is null or empty, it is invalid
            if (ReferenceEquals(null, name) || name.Equals(""))
            {
                ss.BeginSend("IGNORING\n", (e, o) => { }, ss);
                return;
            }

            // If the reference is non-null, then there was a bad connection.
            if (!ReferenceEquals(null, exception))
            {
                ss.BeginSend("IGNORING " + name + "\n", (e, o) => { }, ss);
                return;
            }

            // make the player name uppercase
            name = name.ToUpper();

            // If name doesn't start with PLAY, return
            if (!(name.StartsWith("PLAY ")))                
            {
                ss.BeginSend("IGNORING " + name + "\n", (e, o) => { }, ss);
                return;
            }
            
            string playerName = name.Substring(5); // use this to store the actual name of the player

            // Create new player using playerName
            Player player = new Player(playerName, ss);

            lock (lockObject)
            {
                if (ReferenceEquals(null, waitingPlayer))
                    waitingPlayer = player;
                else
                {
                    Game game = new Game(waitingPlayer, player, gameLength, initialBoggleBoard, legalWords);
                    game.Start();
                    waitingPlayer = null;
                }
            }
        }

        /// <summary>
        /// Stop the Boggle server from accepting new player connections
        /// </summary>
        public void Stop()
        {
            // socket.close on all sockets
            foreach (StringSocket s in allSockets)
                s.Close();
                
            // stop the server from accepting connections
            server.Stop();
        }
    }

    /// <summary>
    /// A new Boggle player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public StringSocket ss {get; private set;}

        /// <summary>
        /// 
        /// </summary>
        public int score {get; set;}

        /// <summary>
        /// 
        /// </summary>
        public HashSet<string> legalWordsFound { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public HashSet<string> illegalWordsFound { get; private set; }

        /// <summary>
        /// Constructor for a new player
        /// </summary>
        public Player(string playerName, StringSocket connection)
        {
            // initialize variables
            name = playerName;
            ss = connection;
            score = 0;
            legalWordsFound = new HashSet<string>();
            illegalWordsFound = new HashSet<string>();
        }
    }

    /// <summary>
    /// A new Boggle game
    /// </summary>
    public class Game
    {
        // private memeber variables
        private Player playerOne;
        private Player playerTwo;
        private int timeRemaining;
        private System.Timers.Timer timer; 
        private BoggleBoard board;
        private HashSet<string> dictionary;
        private HashSet<string> commonWords;

        /// <summary>
        /// Constructor for a new game of boggle
        /// </summary>
        /// <param name="playerOne"></param>
        /// <param name="playerTwo"></param>
        /// <param name="gameLength"></param>
        /// <param name="initialBoardSetup"></param>
        /// <param name="legalWordsDictionary"></param>
        public Game(Player playerOne, Player playerTwo, int gameLength, string initialBoardSetup, HashSet<string> legalWordsDictionary)
        {
            //Initialize the game.
            if (ReferenceEquals(initialBoardSetup, null))
                board = new BoggleBoard();
            else
                board = new BoggleBoard(initialBoardSetup);

            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
            this.timeRemaining = gameLength;
            this.timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            this.dictionary = legalWordsDictionary;
            this.commonWords = new HashSet<string>();
        }

        /// <summary>
        /// Every time 1 second has elapses, decrement the timeRemaining and 
        /// send the time to both clients.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            lock (timer)
            {
                //decrement time
                timeRemaining--;

                //Send the time remaining to each client
                string message = "TIME " + timeRemaining + "\n";
                playerOne.ss.BeginSend(message, (ex, o) => { }, null);
                playerTwo.ss.BeginSend(message, (ex, o) => { }, null);
                
                //When time has run out
                if (timeRemaining < 1)
                {
                    //Stop timer
                    timer.Stop();

                    //Send final scores
                    playerOne.ss.BeginSend("SCORE " + playerOne.score + " " + playerTwo.score + "\n", (o, ex) => { }, null);
                    playerTwo.ss.BeginSend("SCORE " + playerTwo.score + " " + playerOne.score + "\n", (o, ex) => { }, null); 

                    //Send the summaries of the game.
                    playerOne.ss.BeginSend(MakeGameSummary(playerOne, playerTwo), (o, ex) => { }, null);
                    playerTwo.ss.BeginSend(MakeGameSummary(playerTwo, playerOne), (o, ex) => { }, null);

                    //Close the sockets
                    playerOne.ss.Close();
                    playerTwo.ss.Close();
                }
            }
        }

        /// <summary>
        /// Starts the game of Boggle
        /// </summary>
        public void Start()
        {
            //Send the START command to both players. 
            playerOne.ss.BeginSend("START " + board.ToString() + " " + timeRemaining + " " + playerTwo.name + "\n", (e, o) => { }, playerOne.ss);
            playerTwo.ss.BeginSend("START " + board.ToString() + " " + timeRemaining + " " + playerOne.name + "\n", (e, o) => { }, playerTwo.ss);
            playGame();
        }

        /// <summary>
        /// The actual gameplay for a Boggle game
        /// </summary>
        private void playGame ()
        {
            //Start the timer.
            timer.Start();

            // receive words from the the players as they find them on the board
            playerOne.ss.BeginReceive(receiveWordsCallback, playerOne);
            playerTwo.ss.BeginReceive(receiveWordsCallback, playerTwo);                
        }

        /// <summary>
        /// Callback for when a player finds a word (or thinks they have found a word)
        /// 
        /// Invariant: the third parameter should always be a Player object
        /// </summary>
        /// <param name="word">The word found</param>
        /// <param name="exception">Possible exception</param>
        /// <param name="payload">The player who found the word</param>
        private void receiveWordsCallback(string word, Exception exception, object payload)
        {
            lock (board)
            {
                // player one and two, depending on who found the word
                Player playerFoundWord = payload as Player;
                Player opponent;

                // if the player who found the word is playerOne, then the opponent must be playerTwo
                if (ReferenceEquals(playerFoundWord, playerOne))
                    opponent = playerTwo;
                else opponent = playerOne;  // otherwise the opponent is playerOne

                // If both the word and the exception are null, then the socket has been closed.
                // Send "TERMINATED" to the remaining socket and then close that socket. 
                if (ReferenceEquals(null, word))
                {
                    opponent.ss.BeginSend("TERMINATED\n", (e, o) => { }, null);
                    opponent.ss.Close();
                    return;
                }

                // helper method to update the player scores
                updateScore(word, playerFoundWord, opponent);

                //send updated scores to players
                playerFoundWord.ss.BeginSend("SCORE " + playerFoundWord.score + " " + opponent.score + "\n", (o, e) => { }, null);
                opponent.ss.BeginSend("SCORE " + opponent.score + " " + playerFoundWord.score + "\n", (o, e) => { }, null);

                // receive more words from the player 
                playerFoundWord.ss.BeginReceive(receiveWordsCallback, playerFoundWord);
            }
        }

        /// <summary>
        /// Helper method to update the score when a player finds a word
        /// 
        /// Invariant: The second paramter should always be the player who found the word
        /// and the third parameter should always be the opponent 
        /// </summary>
        /// <param name="word">The word found</param>
        /// <param name="playerFoundWord">Player who found it</param>
        /// <param name="opponent">Opponent player</param>
        private void updateScore(string word, Player playerFoundWord, Player opponent)
        {                       
            // make the message all uppercase
            word = word.ToUpper();

            // If name doesn't start with WORD
            if (!(word.StartsWith("WORD ")))
            {
                // then ignore the command and return
                playerFoundWord.ss.BeginSend("IGNORING\n", (e, o) => { }, playerOne.ss);
                return;
            }

            word = word.Substring(5);

            // if word is less than three characters, don't count it
            if (word.Length < 3)
                return;

            // if the word is not valid
            if (!(dictionary.Contains(word)) || !board.CanBeFormed(word))
            {
                // remove a point from the play who found the work
                playerFoundWord.score--;
                playerFoundWord.illegalWordsFound.Add(word);
            }
            else // otherwise if the word is valid
            {
                if (opponent.legalWordsFound.Contains(word)) // then if opponent already found the word
                {
                    // then remove the word from opponents's list                        
                    opponent.legalWordsFound.Remove(word);

                    // and add it to the list of common words
                    commonWords.Add(word);

                    // and reduce opponent's score by the value of word
                    opponent.score = opponent.score - getValue(word);
                }
                else // if the opponent hasn't found the word
                {
                    // then if the finder hasn't found the word yet either
                    if (!(playerFoundWord.legalWordsFound.Contains(word)))
                    {
                        // then add the word to player one's list                        
                        playerFoundWord.legalWordsFound.Add(word);

                        // and increase player one's score by the value of word
                        playerFoundWord.score = playerFoundWord.score + getValue(word);
                    }
                }
            }
            
        }

        /// <summary>
        /// Helper method to get the point value of a found word.
        /// 
        /// Invariant: word will never be less than 3 characters
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int getValue(string word)
        {
            int pointValue = 0; // holds the value of word

            // if word has 3 or 4 characters
            if ((word.Length == 3) || word.Length == 4)
                pointValue = 1; // worth one point
            else if (word.Length == 5) // if 5 characters
                pointValue = 2; // worth 2 point
            else if (word.Length == 6) // if 6 characters 
                pointValue = 3; // worth 3 points
            else if (word.Length == 7) // if 7 characters
                pointValue = 5; // worth 5 points
            else // if more than 7 charcters
                pointValue = 11; // worth 11 points

            return pointValue;
        }

        /// <summary>
        /// Generate a summary of the game for the player specified
        /// </summary>
        /// <param name="player"></param>
        /// <param name="opponent"></param>
        /// <returns></returns>
        private string MakeGameSummary(Player player, Player opponent)
        {
            string summary = "STOP ";

            //how many words this player found
            summary += player.legalWordsFound.Count + " ";

            foreach (string s in player.legalWordsFound)
            {
                //all the unique, legal words
                summary += s + " ";
            }

            //how many words opponent found
            summary += opponent.legalWordsFound.Count + " ";

            //all of the opponent's words
            foreach (string s in opponent.legalWordsFound)
            {
                summary += s + " ";
            }

            //how many legal words in common
            summary += commonWords.Count + " ";

            //list
            foreach (string s in commonWords)
            {
                summary += s + " "; 
            }

            //how many illegal words
            summary += player.illegalWordsFound.Count + " ";

            //list
            foreach (string s in player.illegalWordsFound)
            {
                summary += s + " ";
            }

            //how many opponent illegal words
            summary += opponent.illegalWordsFound.Count + " ";

            //list
            foreach (string s in opponent.illegalWordsFound)
            {
                summary += s + " ";
            }

            summary += "\n";
            return summary;
        }
    }
}
