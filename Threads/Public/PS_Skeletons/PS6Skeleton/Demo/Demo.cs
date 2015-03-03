using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS
{
    /// <summary>
    /// Example of using a SpreadsheetPanel object
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// Constructor for the demo
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // This an example of registering a method so that it is notified when
            // an event happens.  The SelectionChanged event is declared with a
            // delegate that specifies that all methods that register with it must
            // take a SpreadsheetPanel as its parameter and return nothing.  So we
            // register the displaySelection method below.

            // This could also be done graphically in the designer, as has been
            // demonstrated in class.
            spreadsheetPanel1.SelectionChanged += displaySelection;
            spreadsheetPanel1.SetSelection(2, 3);
        }

        // Every time the selection changes, this method is called with the
        // Spreadsheet as its parameter.  We display the current time in the cell.

        private void displaySelection(SpreadsheetPanel ss)
        {
            int row, col;
            String value;
            ss.GetSelection(out col, out row);
            ss.GetValue(col, row, out value);
            if (value == "") 
            {
                ss.SetValue(col, row, DateTime.Now.ToLocalTime().ToString("T"));
                ss.GetValue(col, row, out value);
                MessageBox.Show("Selection: column " + col + " row " + row + " value " + value);
            }
        }

        // Deals with the New menu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tell the application context to run the form on the same
            // thread as the other forms.
            DemoApplicationContext.getAppContext().RunForm(new Form1());
        }

        // Deals with the Close menu
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
