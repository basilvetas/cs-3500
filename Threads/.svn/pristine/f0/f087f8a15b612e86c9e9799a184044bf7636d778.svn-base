using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Factoring
{
    /// <summary>
    /// This form demonstrates a GUI with background workers.
    /// 
    ///  More specifically, it computes either factor counts or prime factorization, depending on
    ///  the setting.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes the window
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            factors_or_prime_factorization_CheckedChanged(null, null);
        }

        /// <summary>
        /// Called when the Count button is clicked
        /// </summary>
        private void button_Click(object sender=null, EventArgs e=null)
        {
            // Start counting ...
            if (start_computation_button.Text != "Cancel")
            {

                // Remove any text from the display area
                user_display_textbox.Text = "";

                // Ask the background worker to start calculating.  
                // Note the parameter
                long number;
                if (Int64.TryParse(input.Text, out number))
                {
                    if (number > 0)
                    {
                        start_computation_button.Text = "Cancel";
                        this.factors_or_prime_factorization.Enabled = false;
                        backgroundWorker1.RunWorkerAsync(number);
                    }
                    else
                    {
                        this.user_display_textbox.Text = "No Negative Numbers Please";
                    }
                }
                else // parsing not successful
                {
                    this.user_display_textbox.Text = "Could not parse number";
                    
                }
            }

            // Cancel (unless already canceled)
            else
            {
                if (!backgroundWorker1.CancellationPending)
                {
                    backgroundWorker1.CancelAsync();
                }
            }

        }

        /// <summary>
        /// Does factor counting
        /// </summary>
        private int countFactors(long number, out string factor_list)
        {
            int progress = 0;
            factor_list = "";

            backgroundWorker1.ReportProgress(0);
            int factors = 0;
            for (long i = 1; i <= number; i++)
            {
                if (number % i == 0)
                {
                    factors++;
                    factor_list += i + ", ";
                }
                if (backgroundWorker1.CancellationPending)
                {
                    return 0;
                }
                int p = (int)(100 * (i / (float)number));
                if (p > progress)
                {
                    backgroundWorker1.ReportProgress(p);
                    progress = p;
                }
            }
            backgroundWorker1.ReportProgress(100);
            return factors;
        }

        /// <summary>
        ///   Determines the prime factorization of a number
        /// </summary>
        /// <param name="number"></param>
        /// <param name="factor_list"></param>
        /// <returns></returns>
        private int prime_factorization(long number, out string factorization)
        {
            int progress = 0;
            factorization = "";

            backgroundWorker1.ReportProgress(0);
            int factors = 0;
            for (int i = 2; i <= number; i++)
            {
                if (number % i == 0)
                {
                    factors++;
                    factorization += i + " * ";
                    number = number / i;
                    i--; // try the same sumer again
                }

                // check if the cancel button has been pressed
                if (backgroundWorker1.CancellationPending)
                {
                    return 0;
                }

                // compute the precentage done
                int p = (int)(100 * (i / (float)number));
                if (number == 1) { p = 100; }
                if (p > progress)
                {
                    backgroundWorker1.ReportProgress(p);
                    progress = p;
                }
            }
            return factors;
        }

        /// <summary>
        /// When we call RunWorkerAsync, it runs this event handler in
        /// a separate thread.
        /// </summary>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Extract the argument from the event
            long number = (long) e.Argument;
            String factor_list = "";

            //jim test invoking on main (GUI) thread

//            This is the old delegate/anonymous function syntax
//            MethodInvoker set_label = new MethodInvoker(delegate()
//            {
//                whats_happening_label.Text = this.in_progress_message;
//            });

            //  This is the new lambda syntax
            MethodInvoker set_label = new MethodInvoker(() => { whats_happening_label.Text = this.in_progress_message; });

            //jim: the following code is not really necessary, as this code is always run in a thread that is not the GUI
            //     and thus we should just call invoke
            if (whats_happening_label.InvokeRequired)
            {
                this.Invoke(set_label);
            }
            else 
            {
                set_label();  // this code should never be used
            }


            // Count the factors
            int count = -1;
            if (factors_or_prime_factorization.Checked)
            {
                count = prime_factorization(number, out factor_list);
            }
            else
            {
                count = countFactors(number, out factor_list);
            }

            // Store the result into the event
            if (backgroundWorker1.CancellationPending)
            {
                // progressBar1.Value = 0;  // cannot set this because we are not the GUI thread... 
                // JIM Note: have to either INVOKE this or move it to the finish function (runworkercompleted)
                e.Cancel = true;
            }
            else
            {
                e.Result = "Total: " + count + "  Values: " + factor_list;
            }
        }

        /// <summary>
        /// This will be run in the GUI event thread when the computation is complete
        /// </summary>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Reset the button
            start_computation_button.Text                = this.button_label_message;
            whats_happening_label.Text = this.done_message;
            factors_or_prime_factorization.Enabled = true;

            // Report what happened
            if (e.Error != null)
            {
                user_display_textbox.Text = "Error";
            }
            else if (e.Cancelled)
            {
                user_display_textbox.Text = "Cancelled";
                progressBar1.Value = 0;
            }
            else
            {
                user_display_textbox.Text = e.Result.ToString();
                progressBar1.Value = 100;
            }






        }

        /// <summary>
        /// Updates the progress bar
        /// </summary>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Change the operation that the GUI is undertaking
        /// </summary>
        /// <param name="sender"> Ignored </param>
        /// <param name="e"> also Ignored</param>
        private void factors_or_prime_factorization_CheckedChanged(object sender, EventArgs e)
        {
            if (factors_or_prime_factorization.Checked)
            {
                this.set_for_prime_factorization();
            }
            else
            {
                this.set_for_factorization();
            }
            this.start_computation_button.Text = button_label_message;
        }

        private void set_for_factorization()
        {
            this.button_label_message = "Count the Factors of the Number";
            this.in_progress_message = "Counting Factors Now";
            this.done_message = "Done Counting Factors";
        }

        private void set_for_prime_factorization()
        {
            this.button_label_message = "Find Prime Factors of the Number";
            this.in_progress_message = "Computing Prime Factorization";
            this.done_message = "Done Computing Prime Factorization";
        }

        public string button_label_message { get; set; }

        public string done_message { get; set; }

        public string in_progress_message { get; set; }

        /// <summary>
        /// Handle the enter key for the textbox (for the number).
        /// 
        /// All this does is simulate the button click
        /// </summary>
        /// <param name="sender"> ignored </param>
        /// <param name="e">      ignored </param>
        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                button_Click();
                e.SuppressKeyPress = true; // stop the annoying bing that Windows makes when you press a key
            }
        }

    }
}
