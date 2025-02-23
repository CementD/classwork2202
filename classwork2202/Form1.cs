using System.Data;
using System.Globalization;
using System.Text;
namespace classwork2202
{
    public partial class Calculator : Form
    {
        private bool isScientific = false;
        public Calculator()
        {
            InitializeComponent();
        }

        private void Number(string num)
        {
            if (textBox1.Text == "0")
                textBox1.Text = num;
            else if (textBox1.Text.Length > 0 && textBox1.Text.Last() == ')')
                textBox1.Text += "*" + num;
            else textBox1.Text += num;
        }

        private void zero_Click(object sender, EventArgs e)
        {
            Number("0");
        }

        private void one_Click(object sender, EventArgs e)
        {
            Number("1");
        }

        private void two_Click(object sender, EventArgs e)
        {
            Number("2");
        }

        private void three_Click(object sender, EventArgs e)
        {
            Number("3");
        }

        private void four_Click(object sender, EventArgs e)
        {
            Number("4");
        }

        private void five_Click(object sender, EventArgs e)
        {
            Number("5");
        }

        private void six_Click(object sender, EventArgs e)
        {
            Number("6");
        }

        private void seven_Click(object sender, EventArgs e)
        {
            Number("7");
        }

        private void eight_Click(object sender, EventArgs e)
        {
            Number("8");
        }

        private void nine_Click(object sender, EventArgs e)
        {
            Number("9");
        }
        private void Operation(char op)
        {
            if (!isScientific)
                equal_Click(null, null);
            textBox1.Text += op;
        }

        private void addition_Click(object sender, EventArgs e)
        {
            Operation('+');
        }

        private void subtraction_Click(object sender, EventArgs e)
        {
            Operation('-');
        }

        private void multiplication_Click(object sender, EventArgs e)
        {
            Operation('*');
        }

        private void division_Click(object sender, EventArgs e)
        {
            Operation('/');
        }

        private void equal_Click(object sender, EventArgs e)
        {
            string expression = textBox1.Text;
            if (isScientific)
            {
                try
                {
                    var result = new DataTable().Compute(expression, null);
                    textBox1.Text = result.ToString();
                    richTextBox1.AppendText($"{expression} = {result}\n");
                }
                catch
                {
                    MessageBox.Show("Error in expression!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                char[] operators = { '+', '-', '*', '/' };
                int operatorIndex = -1;
                char op = ' ';

                for (int i = 1; i < expression.Length; i++)
                {
                    if (operators.Contains(expression[i]))
                    {
                        operatorIndex = i;
                        op = expression[i];
                        break;
                    }
                }
                if (operatorIndex == -1) return;

                string left = "";
                for (int i = 0; i < operatorIndex; i++)
                    left += expression[i];

                string right = "";
                for (int i = operatorIndex + 1; i < expression.Length; i++)
                    right += expression[i];

                if (double.TryParse(left, NumberStyles.Any, CultureInfo.InvariantCulture, out double num1) &&
                    double.TryParse(right, NumberStyles.Any, CultureInfo.InvariantCulture, out double num2))
                {
                    double result = 0;
                    if (op == '+') result = num1 + num2;
                    if (op == '-') result = num1 - num2;
                    if (op == '*') result = num1 * num2;
                    if (op == '/')
                    {
                        if (num2 != 0) result = num1 / num2;
                        else
                        {
                            MessageBox.Show("Division by zero!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    textBox1.Text = result.ToString().Replace(',', '.');
                    richTextBox1.AppendText($"{expression}={result.ToString().Replace(',', '.')}\n");
                }
            }
        }


        private void cancel_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            if (textBox1.Text == "")
                textBox1.Text = "0";
        }

        private void ce_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void square_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
            {
                double result = num * num;
                textBox1.Text = result.ToString(CultureInfo.InvariantCulture);
                richTextBox1.AppendText($"{num}²={result}\n");
            }
            else
                MessageBox.Show("Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void point_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            char[] operators = { '+', '-', '*', '/' };
            string[] parts = text.Split(operators);
            string lastPart = parts[parts.Length - 1];

            if (!lastPart.Contains(".") && lastPart.Length > 0)
                textBox1.Text += ".";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = "";
                saveFileDialog1.Title = "Save File";
                saveFileDialog1.Filter = "Text File|*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    File.WriteAllText(filePath, richTextBox1.Text);
                    MessageBox.Show($"Saved to {filePath}");
                }
            }
            catch
            {
                MessageBox.Show("Incorrect format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
        }

        private void minus_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
            {
                textBox1.Text = "-";
                return;
            }

            if (double.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
            {
                textBox1.Text = (-num).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isScientific = !isScientific;

            openBracket.Visible = isScientific;
            closeBracket.Visible = isScientific;

            button3.Text = isScientific ? "Standard" : "Scientific";
            textBox1.Text = "0";

        }

        private void openBracket_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "0" && char.IsDigit(textBox1.Text.Last()))
            {
                textBox1.Text += "*(";
            }
            else if (textBox1.Text == "0")
            {
                textBox1.Text = "(";
            }
            else
            {
                textBox1.Text += "(";
            }
        }

        private void closeBracket_Click(object sender, EventArgs e)
        {
            int openBrackets = textBox1.Text.Count(c => c == '(');
            int closeBrackets = textBox1.Text.Count(c => c == ')');

            if (openBrackets <= closeBrackets) return;
            if (textBox1.Text.Length > 0 && (char.IsDigit(textBox1.Text.Last()) || textBox1.Text.Last() == ')'))
            {
                textBox1.Text += ")";
            }
        }
    }
}


