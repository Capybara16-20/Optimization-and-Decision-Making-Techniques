using System;
using System.Windows.Forms;
using Calculator;

namespace Forms
{
    public partial class InputPeriodsNumber : Form
    {
        private readonly CostCalculator calculator;

        public InputPeriodsNumber(CostCalculator calculator)
        {
            InitializeComponent();

            this.calculator = calculator;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validating())
            {
                int t = int.Parse(textBox1.Text);

                Visible = false;

                new InputProbabilities(calculator, t).ShowDialog();
            }
            else
            {
                textBox1.Text = string.Empty;

                MessageBox.Show("Введены некорректные значения");
            }
        }

        private new bool Validating()
        {
            bool validated = false;
            if ((textBox1.Text != string.Empty) && (int.TryParse(textBox1.Text, out int x)) && (x > 1))
                validated = true;

            return validated;
        }
    }
}
