using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using System.Linq;

namespace SDA_46651r_MyProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class DialogConfig
        {
            public string InputDialogTitle { get; set; }
            public string InputDialogLabelText { get; set; }
            public string InputDialogButtonText { get; set; }
            public string InfoDialogTitle { get; set; }
            public string InfoDialogDescription { get; set; }
        }

        readonly DialogConfig fibonacciConfig = new DialogConfig
        {
            InputDialogTitle = "Find the n-th fibonacci number",
            InputDialogLabelText = "Enter a number:",
            InputDialogButtonText = "Calculate",
            InfoDialogTitle = "What is the fibonacci sequence?",
            InfoDialogDescription = "The Fibonacci sequence is a series of numbers in which each number is the sum of the two preceding ones."
        };

        readonly DialogConfig euclidConfig = new DialogConfig
        {
            InputDialogTitle = "Find the GCD of 2 numbers",
            InputDialogLabelText = "Enter 2 numbers:",
            InputDialogButtonText = "Find GCD",
            InfoDialogTitle = "GCD and the Euclidean Algorithm",
            InfoDialogDescription = "The greatest common divisor (GCD) of two integers is the largest positive integer that divides both of them without leaving a remainder. The Euclidean Algorithm is a method for finding the GCD of two integers by iteratively finding the remainder when the larger number is divided by the smaller number, and replacing the larger number with the smaller number and the smaller number with the remainder until the remainder is 0."
        };

        private void ButtonFibonacci_Click(object s, EventArgs e)
        {
            Form inputForm = CreateInputDialog(fibonacciConfig);

            inputForm.Controls[2].Click += (s2, e2) =>
            {
                int userInput = -1;

                try
                {
                    // because I check if the input is != 0 outside the try-block, I shouldn't change its value until I'm sure that the value is valid
                    // so I use middle variable for that purpose. Otherwise, an error would be showed and the fibonacci would be calculated for that 
                    // value that is out of range since userInput would not be 0 anymore. I should make it something other than 0 only if the value is safe (valid)
                    int userInputToBeChecked = int.Parse(inputForm.Controls[1].Text);

                    if (userInputToBeChecked > 150 || userInputToBeChecked <= 0)
                    {
                        throw new Exception();
                    }

                    // the input is safe so I can take it
                    userInput = userInputToBeChecked;
                }
                catch (Exception)
                {
                    // Display an error message if the user enters a non-numeric value
                    MessageBox.Show(this, "Wrong input.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (userInput != -1)
                {
                    // Calculate the nth Fibonacci number
                    BigInteger fibonacci = CalculateFibonacci(userInput);

                    ShowResultDialog($"The {userInput}th Fibonacci number is {fibonacci}.");
                };

            };

            inputForm.ShowDialog();
        }

        private void ButtonFindGCD_Click(object s, EventArgs e)
        {
            Form inputForm = CreateInputDialog(euclidConfig);

            inputForm.Controls[2].Click += (s2, e2) =>
            {
                int[] inputNumbers = {};

                try
                {
                    string textBoxValue = inputForm.Controls[1].Text;

                    string[] separators = new string[] { " ", ",", ", " };

                    int[] numbers = textBoxValue.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(int.Parse)
                                         .ToArray();


                    if(numbers.Length == 2)
                    {
                        // the input is safe so I can take it
                        inputNumbers = numbers;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    // Display an error message if the user enters a non-numeric value
                    MessageBox.Show(this, "The input should be exactly 2 numbers, separated by either a white space or a comma.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (inputNumbers.Length != 0)
                {
                    // Calculate the nth Fibonacci number
                    int gcd = EuclideanAlgorithm(inputNumbers[0], inputNumbers[1]);

                    ShowResultDialog($"The GCD of {inputNumbers[0]} and {inputNumbers[1]} is {gcd}.");
                };

            };

            inputForm.ShowDialog();
        }

        private Form CreateInputDialog(DialogConfig dialogConfig)
        {
            // Show a modal form with a text box for user input
            Form inputForm = new Form
            {
                MinimumSize = new Size(300, 150),
                MaximumSize = new Size(900, 450),
                // I started using the ClientSize since the Size wasn't quite accurate for me because it includes some additional area like title bar, borders, etc.
                ClientSize = new Size(400, 200),
                Text = dialogConfig.InputDialogTitle,
                StartPosition = FormStartPosition.CenterParent

            };
            Label inputLabel = new Label();
            TextBox inputTextBox = new TextBox();
            Button inputButton = new Button();

            inputLabel.Text = dialogConfig.InputDialogLabelText;
            inputLabel.AutoSize = true;
            // because otherwise the standard height will be taken and won't be resized after the autosize is being calculated
            inputLabel.SizeChanged += (s2, e2) =>
            {
                inputLabel.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 - inputLabel.ClientSize.Height - 10;
            };

            inputTextBox.ClientSize = new Size(200, 20);
            inputTextBox.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2;
            inputTextBox.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;

            // on pressing enter perform click
            inputTextBox.KeyDown += (s2, e2) =>
            {
                if (e2.KeyCode == Keys.Enter)
                {
                    e2.SuppressKeyPress = true;
                    inputButton.PerformClick();
                }
            };

            inputButton.Text = dialogConfig.InputDialogButtonText;
            inputButton.ClientSize = new Size(150, 20);
            // the position of the textBox + the height of the textBox itself + 20 (so as result I've positioned the button 20px under the textBox)
            inputButton.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 + inputTextBox.ClientSize.Height + 20;

            inputButton.Left = (inputForm.ClientSize.Width - inputButton.ClientSize.Width) / 2;

            // on the same level on the y axis as the textBox
            inputLabel.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;
            // 10px above the textBox
            inputLabel.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 - inputLabel.ClientSize.Height - 10;

            PictureBox inputPictureBox = new PictureBox
            {
                Image = Properties.Resources.icon_info,
                Left = inputForm.ClientSize.Width - 30,
                Top = inputForm.ClientSize.Height - 30
            };

            inputPictureBox.Click += (s2, e2) =>
            {
                ShowInfoDialog(dialogConfig.InfoDialogTitle, dialogConfig.InfoDialogDescription);
            };


            // make it responsive on window resizing
            inputForm.SizeChanged += (s2, e2) =>
            {
                inputTextBox.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2;
                inputTextBox.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;

                inputLabel.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;
                inputLabel.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 - 20;

                inputButton.Left = (inputForm.ClientSize.Width - inputButton.ClientSize.Width) / 2;
                inputButton.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 + inputTextBox.ClientSize.Height + 20;

                inputPictureBox.Left = inputForm.ClientSize.Width - 30;
                inputPictureBox.Top = inputForm.ClientSize.Height - 30;
            };

            inputForm.Controls.Add(inputLabel);
            inputForm.Controls.Add(inputTextBox);
            inputForm.Controls.Add(inputButton);
            inputForm.Controls.Add(inputPictureBox);

            // tried this but the function is not finished until the dialog is not being closed
            //return new InputControls
            //{
            //    TextBox = inputTextBox,
            //    Button = inputButton,
            //};
            // so i had to attach the click event listener before the dialog is being showed and decided to return it

            return inputForm;
        }

        private void ShowInfoDialog(string infoDialogTitle, string infoDialogDescription)
        {
            Form infoForm = new Form
            {
                ClientSize = new Size(400, 300),
                MaximumSize = new Size(400, 300),
                MinimumSize = new Size(400, 300),
                Text = infoDialogTitle,
                StartPosition = FormStartPosition.CenterParent
            };

            Label infoLabel = new Label
            {
                ClientSize = new Size(infoForm.ClientSize.Width - 50, infoForm.ClientSize.Height - 50)
            };
            infoLabel.Left = (infoForm.ClientSize.Width - infoLabel.ClientSize.Width) / 2;
            infoLabel.Top = (infoForm.ClientSize.Height - infoLabel.ClientSize.Height) / 2;
            infoLabel.Text = infoDialogDescription;
            infoLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            infoLabel.TextAlign = ContentAlignment.MiddleCenter;

            infoForm.Controls.Add(infoLabel);
            infoForm.ShowDialog();
        }

        private void ShowResultDialog(string result)
        {
            // Show the result in a new modal form
            Form resultForm = new Form
            {
                ClientSize = new Size(400, 200),
                StartPosition = FormStartPosition.CenterParent

            };
            Label resultLabel = new Label
            {
                // make the control has width based on its content - like fit-content in CSS
                AutoSize = true,
                Text = result,
                Font = new Font("Arial", 12, FontStyle.Regular)
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

            resultLabel.Top = (resultForm.Height - resultLabel.Height - closeButton.Height) * 1 / 3;
            closeButton.Top = (resultForm.Height - resultLabel.Height - closeButton.Height) * 2 / 3;

            // I decided to add it there as well, just to make it responsive - on resizing it should stay centered
            resultForm.SizeChanged += (s3, e3) =>
            {
                resultLabel.Left = (resultForm.Width - resultLabel.Width) / 2;
                closeButton.Left = (resultForm.Width - closeButton.Width) / 2;
            };

            resultForm.Controls.Add(resultLabel);
            resultForm.Controls.Add(closeButton);
            resultForm.ShowDialog();
        }

        //private int CalculateFibonacci(int n)
        //{
        //    if (n == 0)
        //        return 0;
        //    if (n == 1)
        //        return 1;
        //   return CalculateFibonacci(n - 1) + CalculateFibonacci(n - 2); 
        //}

        // the first version of the function looks better because of less code but its complexity is higher - O(n^2) and it struggles to compute the answer after 40
        // because the stack is overloaded
        private BigInteger CalculateFibonacci(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;

            BigInteger a = 0, b = 1, c = 0;
            for (BigInteger i = 2; i <= n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }
            return c;
        }

        private int EuclideanAlgorithm(int num1, int num2)
        {
            int remainder;
            while (num2 != 0)
            {
                remainder = num1 % num2;
                num1 = num2;
                num2 = remainder;
            }
            return num1;
        }
    }
}
