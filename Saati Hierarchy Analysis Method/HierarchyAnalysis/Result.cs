using System.Globalization;
using System.Windows.Forms;
using Analyzer;

namespace Forms
{
    public partial class Result : Form
    {
        private HierarchyAnalyzer analyzer;

        public Result(HierarchyAnalyzer analyzer)
        {
            this.analyzer = analyzer;

            InitializeComponent();

            Text = "Результат";
            label1.Text = "Приоритеты альтернатив";

            for (int i = 0; i < analyzer.n; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), "K" + (i + 1).ToString());
            }

            for (int i = 0; i < analyzer.m; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = "B" + (i + 1).ToString();
            }

            var column = new DataGridViewTextBoxColumn()
            {
                Name = "priority",
                HeaderText = "Приоритет"
            };
            dataGridView1.Columns.Add(column);

            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Height = dataGridView1.Rows[0].Height * dataGridView1.Rows.Count + 1;
            dataGridView1.Width = dataGridView1.Columns[0].Width * dataGridView1.Columns.Count + 52;

            Width = dataGridView1.Width + 50;
            Height = dataGridView1.Height + 100;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.AllowUserToAddRows = false;


            double[,] alternativesPriorityMatrix = new double[analyzer.m, analyzer.n];
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                double[,] matrix = new double[analyzer.m, analyzer.m];
                for (int j = 0; j < analyzer.m; j++)
                {
                    for (int z = 0; z < analyzer.m; z++)
                    {
                        matrix[j, z] = analyzer.alternativesCube[i, j, z];
                    }
                }

                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    dataGridView1[i, j].Value = analyzer.GetImportance(matrix)[j].ToString("P", CultureInfo.InvariantCulture);
                    dataGridView1[i, j].ReadOnly = true;
                }

                for (int j = 0; j < analyzer.m; j++)
                {
                    alternativesPriorityMatrix[j, i] = analyzer.GetImportance(matrix)[j];
                }
            }

            double[] criteriaPriority = analyzer.GetImportance(analyzer.criteriaMatrix);
            double[] result = new double[analyzer.m];
            for (int i = 0; i < analyzer.m; i++)
            {
                double sum = 0;
                for (int j = 0; j < analyzer.n; j++)
                {
                    sum += alternativesPriorityMatrix[i, j] * criteriaPriority[j];
                }

                result[i] = sum;
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1[dataGridView1.ColumnCount - 1, i].Value = result[i].ToString("P", CultureInfo.InvariantCulture);
                dataGridView1[dataGridView1.ColumnCount - 1, i].ReadOnly = true;
            }
        }
    }
}
