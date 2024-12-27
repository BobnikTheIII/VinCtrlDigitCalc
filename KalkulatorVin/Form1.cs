using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KalkulatorVin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTextBoxEvents();
        }

        private void InitializeTextBoxEvents()
        {
            for (int i = 1; i <= 16; i++)
            {
                TextBox textBox = (TextBox)this.Controls["textBox" + i];
                textBox.MaxLength = 1;
                textBox.TextChanged += TextBox_TextChanged;
                textBox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            if (!char.IsControl(c) && !char.IsLetterOrDigit(c))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;
            if (currentTextBox.Text.Length > 0)
            {
                char c = currentTextBox.Text[0];

                if (char.IsLetter(c))
                {
                    c = char.ToUpper(c);

                    if (c == 'Q' || c == 'I' || c == 'O')
                    {
                        currentTextBox.Text = "";
                        return;
                    }

                    currentTextBox.Text = c.ToString();
                }

                currentTextBox.SelectionStart = currentTextBox.Text.Length;

                int currentIndex = int.Parse(currentTextBox.Name.Replace("textBox", ""));
                if (currentIndex < 16)
                {
                    TextBox nextTextBox = (TextBox)this.Controls["textBox" + (currentIndex + 1)];
                    nextTextBox.Focus();
                }
            }

            CalculateControlDigit();
        }


        private void CalculateControlDigit()
        {
            string vin = string.Join("", Enumerable.Range(1, 16)
                .Select(i => this.Controls["textBox" + i].Text));

            if (vin.Length == 16 && vin.All(c => !string.IsNullOrEmpty(c.ToString())))
            {
                textBoxCTR.Text = CalcVINCheckDigit(vin).ToString();
            }
            else
            {
                textBoxCTR.Text = "";
            }
        }

        private char CalcVINCheckDigit(string vin)
        {
            vin = vin.ToUpper();
            var cv = new Dictionary<char, int>
    {
        {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
        {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8},
        {'J', 1}, {'K', 2}, {'L', 3}, {'M', 4}, {'N', 5}, {'P', 7}, {'R', 9},
        {'S', 2}, {'T', 3}, {'U', 4}, {'V', 5}, {'W', 6}, {'X', 7}, {'Y', 8}, {'Z', 9}
    };

            var multiplier = new int[] { 8, 7, 6, 5, 4, 3, 2, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int total = 0;
            for (int i = 0; i < 16; i++)
            {
                char c = vin[i];
                if (!cv.ContainsKey(c))
                    throw new ArgumentException($"Invalid character in VIN: {c}");

                total += cv[c] * multiplier[i];
            }

            int remainder = total % 11;
            return remainder == 10 ? 'X' : (char)('0' + remainder);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            {
                for (int i = 1; i <= 16; i++)
                {
                    this.Controls["textBox" + i].Text = "";
                }
                textBoxCTR.Text = "";
                textBox1.Focus();
            }
        }
    }
}
