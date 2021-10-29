using System;
using System.Windows.Forms;
using Analyzer;

namespace Forms
{
    public partial class InputConstCosts : Form
    {
        public InputConstCosts()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validating())
            {
                int constCosts = int.Parse(textBox1.Text);

                MarginAnalyzer analyzer = new MarginAnalyzer(constCosts);

                Visible = false;

                new Main(analyzer).ShowDialog();

                Environment.Exit(0);
            }
            else
            {
                textBox1.Text = string.Empty;

                MessageBox.Show("Введите целое число больше нуля!");
            }
        }

        private new bool Validating()
        {
            bool validated = false;
            if ((textBox1.Text != string.Empty) && (int.TryParse(textBox1.Text, out int x)) && (x > 0))
                validated = true;

            return validated;
        }
    }
}
