using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace SDA_46651r_MyProject
{
    public partial class formGrokkingAlgos : Form
    {
        public formGrokkingAlgos()
        {
            InitializeComponent();
        }

        private readonly string[] separators = new string[] { " ", ",", ", " };

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

        readonly DialogConfig lcpConfig = new DialogConfig
        {
            InputDialogTitle = "Find the longest common prefix",
            InputDialogLabelText = "Enter 2 or more strings:",
            InputDialogButtonText = "Find LCP",
            InfoDialogTitle = "What is LCP?",
            InfoDialogDescription = "LCP stands for \"longest common prefix\". It is a problem in computer science that involves finding the longest string that is a prefix of a set of strings.\r\n\r\nFor example, consider the set of strings [\"flower\", \"flow\", \"flight\"]. The longest common prefix of these strings is \"fl\"."
        };

        readonly DialogConfig restoreIPConfig = new DialogConfig
        {
            InputDialogTitle = "Restore IP Addresses",
            InputDialogLabelText = "Enter a string of digits:",
            InputDialogButtonText = "Restore IPs",
            InfoDialogTitle = "How does the 'Restore IPs algorithm' work?",
            InfoDialogDescription = "Given a string of digits, this problem involves restoring all possible valid IP addresses that can be formed by inserting dots into the string. Each integer is between 0 and 255 (inclusive) and cannot have leading zeros. For example, \"25525511135\" can be restored as [\"255.255.11.135\", \"255.255.111.35\"]."
        };

        readonly DialogConfig maxTwinSumConfig = new DialogConfig
        {
            InputDialogTitle = "Maximum Twin Sum of a Linked List",
            InputDialogLabelText = "Enter 2 or more integers:",
            InputDialogButtonText = "Calculate Maximum Twin Sum",
            InfoDialogTitle = "What is Maximum Twin Sum?",
            InfoDialogDescription = "In a linked list with even non-zero length (n), the numbers are grouped as follows - the 1st one with the last one (n-th), the 2nd with the n-1, etc. Maximum Twin Sum is the highest sum of integers among the pairs."
        };

        private void ButtonFibonacci_Click(object s, EventArgs e)
        {
            Form inputForm = CreateInputDialog(fibonacciConfig);

            inputForm.Controls[2].Click += (s2, e2) =>
            {
                int userInput = -1;

                try
                {
                    int userInputToBeChecked = int.Parse(inputForm.Controls[1].Text);

                    if (userInputToBeChecked > 150 || userInputToBeChecked <= 0)
                    {
                        throw new Exception();
                    }

                    userInput = userInputToBeChecked;
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Wrong input.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (userInput != -1)
                {
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
                int[] inputNumbers = { };

                try
                {
                    string textBoxValue = inputForm.Controls[1].Text;

                    int[] numbers = textBoxValue.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                    if (numbers.Length == 2)
                    {
                        inputNumbers = numbers;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "The input should be exactly 2 numbers, separated by either a white space or a comma.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (inputNumbers.Length != 0)
                {
                    int gcd = EuclideanAlgorithm(inputNumbers[0], inputNumbers[1]);

                    ShowResultDialog($"The GCD of {inputNumbers[0]} and {inputNumbers[1]} is {gcd}.");
                };

            };

            inputForm.ShowDialog();
        }

        private void ButtonFindLCP_Click(object sender, EventArgs e)
        {
            Form inputForm = CreateInputDialog(lcpConfig);

            inputForm.Controls[2].Click += (s2, e2) =>
            {
                string[] inputStrings = { };

                try
                {
                    string textBoxValue = inputForm.Controls[1].Text;

                    string[] strings = textBoxValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);


                    if (strings.Length >= 2)
                    {
                        inputStrings = strings;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "The input should be at least 2 strings, separated by either a white space or a comma.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (inputStrings.Length != 0)
                {
                    string lcp = LongestCommonPrefix(inputStrings);

                    ShowResultDialog(lcp != "" ? $"The LCP of the given input is {lcp}." : "The given strings have no common prefix.");
                };
            };

            inputForm.ShowDialog();
        }

        private void ButtonRestoreIPAddress_Click(object sender, EventArgs e)
        {
            Form inputForm = CreateInputDialog(restoreIPConfig);

            inputForm.Controls[2].Click += (s2, e2) =>
            {
                string inputString = "";

                try
                {
                    inputString = inputForm.Controls[1].Text;

                    if (IsIPAddressInputValid(inputString))
                    {
                        List<string> validIPs = RestoreIPAddress(inputString);

                        if (validIPs.Count > 0)
                        {
                            ShowResultDialog($"The valid IP addresses that can be formed from\n the input are:\n{string.Join("\n", validIPs)}");
                        }
                        else
                        {
                            ShowResultDialog("No valid IP addresses can be formed from the input.");
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "The input should be a string of digits representing an IPv4 address.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            inputForm.ShowDialog();
        }

        private void ButtonMaxTwinSum_Click(object sender, EventArgs e)
        {
            Form inputForm = CreateInputDialog(maxTwinSumConfig);

            inputForm.Controls[2].Click += (s2, e2) =>
            {
                string inputString = "";

                try
                {
                    inputString = inputForm.Controls[1].Text;
                    string[] numStrings = inputString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    int[] nums = numStrings.Select(int.Parse).ToArray();

                    if (nums.Length % 2 != 0)
                    {
                        throw new Exception();
                    }

                    ListNode head = null;
                    ListNode tail = null;

                    foreach (int num in nums)
                    {
                        ListNode newNode = new ListNode(num);

                        if (head == null)
                        {
                            head = newNode;
                            tail = newNode;
                        }
                        else
                        {
                            tail.next = newNode;
                            tail = newNode;
                        }
                    }

                    int maxTwinSum = FindMaxTwinSum(head);

                    ShowResultDialog($"The maximum twin sum of the input array is {maxTwinSum}.");
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "The input should be an even length list of integers, separated by either a white space or a comma.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            inputForm.ShowDialog();
        }

        private Form CreateInputDialog(DialogConfig dialogConfig)
        {
            Form inputForm = new Form
            {
                MinimumSize = new Size(300, 150),
                MaximumSize = new Size(900, 450),
                ClientSize = new Size(400, 200),
                Text = dialogConfig.InputDialogTitle,
                StartPosition = FormStartPosition.CenterParent

            };
            Label inputLabel = new Label();
            TextBox inputTextBox = new TextBox();
            Button inputButton = new Button();

            inputLabel.Text = dialogConfig.InputDialogLabelText;
            inputLabel.AutoSize = true;
            inputLabel.SizeChanged += (s2, e2) =>
            {
                inputLabel.Top = (inputForm.ClientSize.Height - inputTextBox.Height) / 2 - inputLabel.Height - 10;
            };

            inputTextBox.Size = new Size(200, 20);
            inputTextBox.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2;
            inputTextBox.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;

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
            inputButton.Top = (inputForm.ClientSize.Height - inputTextBox.ClientSize.Height) / 2 + inputTextBox.ClientSize.Height + 20;
            inputButton.Left = (inputForm.ClientSize.Width - inputButton.ClientSize.Width) / 2;

            inputLabel.Left = (inputForm.ClientSize.Width - inputTextBox.ClientSize.Width) / 2;
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
                ClientSize = new Size(infoForm.ClientSize.Width - 50, infoForm.ClientSize.Height - 50),

                Text = infoDialogDescription,
                Font = new Font("Arial", 12, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            infoLabel.Left = (infoForm.ClientSize.Width - infoLabel.Width) / 2;
            infoLabel.Top = (infoForm.ClientSize.Height - infoLabel.Height) / 2;

            infoForm.Controls.Add(infoLabel);
            infoForm.ShowDialog();
        }

        private void ShowResultDialog(string result)
        {
            Form resultForm = new Form
            {
                ClientSize = new Size(400, 200),
                StartPosition = FormStartPosition.CenterParent

            };

            Label resultLabel = new Label
            {
                AutoSize = true,
                Text = result,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

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

            resultLabel.Top = (resultForm.ClientSize.Height) * 1 / 3 - resultLabel.Height * 1 / 2;
            closeButton.Top = (resultForm.ClientSize.Height) * 2 / 3 - closeButton.Height * 1 / 2;

            resultForm.SizeChanged += (s3, e3) =>
            {
                resultLabel.Left = (resultForm.Width - resultLabel.Width) / 2;
                closeButton.Left = (resultForm.Width - closeButton.Width) / 2;
            };

            resultLabel.SizeChanged += (s2, e2) =>
            {
                if (resultLabel.Height > 100)
                {
                    resultLabel.Top = 10;
                    closeButton.Top = resultForm.ClientSize.Height - closeButton.Height - 10;
                }
                else
                {
                    resultLabel.Top = (resultForm.ClientSize.Height) * 1 / 3 - resultLabel.Height * 1 / 2;
                    closeButton.Top = (resultForm.ClientSize.Height) * 2 / 3 - closeButton.Height * 1 / 2;
                }
            };

            resultForm.Controls.Add(resultLabel);
            resultForm.Controls.Add(closeButton);
            resultForm.ShowDialog();
        }

        private BigInteger CalculateFibonacci(int nthFibonacci)
        {
            if (nthFibonacci == 0)
                return 0;
            if (nthFibonacci == 1)
                return 1;

            BigInteger penultimateNumber = 0, lastNumber = 1, currentNumber = 0;

            for (BigInteger i = 2; i <= nthFibonacci; i++)
            {
                currentNumber = penultimateNumber + lastNumber;
                penultimateNumber = lastNumber;
                lastNumber = currentNumber;
            }

            return currentNumber;
        }

        private int EuclideanAlgorithm(int number1, int number2)
        {
            int remainder;

            while (number2 != 0)
            {
                remainder = number1 % number2;
                number1 = number2;
                number2 = remainder;
            }

            return number1;
        }

        public string LongestCommonPrefix(string[] strings)
        {
            string prefix = strings[0];

            for (int i = 1; i < strings.Length; i++)
            {
                while (strings[i].IndexOf(prefix) != 0)
                {
                    prefix = prefix.Substring(0, prefix.Length - 1);
                    if (prefix == "")
                    {
                        return "";
                    }
                }
            }

            return prefix;
        }

        public List<string> RestoreIPAddress(string input)
        {
            List<string> result = new List<string>();
            int inputLength = input.Length;

            for (int secondSegmentStart = 1; secondSegmentStart <= 3; secondSegmentStart++)
            {
                for (int thirdSegmentStart = secondSegmentStart + 1; thirdSegmentStart <= secondSegmentStart + 3; thirdSegmentStart++)
                {
                    for (int fourthSegmentStart = thirdSegmentStart + 1; fourthSegmentStart <= thirdSegmentStart + 3 && fourthSegmentStart < inputLength; fourthSegmentStart++)
                    {
                        string segment1 = input.Substring(0, secondSegmentStart);
                        string segment2 = input.Substring(secondSegmentStart, thirdSegmentStart - secondSegmentStart);
                        string segment3 = input.Substring(thirdSegmentStart, fourthSegmentStart - thirdSegmentStart);
                        string segment4 = input.Substring(fourthSegmentStart);
                        if (IsIPAddressSegmentValid(segment1) && IsIPAddressSegmentValid(segment2) && IsIPAddressSegmentValid(segment3) && IsIPAddressSegmentValid(segment4))
                        {
                            result.Add(segment1 + "." + segment2 + "." + segment3 + "." + segment4);
                        }
                    }
                }
            }

            return result;
        }

        private bool IsIPAddressSegmentValid(string segment)
        {
            int segmentLength = segment.Length;
            if (segmentLength == 0 || segmentLength > 3 || (segmentLength > 1 && segment[0] == '0') || int.Parse(segment) > 255)
            {
                return false;
            }
            return true;
        }

        private bool IsIPAddressInputValid(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                return false;
            }

            int n = ipAddress.Length;

            if (n < 4 || n > 12)
            {
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                if (!char.IsDigit(ipAddress[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public int FindMaxTwinSum(ListNode head)
        {
            List<int> extractedValues = new List<int>();
            ListNode current = head;

            while (current != null)
            {
                extractedValues.Add(current.val);
                current = current.next;
            }

            int maxTwinSum = 0;

            for (int i = 0; i < extractedValues.Count / 2; i++)
            {
                int twinSum = extractedValues[i] + extractedValues[extractedValues.Count - 1 - i];
                if (twinSum > maxTwinSum)
                {
                    maxTwinSum = twinSum;
                }
            }

            return maxTwinSum;
        }
    }
}
