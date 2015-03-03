using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipCalculator
{
    public partial class Form1 : Form
    {

        private bool valid_box1, valid_box2;
        decimal result, tip_amount;
        public Form1()
        {
            InitializeComponent();
            valid_box1 = false;
            valid_box2 = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if((valid_box1 == true) && (valid_box2 == true))
            { 
                tip_amount = Convert.ToDecimal(tip_box_percent.Text)/100m;
                result = Convert.ToDecimal(bill_box.Text);
                decimal total = result * tip_amount;
                tip_box.Text = String.Format("{0:C}", total);
            }            
        }

        private void bill_box_TextChanged(object sender, EventArgs e)
        {
            if(decimal.TryParse(bill_box.Text, out result))
            {
                valid_box1 = true;
            }
            else{
                valid_box1 = false;
            }
        }

        private void tip_box_percent_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(tip_box_percent.Text, out tip_amount))
            {
                valid_box2 = true;
            }
            else
            {
                valid_box2 = false;
            }
        }
    }
}
