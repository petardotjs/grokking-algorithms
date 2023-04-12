using System;
using System.Drawing;
using System.Windows.Forms;

namespace SDA_46651r_MyProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonFibonacci_Click(object sender, EventArgs e)
        {
            // Show a modal form with a text box for user input
            Form inputForm = new Form
            {
                MinimumSize = new Size(300, 150),
                MaximumSize = new Size(900, 450),
                // I started using the ClientSize since the Size wasn't quite accurate for me because it includes some additional area like title bar, borders, etc.
                ClientSize = new Size(400, 200),
                Text = "Calculate Fibonacci"
            };
            Label inputLabel = new Label();
            TextBox inputTextBox = new TextBox();
            Button inputButton = new Button();

            inputLabel.Text = "Enter a number:";
            inputLabel.AutoSize = true;
            // because otherwise the standard height will be taken and won't be resized after the autosize is being calculated
            inputLabel.SizeChanged += (s2, e2) =>
            {
                inputLabel.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 - inputLabel.ClientSize.Height - 10;
            };

            inputTextBox.ClientSize = new Size(200, 20);
            inputTextBox.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2;
            inputTextBox.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;

            inputButton.Text = "Calculate";
            inputButton.ClientSize = new Size(150, 20);
            // the position of the textBox + the height of the textBox itself + 20 (so as result I've positioned the button 20px under the textBox)
            inputButton.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 + inputTextBox.ClientSize.Height + 20;

            inputButton.Left = (inputForm.ClientSize.Width - inputButton.ClientSize.Width) / 2;

            // on the same level on the y axis as the textBox
            inputLabel.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;
            // 10px above the textBox
            inputLabel.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 - inputLabel.ClientSize.Height - 10;

            // make it responsive on window resizing
            inputForm.SizeChanged += (s2, e2) =>
            {
                inputTextBox.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2;
                inputTextBox.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;

                inputLabel.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;
                inputLabel.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 - 20;

                inputButton.Left = (inputForm.ClientSize.Width - inputButton.ClientSize.Width) / 2;
                inputButton.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 + inputTextBox.ClientSize.Height + 20;
            };

            inputButton.Click += (s2, e2) =>
            {
                // Get the user's input
                int n = int.Parse(inputTextBox.Text);

                // Calculate the nth Fibonacci number
                int fibonacci = CalculateFibonacci(n);

                // Show the result in a new modal form
                Form resultForm = new Form
                {
                    ClientSize = new Size(400, 200)
                };
                Label resultLabel = new Label
                {
                    // make the control has width based on its content - like fit-content in CSS
                    AutoSize = true,
                    Text = $"The {n}th Fibonacci number is {fibonacci}."
                };
                // when using autosize, the width of the result label would stay static 100 and wouldn't be centered properly unless I've added this snippet
                resultLabel.SizeChanged += (s3, e3) =>
                {
                    resultLabel.Left = (resultForm.ClientSize.Width - resultLabel.ClientSize.Width) / 2;
                };

                Button closeButton = new Button
                {
                    Text = "Close"
                };
                closeButton.Left = (resultForm.Width - closeButton.Width) / 2;
                closeButton.Click += (s3, e3) => { resultForm.Close(); };

                resultLabel.Top = (resultForm.Height - resultLabel.Height - closeButton.Height) * 1/3;
                closeButton.Top = (resultForm.Height - resultLabel.Height - closeButton.Height) * 2/3;

                // I decided to add it there as well, just to make it responsive - on resizing it should stay centered
                resultForm.SizeChanged += (s3, e3) =>
                {
                    resultLabel.Left = (resultForm.Width - resultLabel.Width) / 2;
                    closeButton.Left = (resultForm.Width - closeButton.Width) / 2;
                };

                resultForm.Controls.Add(resultLabel);
                resultForm.Controls.Add(closeButton);
                resultForm.ShowDialog();
            };

            inputForm.Controls.Add(inputLabel);
            inputForm.Controls.Add(inputTextBox);
            inputForm.Controls.Add(inputButton);
            inputForm.ShowDialog();
        }

        private int CalculateFibonacci(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            return CalculateFibonacci(n - 1) + CalculateFibonacci(n - 2);
        }
    }
}
