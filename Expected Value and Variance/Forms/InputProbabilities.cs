using System;
using System.Windows.Forms;
using Calculator;

namespace Forms
{
    public partial class InputProbabilities : Form
    {
        private readonly CostCalculator calculator;
        private readonly int t;

        public InputProbabilities(CostCalculator calculator, int t)
        {
            InitializeComponent();

            this.calculator = calculator;
            this.t = t;

            for (int i = 0; i < t; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), "T" + (i + 1).ToString());
                dataGridView1.Columns[i].Width = 60;
            }
            dataGridView1.Rows.Add();
            for (int i = 0; i < t; i++)
                dataGridView1[i, 0].Value = string.Empty;
            dataGridView1.Rows[0].HeaderCell.Value = "p(t)";
            dataGridView1.RowHeadersWidth = 60;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Height = dataGridView1.Rows[0].Height * dataGridView1.Rows.Count + 1;
            dataGridView1.Width = dataGridView1.Columns[0].Width * dataGridView1.Columns.Count + 63;
            Width = dataGridView1.Width + 43;
            Height = dataGridView1.Height + 120;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validating())
            {
                double[] probabilities = new double[dataGridView1.ColumnCount];
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    probabilities[i] = double.Parse(dataGridView1[i, 0].Value.ToString());

                Visible = false;
                
                new ShowResult(calculator, t, probabilities).ShowDialog();
            }
            else
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    dataGridView1[i, 0].Value = string.Empty;

                MessageBox.Show("Введены некорректные значения");
            }
        }

        private new bool Validating()
        {
            bool validated = true;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                string value = dataGridView1[i, 0].Value.ToString();
                if ((value == string.Empty) || !double.TryParse(value, out double x) || (x <= 0) || (x >= 1))
                    validated = false;
            }

            return validated;
        }
    }
}
