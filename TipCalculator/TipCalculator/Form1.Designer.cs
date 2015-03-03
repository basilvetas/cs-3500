namespace TipCalculator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.bill_box = new System.Windows.Forms.TextBox();
            this.bill_amount = new System.Windows.Forms.Label();
            this.tip_box = new System.Windows.Forms.TextBox();
            this.Tip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tip_box_percent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(125, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bill_box
            // 
            this.bill_box.Location = new System.Drawing.Point(125, 45);
            this.bill_box.Name = "bill_box";
            this.bill_box.Size = new System.Drawing.Size(100, 20);
            this.bill_box.TabIndex = 1;
            this.bill_box.TextChanged += new System.EventHandler(this.bill_box_TextChanged);
            // 
            // bill_amount
            // 
            this.bill_amount.AutoSize = true;
            this.bill_amount.Location = new System.Drawing.Point(29, 48);
            this.bill_amount.Name = "bill_amount";
            this.bill_amount.Size = new System.Drawing.Size(59, 13);
            this.bill_amount.TabIndex = 2;
            this.bill_amount.Text = "Bill Amount";
            // 
            // tip_box
            // 
            this.tip_box.Location = new System.Drawing.Point(125, 154);
            this.tip_box.Name = "tip_box";
            this.tip_box.Size = new System.Drawing.Size(100, 20);
            this.tip_box.TabIndex = 3;
            // 
            // Tip
            // 
            this.Tip.AutoSize = true;
            this.Tip.Location = new System.Drawing.Point(29, 157);
            this.Tip.Name = "Tip";
            this.Tip.Size = new System.Drawing.Size(49, 13);
            this.Tip.TabIndex = 4;
            this.Tip.Text = "Total Tip";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tip (%)";
            // 
            // tip_box_percent
            // 
            this.tip_box_percent.Location = new System.Drawing.Point(125, 77);
            this.tip_box_percent.Name = "tip_box_percent";
            this.tip_box_percent.Size = new System.Drawing.Size(100, 20);
            this.tip_box_percent.TabIndex = 6;
            this.tip_box_percent.TextChanged += new System.EventHandler(this.tip_box_percent_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 287);
            this.Controls.Add(this.tip_box_percent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Tip);
            this.Controls.Add(this.tip_box);
            this.Controls.Add(this.bill_amount);
            this.Controls.Add(this.bill_box);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox bill_box;
        private System.Windows.Forms.Label bill_amount;
        private System.Windows.Forms.TextBox tip_box;
        private System.Windows.Forms.Label Tip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tip_box_percent;
    }
}

