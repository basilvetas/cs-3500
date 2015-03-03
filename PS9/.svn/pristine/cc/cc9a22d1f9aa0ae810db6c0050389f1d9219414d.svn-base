using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for SummaryPage.xaml
    /// </summary>
    public partial class SummaryPage : Page
    {

        BoggleClientModel model;

        /// <summary>
        /// Summary of a boggle game
        /// </summary>
        /// <param name="model"></param>
        /// <param name="summary"></param>
        public SummaryPage(BoggleClientModel model, string summary)
        {
            InitializeComponent();

            this.model = model;

            MakeSummary(summary);
        }

        /// <summary>
        /// Makes the summary
        /// </summary>
        /// <param name="summary"></param>
        private void MakeSummary (string summary)
        {

            string playerLegalWords = "";
            string opponentLegalWords = "";
            string commonWords = "";
            string playerIllegalWords = "";
            string opponentIllegalWords = "";
            int dummyVar;

            int count = 0;

            string[] words = summary.Split(new Char [] {' '});

            foreach(string s in words)
            {
                if (int.TryParse(s, out dummyVar))
                {
                    count++;
                    continue;
                }

                else if (count == 1)
                {
                    playerLegalWords = playerLegalWords + s + "\n";
                }

                else if (count == 2)
                {
                    opponentLegalWords = opponentLegalWords + s + "\n";
                }

                else if (count == 3)
                {
                    commonWords = commonWords + s + "\n";
                }

                else if (count == 4)
                {
                    playerIllegalWords = playerIllegalWords + s + "\n";
                }

                else if (count == 5)
                {
                    opponentIllegalWords = opponentIllegalWords + s + "\n";
                }
            }

            PlayerFinalScore.Text = model.getPlayerScore();
            OpponentFinalScore.Text = model.getOpponentScore();
            PlayerLegal.Text = playerLegalWords;
            OpponentLegal.Text = opponentLegalWords;
            Common.Text = commonWords;
            PlayerIllegal.Text = playerIllegalWords;
            OpponentIllegal.Text = opponentIllegalWords;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() => NavigationService.Navigate(new HomePage(model)));
        }
    }
}
