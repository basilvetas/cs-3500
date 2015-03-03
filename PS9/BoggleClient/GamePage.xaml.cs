using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BC
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        /// <summary>
        /// Model to allow this page to interact with data (passed in as a parameter)
        /// </summary>
        private BoggleClientModel model;        

        /// <summary>
        /// This is the page where most of the game will happen.
        /// </summary>
        /// <param name="model"></param>
        public GamePage(BoggleClientModel model)
        {
            InitializeComponent();
            this.model = model;

            // test code - remove later
            //model.setBoard("ABCDEFGHIJKLMNOP");
            //model.setTime("322");
            //model.setOpponent("Lance");
            //model.setPlayerName("Basil");            
            // remove above this

            // initialize scores to zero
            model.setPlayerScore("0");
            model.setOpponentScore("0");

            // set initial game information in view
            Timer.Text = getRemainingTime(model.getTime());
            Opponent.Text = model.getOpponent();
            Player.Text = model.getPlayerName();

            // initialize the boggle board
            InitializeBoard(model.getBoard().ToCharArray());

            model.TimeLineEvent += TimeReceived;
            model.ScoreLineEvent += ScoreReceived;
            model.StopLineEvent += StopReceived;
            model.TerminatedLineEvent += TerminatedReceived;
            model.IgnoreLineEvent += IgnoreReceived;
        }

        /// <summary>
        /// Helper Method to initialize Boggle Board
        /// </summary>
        /// <param name="board"></param>
        private void InitializeBoard(Char[] board)
        {
            // initialize character values
            Cell1.Text = board[0].ToString();
            Cell2.Text = board[1].ToString();
            Cell3.Text = board[2].ToString();
            Cell4.Text = board[3].ToString();
            Cell5.Text = board[4].ToString();
            Cell6.Text = board[5].ToString();
            Cell7.Text = board[6].ToString();
            Cell8.Text = board[7].ToString();
            Cell9.Text = board[8].ToString();
            Cell10.Text = board[9].ToString();
            Cell11.Text = board[10].ToString();
            Cell12.Text = board[11].ToString();
            Cell13.Text = board[12].ToString();
            Cell14.Text = board[13].ToString();
            Cell15.Text = board[14].ToString();
            Cell16.Text = board[15].ToString();

            // make cells read only
            Cell1.IsReadOnly = true;
            Cell2.IsReadOnly = true;
            Cell3.IsReadOnly = true;
            Cell4.IsReadOnly = true;
            Cell5.IsReadOnly = true;
            Cell6.IsReadOnly = true;
            Cell7.IsReadOnly = true;
            Cell8.IsReadOnly = true;
            Cell9.IsReadOnly = true;
            Cell10.IsReadOnly = true;
            Cell11.IsReadOnly = true;
            Cell12.IsReadOnly = true;
            Cell13.IsReadOnly = true;
            Cell14.IsReadOnly = true;
            Cell15.IsReadOnly = true;
            Cell16.IsReadOnly = true;
        }

        /// <summary>
        /// Handle when the server sends information about the timer.
        /// </summary>
        /// <param name="line"></param>
        private void TimeReceived(String line)
        {
            // converts the line to proper for then sets the textbox
            this.Dispatcher.Invoke(() =>Timer.Text = getRemainingTime(line));
        }

        /// <summary>
        /// Handle when the server sends information about the score.
        /// </summary>
        /// <param name="line"></param>
        private void ScoreReceived(String line)
        {            
            // split text into the two player scores separated by space
            string[] scores = Regex.Split(line, @"[\s]+");

            // update scores in model
            model.setPlayerScore(scores[0]);
            model.setOpponentScore(scores[1]);

            // update scores in view
            this.Dispatcher.Invoke(() => PlayerScoreCount.Text = model.getPlayerScore());
            this.Dispatcher.Invoke(() => OpponentScoreCount.Text = model.getOpponentScore());

        }

        /// <summary>
        /// If a STOP message is received, it means the game is over.
        /// </summary>
        /// <param name="line"></param>
        private void StopReceived(String line)
        {            
            this.Dispatcher.Invoke(() => NavigationService.Navigate(new SummaryPage(model, line)));            
        }

        /// <summary>
        /// Event for when we receive a TERMINATED message from the server,
        /// meaning a client has disconnected or become inaccessible. If this
        /// happens, the game ends so just disconnect the remaining client.
        /// </summary>
        /// <param name="line"></param>
        private void TerminatedReceived(String line)
        {            
            model.Disconnect();
            //MessageBox.Show("The server has teminated your game. Please try again.", "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            this.Dispatcher.Invoke(() => NavigationService.Navigate(new HomePage(model)));
        }

        /// <summary>
        /// If we receive an IGNORING message from the server, it means the 
        /// client has deviated from the protocol.
        /// </summary>
        /// <param name="line"></param>
        private void IgnoreReceived(String line)
        {
            // Do NOTHING !!!!!!!!!!!!!!
        }   
     
        /// <summary>
        /// Helper method to convert the time as a string in seconds to 
        /// the proper form in minutes:seconds. 
        /// </summary>
        /// <param name="gameLength"></param>
        /// <returns></returns>
        private string getRemainingTime(string gameLength)
        {
            string timeString = "";
            int timeInt;

            // try to parse the sting to an int, should always work
            int.TryParse(gameLength, out timeInt);

            int minutes = timeInt / 60;
            int seconds = timeInt % 60;
            string secondString;
            if (seconds < 10)
                secondString = "0" + seconds;
            else secondString = "" + seconds;

            // convert it to a string
            timeString = "" +  minutes + ":" + secondString;

            return timeString;
        }

        /// <summary>
        /// Triggered when a new word is entered 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WordsEntered_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                model.SendWord(WordsEntered.Text.Trim());
                WordsEntered.Text = "";
            }
        }
    }
}
