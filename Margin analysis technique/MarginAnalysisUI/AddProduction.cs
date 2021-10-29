using System;
using System.Windows.Forms;
using Analyzer;

namespace Forms
{
    public partial class AddProduction : Form
    {
        private MarginAnalyzer analyzer;

        public AddProduction(MarginAnalyzer analyzer)
        {
            this.analyzer = analyzer;

            InitializeComponent();

            Height = 150;
            Width = 270;

            dataGridView1.ScrollBars = ScrollBars.None;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.RowHeadersWidth = 160;

            for (int i = 0; i < 3; i++)
                dataGridView1.Rows.Add();
            dataGridView1.Rows[0].HeaderCell.Value = "Цена";
            dataGridView1.Rows[1].HeaderCell.Value = "Количество";
            dataGridView1.Rows[2].HeaderCell.Value = "Переменные издержки";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validating())
            {
                int price = int.Parse(dataGridView1[0, 0].Value.ToString());
                int number = int.Parse(dataGridView1[0, 1].Value.ToString());
                int variableCost = int.Parse(dataGridView1[0, 2].Value.ToString());
                analyzer.AddProduction(new Production(price, number, variableCost));

                Close();
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    dataGridView1[0, i].Value = string.Empty;

                MessageBox.Show("Введите целые числа больше нуля!");
            }
        }

        private new bool Validating()
        {
            bool validated = false;

            string price = dataGridView1[0, 0].Value.ToString();
            string number = dataGridView1[0, 1].Value.ToString();
            string variableCost = dataGridView1[0, 2].Value.ToString();
            if ((price != string.Empty) && (int.TryParse(price, out int x)) && (x > 0))
                if ((number != string.Empty) && (int.TryParse(number, out int y)) && (x > 0))
                    if ((variableCost != string.Empty) && (int.TryParse(variableCost, out int z)) && (z > 0))
                        validated = true;

            return validated;
        }
    }
}
