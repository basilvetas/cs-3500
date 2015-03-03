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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Internal model
        /// </summary>
        private BoggleClientModel model;

        /// <summary>
        /// Constructor for MainWindow. This constructor simply creates a new
        /// BoggleClientModel object and passes it to the homepage. 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            model = new BoggleClientModel();            

            // Navigate to the home page.
            MainFrame.NavigationService.Navigate(new HomePage(model));
            
        }
    }
}
