namespace SDA_46651r_MyProject
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFibonacci = new System.Windows.Forms.Button();
            this.buttonFindGCD = new System.Windows.Forms.Button();
            this.buttonFindLCP = new System.Windows.Forms.Button();
            this.buttonRestoreIPAddress = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(204, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grokking algorithms";
            // 
            // buttonFibonacci
            // 
            this.buttonFibonacci.Location = new System.Drawing.Point(646, 127);
            this.buttonFibonacci.Name = "buttonFibonacci";
            this.buttonFibonacci.Size = new System.Drawing.Size(75, 23);
            this.buttonFibonacci.TabIndex = 1;
            this.buttonFibonacci.Text = "Fibonacci";
            this.buttonFibonacci.UseVisualStyleBackColor = true;
            this.buttonFibonacci.Click += new System.EventHandler(this.ButtonFibonacci_Click);
            // 
            // buttonFindGCD
            // 
            this.buttonFindGCD.Location = new System.Drawing.Point(28, 127);
            this.buttonFindGCD.Name = "buttonFindGCD";
            this.buttonFindGCD.Size = new System.Drawing.Size(75, 23);
            this.buttonFindGCD.TabIndex = 2;
            this.buttonFindGCD.Text = "Find GCD";
            this.buttonFindGCD.UseVisualStyleBackColor = true;
            this.buttonFindGCD.Click += new System.EventHandler(this.ButtonFindGCD_Click);
            // 
            // buttonFindLCP
            // 
            this.buttonFindLCP.Location = new System.Drawing.Point(83, 300);
            this.buttonFindLCP.Name = "buttonFindLCP";
            this.buttonFindLCP.Size = new System.Drawing.Size(130, 23);
            this.buttonFindLCP.TabIndex = 3;
            this.buttonFindLCP.Text = "Longest Common Prefix";
            this.buttonFindLCP.UseVisualStyleBackColor = true;
            this.buttonFindLCP.Click += new System.EventHandler(this.ButtonFindLCP_Click);
            // 
            // buttonRestoreIPAddress
            // 
            this.buttonRestoreIPAddress.Location = new System.Drawing.Point(576, 300);
            this.buttonRestoreIPAddress.Name = "buttonRestoreIPAddress";
            this.buttonRestoreIPAddress.Size = new System.Drawing.Size(84, 23);
            this.buttonRestoreIPAddress.TabIndex = 4;
            this.buttonRestoreIPAddress.Text = "Restore IP";
            this.buttonRestoreIPAddress.UseVisualStyleBackColor = true;
            this.buttonRestoreIPAddress.Click += new System.EventHandler(this.ButtonRestoreIPAddress_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(363, 41);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.buttonRestoreIPAddress);
            this.Controls.Add(this.buttonFindLCP);
            this.Controls.Add(this.buttonFindGCD);
            this.Controls.Add(this.buttonFibonacci);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonFibonacci;
        private System.Windows.Forms.Button buttonFindGCD;
        private System.Windows.Forms.Button buttonFindLCP;
        private System.Windows.Forms.Button buttonRestoreIPAddress;
        private System.Windows.Forms.Button button5;
    }
}

