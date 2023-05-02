using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace SDA_46651r_MyProject
{
    public partial class Form1 : Form
    {
        public Form1()
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

        // https://leetcode.com/problems/longest-common-prefix/
        readonly DialogConfig lcpConfig = new DialogConfig
        {
            InputDialogTitle = "Find the longest common prefix",
            InputDialogLabelText = "Enter 2 or more strings:",
            InputDialogButtonText = "Find LCP",
            InfoDialogTitle = "What is LCP?",
            InfoDialogDescription = "LCP stands for \"longest common prefix\". It is a problem in computer science that involves finding the longest string that is a prefix of a set of strings.\r\n\r\nFor example, consider the set of strings [\"flower\", \"flow\", \"flight\"]. The longest common prefix of these strings is \"fl\"."
        };

        // https://leetcode.com/problems/restore-ip-addresses/
        readonly DialogConfig restoreIPConfig = new DialogConfig
        {
            InputDialogTitle = "Restore IP Addresses",
            InputDialogLabelText = "Enter a string of digits:",
            InputDialogButtonText = "Restore IPs",
            InfoDialogTitle = "How does the 'Restore IPs algorithm' work?",
            InfoDialogDescription = "Given a string of digits, this problem involves restoring all possible valid IP addresses that can be formed by inserting dots into the string. Each integer is between 0 and 255 (inclusive) and cannot have leading zeros. For example, \"25525511135\" can be restored as [\"255.255.11.135\", \"255.255.111.35\"]."
        };

        // https://leetcode.com/problems/maximum-twin-sum-of-a-linked-list/
        readonly DialogConfig maxTwinSumConfig = new DialogConfig
        {
            InputDialogTitle = "Maximum Twin Sum of a Linked List",
            InputDialogLabelText = "Enter 2 or more integers:",
            InputDialogButtonText = "Calculate Maximum Twin Sum",
            InfoDialogTitle = "How does the 'Maximum Twin Sum algorithm' work?",
            InfoDialogDescription = "Given a linked list of even length, this problem involves finding the maximum twin sum of the linked list. A node is the twin of another node if its index is the mirror image of the index of the other node. The twin sum is defined as the sum of a node and its twin."
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
                int[] inputNumbers = { };

                try
                {
                    string textBoxValue = inputForm.Controls[1].Text;

                    int[] numbers = textBoxValue.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(int.Parse)
                                         .ToArray();


                    if (numbers.Length == 2)
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
                        // the input is safe so I can take it
                        inputStrings = strings;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    // Display an error message if the user enters a non-numeric value
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
                    // Display an error message if the user enters an invalid input
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
                    int[] nums = inputString.Split(' ').Select(int.Parse).ToArray();

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
                    // Display an error message if the user enters an invalid input
                    MessageBox.Show(this, "The input should be a space-separated list of non-negative integers.", "Invalid Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

            resultLabel.Top = (resultForm.ClientSize.Height) * 1 / 3 - resultLabel.Height * 1 / 2;
            closeButton.Top = (resultForm.ClientSize.Height) * 2 / 3 - closeButton.Height * 1 / 2;

            // I decided to add it there as well, just to make it responsive - on resizing it should stay centered
            resultForm.SizeChanged += (s3, e3) =>
            {
                resultLabel.Left = (resultForm.Width - resultLabel.Width) / 2;
                closeButton.Left = (resultForm.Width - closeButton.Width) / 2;
            };

            resultLabel.SizeChanged += (s2, e2) =>
            {
                // if the label is too big so it could overlap the button if it was placed near the 2/3rd part of the form; especially needed for the restore ip algo
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

        public string LongestCommonPrefix(string[] strs)
        {
            string prefix = strs[0];

            for (int i = 1; i < strs.Length; i++)
            {
                while (strs[i].IndexOf(prefix) != 0)
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

        public List<string> RestoreIPAddress(string s)
        {
            List<string> result = new List<string>();
            int n = s.Length;

            for (int i = 1; i <= 3; i++)
            {
                for (int j = i + 1; j <= i + 3; j++)
                {
                    for (int k = j + 1; k <= j + 3 && k < n; k++)
                    {
                        string s1 = s.Substring(0, i);
                        string s2 = s.Substring(i, j - i);
                        string s3 = s.Substring(j, k - j);
                        string s4 = s.Substring(k);
                        if (IsIPAddressSegmentValid(s1) && IsIPAddressSegmentValid(s2) && IsIPAddressSegmentValid(s3) && IsIPAddressSegmentValid(s4))
                        {
                            result.Add(s1 + "." + s2 + "." + s3 + "." + s4);
                        }
                    }
                }
            }

            return result;
        }


        private bool IsIPAddressSegmentValid(string s)
        {
            int n = s.Length;
            if (n == 0 || n > 3 || (n > 1 && s[0] == '0') || int.Parse(s) > 255)
            {
                return false;
            }
            return true;
        }

        private bool IsIPAddressInputValid(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            int n = s.Length;

            // Check if the string length is between 4 and 12
            if (n < 4 || n > 12)
            {
                return false;
            }

            // Check if the string contains only digits
            for (int i = 0; i < n; i++)
            {
                if (!char.IsDigit(s[i]))
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

        public static int FindMaxTwinSum(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return 0; // empty list or list with one node
            }

            List<int> nums = new List<int>();
            while (head != null)
            {
                nums.Add(head.val);
                head = head.next;
            }

            nums.Sort(); // sort the list to maximize the twin sum

            int left = 0, right = nums.Count - 1;
            int maxSum = 0;
            while (left < right)
            {
                int sum = nums[left] + nums[right];
                maxSum = Math.Max(maxSum, sum);
                left++;
                right--;
            }

            return maxSum;
        }
    }
}
