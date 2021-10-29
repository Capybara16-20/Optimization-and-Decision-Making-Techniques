using System;
using System.Drawing;
using System.Windows.Forms;
using Calculator;

namespace Forms
{
    public partial class ShowResult : Form
    {
        private readonly CostCalculator calculator;
        private readonly int t;
        private readonly double[] probabilities;

        public ShowResult(CostCalculator calculator, int t, double[] probabilities)
        {
            InitializeComponent();

            this.calculator = calculator;
            this.t = t;
            this.probabilities = probabilities;

            dataGridView1.Columns.Add("t", "T");
            dataGridView1.Columns.Add("p", "p(t)");
            dataGridView1.Columns.Add("p", "p²(t)");
            dataGridView1.Columns.Add("p", "∑p(t)");
            dataGridView1.Columns.Add("p", "∑p²(t)");
            dataGridView1.Columns.Add("costs", "Затраты");
            
            for (int i = 0; i < t; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = (i + 1).ToString();
            }
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Height = dataGridView1.Rows[0].Height * dataGridView1.Rows.Count + 1;
            dataGridView1.Width = dataGridView1.Columns[0].Width * dataGridView1.Columns.Count + 43;
            Width = dataGridView1.Width + 43;
            Height = dataGridView1.Height + 120;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.AllowUserToAddRows = false;

            double[] temporalProbabilities = calculator.GetTemporalProbabilities(t, probabilities);
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1[1, i].Value = temporalProbabilities[i].ToString("0.####");

            double[] temporalProbabilitiesSquares = calculator.GetTemporalProbabilitiesSquares(t, probabilities);
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1[2, i].Value = temporalProbabilitiesSquares[i].ToString("0.####");

            double[] temporalProbabilitiesSums = calculator.GetTemporalProbabilitiesSums(t, probabilities);
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1[3, i].Value = temporalProbabilitiesSums[i].ToString("0.####");

            double[] temporalProbabilitiesSquaresSums = calculator.GetTemporalProbabilitiesSquaresSums(t, probabilities);
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1[4, i].Value = temporalProbabilitiesSquaresSums[i].ToString("0.####");

            double[] totalCosts = calculator.GetTotalCosts(t, probabilities);
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1[5, i].Value = totalCosts[i].ToString("0.###");

            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1[j, i].ReadOnly = true;

            double minTotalCost = calculator.GetMinTotalCost(t, probabilities);
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (double.Parse(dataGridView1[5, i].Value.ToString()) == double.Parse(minTotalCost.ToString("0.###")))
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        dataGridView1[j, i].Style.BackColor = Color.GreenYellow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;

            new InputProbabilities(calculator, t).ShowDialog();
        }
    }
}
