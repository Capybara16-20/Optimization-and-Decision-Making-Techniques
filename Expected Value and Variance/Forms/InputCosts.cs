using System;
using System.Windows.Forms;
using Calculator;

namespace Forms
{
    public partial class InputCosts : Form
    {
        public InputCosts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validating())
            {
                double repairCost = double.Parse(textBox1.Text);
                double preventiveMaintenanceCost = double.Parse(textBox2.Text);
                int numberOfBroken = int.Parse(textBox3.Text);

                CostCalculator calculator = new CostCalculator(repairCost, preventiveMaintenanceCost, numberOfBroken);

                Visible = false;

                new InputPeriodsNumber(calculator).ShowDialog();

                Environment.Exit(0);
            }
            else
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;

                MessageBox.Show("Введены некорректные значения");
            }
        }

        private new bool Validating()
        {
            bool validated = false;
            if ((textBox1.Text != string.Empty) && (double.TryParse(textBox1.Text, out double x)) && (x > 0))
                if ((textBox2.Text != string.Empty) && (double.TryParse(textBox2.Text, out double y)) && (y > 0))
                    if ((textBox3.Text != string.Empty) && (int.TryParse(textBox3.Text, out int z)) && (z > 0))
                        validated = true;

            return validated;
        }
    }
}
