using System;
using System.Drawing;
using System.Windows.Forms;
using Analyzer;

namespace Forms
{
    public partial class Main : Form
    {
        private MarginAnalyzer analyzer;

        public Main(MarginAnalyzer analyzer)
        {
            this.analyzer = analyzer;

            InitializeComponent();

            menuStrip1.Items[1].Visible = false;

            Height = 240;
            Width = 470;

            dataGridView1.ScrollBars = ScrollBars.None;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.RowHeadersWidth = 160;

            UpdateGrid(analyzer);
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddProduction(analyzer).ShowDialog();

            UpdateGrid(analyzer);
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (analyzer.productions.Count > 1)
                analyzer.RemoveProduction();
            else
                MessageBox.Show("Невозможно отменить!");

            UpdateGrid(analyzer);
        }

        private void UpdateGrid(MarginAnalyzer analyzer)
        {
            if (analyzer.productions.Count > 1)
                menuStrip1.Items[1].Visible = true;

            dataGridView1.Rows.Clear();

            for (int i = 0; i < 7; i++)
                dataGridView1.Rows.Add();
            dataGridView1.Rows[0].HeaderCell.Value = "Постоянные издержки";
            dataGridView1.Rows[1].HeaderCell.Value = "Количество";
            dataGridView1.Rows[2].HeaderCell.Value = "Выручка";
            dataGridView1.Rows[3].HeaderCell.Value = "Переменные издержки";
            dataGridView1.Rows[4].HeaderCell.Value = "Общие издержки";
            dataGridView1.Rows[5].HeaderCell.Value = "Прибыль";
            dataGridView1.Rows[6].HeaderCell.Value = "Предельные издержки";

            dataGridView1[0, 0].Value = analyzer.ConstCosts.ToString();

            if (analyzer.productions.Count == 2)
            {
                Production currentProduction = analyzer.productions[analyzer.productions.Count - 2];
                Production newProduction = analyzer.productions[analyzer.productions.Count - 1];

                dataGridView1[0, 1].Value = currentProduction.Number;
                dataGridView1[0, 2].Value = currentProduction.Proceeds;
                dataGridView1[0, 3].Value = currentProduction.VariableCost;
                dataGridView1[0, 4].Value = currentProduction.VariableCost;
                dataGridView1[0, 5].Value = currentProduction.Profit;

                dataGridView1[1, 1].Value = newProduction.Number;
                dataGridView1[1, 2].Value = newProduction.Proceeds;
                dataGridView1[1, 3].Value = newProduction.VariableCost;
                dataGridView1[1, 4].Value = newProduction.VariableCost + analyzer.ConstCosts;
                dataGridView1[1, 5].Value = newProduction.Profit - analyzer.ConstCosts;
                dataGridView1[1, 6].Value = newProduction.MarginalCost;

                dataGridView1[2, 1].Value = currentProduction.Number + newProduction.Number;
                dataGridView1[2, 2].Value = currentProduction.Proceeds + newProduction.Proceeds;
                dataGridView1[2, 3].Value = currentProduction.VariableCost + newProduction.VariableCost;
                dataGridView1[2, 4].Value = currentProduction.VariableCost + analyzer.ConstCosts + newProduction.VariableCost;
                dataGridView1[2, 5].Value = currentProduction.Profit + newProduction.Profit - analyzer.ConstCosts;

                if ((newProduction.Profit - analyzer.ConstCosts) < 0)
                    dataGridView1[1, 5].Style.BackColor = Color.Red;
                else
                    dataGridView1[1, 5].Style.BackColor = Color.Green;
            }
            else if (analyzer.productions.Count > 2)
            {
                Production newProduction = analyzer.productions[analyzer.productions.Count - 1];

                int currentNumber = 0;
                int currentProceeds = 0;
                int currentVariableCost = 0;
                int currentProfit = 0;
                for (int i = 0; i < analyzer.productions.Count - 1; i++)
                {
                    currentNumber += analyzer.productions[i].Number;
                    currentProceeds += analyzer.productions[i].Proceeds;
                    currentVariableCost += analyzer.productions[i].VariableCost;
                    currentProfit += analyzer.productions[i].Profit;
                }

                dataGridView1[0, 1].Value = currentNumber;
                dataGridView1[0, 2].Value = currentProceeds;
                dataGridView1[0, 3].Value = currentVariableCost;
                dataGridView1[0, 4].Value = currentVariableCost + analyzer.ConstCosts;
                dataGridView1[0, 5].Value = currentProfit - analyzer.ConstCosts;

                dataGridView1[1, 1].Value = newProduction.Number;
                dataGridView1[1, 2].Value = newProduction.Proceeds;
                dataGridView1[1, 3].Value = newProduction.VariableCost;
                dataGridView1[1, 4].Value = newProduction.VariableCost;
                dataGridView1[1, 5].Value = newProduction.Profit;
                dataGridView1[1, 6].Value = newProduction.MarginalCost;

                dataGridView1[2, 1].Value = currentNumber + newProduction.Number;
                dataGridView1[2, 2].Value = currentProceeds + newProduction.Proceeds;
                dataGridView1[2, 3].Value = currentVariableCost + newProduction.VariableCost;
                dataGridView1[2, 4].Value = currentVariableCost + analyzer.ConstCosts + newProduction.VariableCost;
                dataGridView1[2, 5].Value = currentProfit + newProduction.Profit - analyzer.ConstCosts;

                if ((newProduction.Profit) < 0)
                    dataGridView1[1, 5].Style.BackColor = Color.Red;
                else
                    dataGridView1[1, 5].Style.BackColor = Color.Green;
            }
        }
    }
}
